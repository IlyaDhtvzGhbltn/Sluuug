using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Context;
using Slug.Context.Dto.Albums;
using Slug.Context.Dto.Cloudinary;
using Slug.Context.Dto.Comments;
using Slug.Context.Dto.Fotos;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Tables;
using Slug.Helpers.BaseController;
using Slug.ImageEdit;
using Slug.Model.Albums;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;

namespace Slug.Helpers
{
    public class AlbumsHandler
    {
        public FotoModel GetFotoByGUID(string session, Guid foto)
        {
            var handler = new UsersHandler();
            FullUserInfoModel userUploader = handler.GetFullUserInfo(session);
            using (var context = new DataBaseContext())
            {
                Foto calledFoto = context.Fotos.Where(x => x.FotoGUID == foto).FirstOrDefault();
                if (calledFoto != null)
                {
                    bool isFriens = FriendshipChecker.CheckUsersFriendshipByIDs(userUploader.UserId, calledFoto.UploadUserID);
                    if (isFriens || userUploader.UserId == calledFoto.UploadUserID)
                    {
                        FotoModel model = new FotoModel()
                        {
                            ID = calledFoto.FotoGUID,
                            FullFotoUri = calledFoto.Url,
                            AuthorDescription = calledFoto.Description,
                            Title = calledFoto.Title,
                            UploadDate = calledFoto.UploadDate
                        };
                        return model;
                    }
                }
            }
            return null;
        }

        public AlbumModel GetAlbumByGUID(string session, Guid album)
        {
            var handler = new UsersHandler();
            FullUserInfoModel userUploader = handler.GetFullUserInfo(session);
            using (var context = new DataBaseContext())
            {
                Album calledAlbum = context.Albums.FirstOrDefault(x => x.Id == album);
                if (calledAlbum != null)
                {
                    bool friends = FriendshipChecker.CheckUsersFriendshipByIDs(userUploader.UserId, calledAlbum.CreateUserID);
                    if (friends || calledAlbum.CreateUserID == userUploader.UserId)
                    {

                        var fotos = new List<FotoModel>();
                        calledAlbum.Fotos.ForEach(foto => 
                        {
                            var comments = new List<FotoCommentModel>();
                            foto.UserComments.ForEach(comment => 
                            {
                                FullUserInfoModel userCommenter = handler.GetFullUserInfo(comment.UserCommenter);
                                var userComment = new FotoCommentModel()
                                {
                                     Text = comment.CommentText,
                                     UserName = userCommenter.Name,
                                     UserSurName = userCommenter.SurName,
                                     UserPostedAvatarUri = Resize.ResizedUri( userCommenter.AvatarUri, ModTypes.c_scale, 40
                                     ),
                                     UserPostedID = userCommenter.UserId,
                                     DateFormat = comment.CommentWriteDate.ToLongDateString()
                                };
                                comments.Add(userComment);
                            });

                            var fotoModel = new FotoModel()
                            {
                                 Album = calledAlbum.Id,
                                 AuthorDescription = foto.Description,
                                 Title = foto.Title,
                                 ID = foto.FotoGUID,
                                 SmallFotoUri = Resize.ResizedUri(foto.Url, ModTypes.c_scale, 50), 
                                 UploadDate = foto.UploadDate,
                                 FotoComments = comments
                            };
                            fotos.Add(fotoModel);
                        });

                        AlbumModel model = new AlbumModel()
                        {
                             AlbumLabelUrl = calledAlbum.AlbumLabelUrl,
                             AuthorComment = calledAlbum.Description,
                             CreationTime = calledAlbum.CreationDate,
                             Title = calledAlbum.Title,
                             AlbumId = calledAlbum.Id,
                             Fotos = fotos
                        };
                        return model;
                    }
                }
            }
            return null;
        }

        public CreateAlbumResponse CreateAlbum(string session, AlbumModel model, HttpPostedFileBase albumLabel)
        {
            Guid albumGUID = Guid.NewGuid();
            var handler = new UsersHandler();
            var userUploader = handler.GetFullUserInfo(session);

            string labelUri = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1557838909/system/template.jpg";
            string labePubID = null;
            if(albumLabel != null )
            {
                CloudImageUploadResult upl = SlugController.UploadImg(albumLabel, "/users/albums/" + albumGUID.ToString());
                labelUri = upl.SecureUrl.ToString();
                labePubID = upl.PublicId;
            }
            try
            {
                using (var context = new DataBaseContext())
                {
                    var album = new Album()
                    {
                        Id = albumGUID,
                        AlbumLabelUrl = labelUri,
                        Description = model.AuthorComment,
                        CreateUserID = userUploader.UserId,
                        CreationDate = DateTime.Now,
                        Title = model.Title,
                        User = context.Users.First(x => x.Id == userUploader.UserId),
                        AlbumLabesPublicID = labePubID
                    };
                    context.Albums.Add(album);
                    context.SaveChanges();
                    return new CreateAlbumResponse() { isSuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new CreateAlbumResponse() { isSuccess = false, Comment = ex.Message };
            }
        }

        public UploadAlbumResponse UploadToAlbum(string session, FotoModel model, UploadModel uploadFiles)
        {
            var result = new UploadAlbumResponse();

            var handler = new UsersHandler();
            int userUploaderID = handler.GetFullUserInfo(session).UserId;
            using (var context = new DataBaseContext())
            {
                var album = context.Albums.FirstOrDefault(x => x.Id == model.Album);
                if (album == null)
                {
                    result.Comment = UploadAlbumResponse.Errors.NOT_EXIST;
                    return result;
                }
                else
                {
                    if (album.CreateUserID != userUploaderID)
                    {
                        result.Comment = UploadAlbumResponse.Errors.NOT_ACCESS;
                        return result;
                    }
                    else
                    {
                        if (uploadFiles.Files.Count() != 0)
                        {
                            foreach (var file in uploadFiles.Files)
                            {
                                var uploadCloud = SlugController.UploadImg(file, "/users/albums/" + model.Album.ToString());
                                var uplFoto = new Foto()
                                {
                                    AlbumID = model.Album,
                                    FotoGUID = Guid.NewGuid(),
                                    Title = model.Title,
                                    Description = model.AuthorDescription,
                                    UploadDate = DateTime.Now,
                                    UploadUserID = userUploaderID,
                                    Url = uploadCloud.SecureUrl.ToString(),
                                    ImagePublicID = uploadCloud.PublicId
                                };
                                context.Fotos.Add(uplFoto);
                            }
                            context.SaveChanges();
                            result.isSuccess = true;
                            return result;
                        }
                        else
                        {
                            result.Comment = UploadAlbumResponse.Errors.NOT_FILE_SELECT;
                            return result;
                        }
                    }
                }
            }
        }

        public AlbumPhotosResponse GetMyPhotosInAlbum(string session, Guid albumID)
        {
            var resp = new AlbumPhotosResponse();
            var handler = new UsersHandler();
            int getUserId = handler.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                Album album = context.Albums.FirstOrDefault(x => x.Id == albumID);
                if (album == null)
                {
                    resp.Comment = AlbumPhotosResponse.Errors.NOT_EXIST;
                    return resp;
                }
                else
                {
                    bool isFriendlyAlbum = FriendshipChecker.CheckUsersFriendshipByIDs(getUserId, album.CreateUserID);

                    if (isFriendlyAlbum || album.CreateUserID == getUserId)
                    {
                        resp.Photos = new List<FotoModel>();
                        album.Fotos.ToList().ForEach(foto =>
                        {
                            var fModel = new FotoModel()
                            {
                                Album = foto.AlbumID,
                                SmallFotoUri = Resize.ResizedUri(foto.Url, ModTypes.c_scale, 50),
                                FullFotoUri = foto.Url,
                                AuthorDescription = foto.Description,
                                Title = foto.Title,
                                UploadDate = foto.UploadDate,
                                ID = foto.FotoGUID,
                                PositiveRating = foto.PositiveRating,
                                NegativeRating = foto.NegativeRating
                            };
                            resp.Photos.Add(fModel);
                        });

                        resp.isSucces = true;
                        resp.Count = album.Fotos.Count;
                        return resp;
                    }
                    else
                    {
                        resp.Comment = AlbumPhotosResponse.Errors.NOT_ACCESS;
                        return resp;
                    }
                }
            }
        }

        public EditFotoResponse EditFotoInfo(string session, EditFotoInfoModel model)
        {
            var resp = new EditFotoResponse();
            var handler = new UsersHandler();
            int editorID = handler.GetFullUserInfo(session).UserId;
            using (var context = new DataBaseContext())
            {
                Foto foto = context.Fotos.FirstOrDefault(x => x.FotoGUID == model.PhotoGUID);
                if (foto == null)
                {
                    resp.Comment = EditFotoResponse.Errors.NOT_EXIST;
                }
                else
                {
                    int fotoUploadedUserID = foto.UploadUserID;
                    if (editorID != fotoUploadedUserID)
                    {
                        resp.Comment = EditFotoResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(model.NewValue))
                        {
                            resp.Comment = EditFotoResponse.Errors.EMPTY_VALUE;
                        }
                        else
                        {
                            if (model.EditMode == EditMode.EditTitle)
                            {
                                foto.Title = model.NewValue;
                            }
                            else if (model.EditMode == EditMode.EditDesc)
                            {
                                foto.Description = model.NewValue;
                            }
                            context.SaveChanges();
                            resp.isSuccess = true;
                        }
                    }
                }
            }

            return resp;
        }

        public DropFotoResponse DropFoto(string session, Guid fotoGUID)
        {
            var resp = new DropFotoResponse();
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;
            using (var context = new DataBaseContext())
            {
                var fotoInf = context.Fotos.FirstOrDefault(x => x.FotoGUID == fotoGUID);
                if (fotoInf == null)
                {
                    resp.Comment = DropFotoResponse.Errors.NOT_EXIST;
                }
                else
                {
                    if (fotoInf.UploadUserID != userID)
                    {
                        resp.Comment = DropFotoResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {

                        var account = new Account(
                          WebAppSettings.AppSettings[AppSettingsEnum.cloud.ToString()],
                          WebAppSettings.AppSettings[AppSettingsEnum.apiKey.ToString()],
                          WebAppSettings.AppSettings[AppSettingsEnum.apiSecret.ToString()]);

                        Cloudinary cloudinary = new Cloudinary(account);
                        DelResResult delResponse = cloudinary.DeleteResources(ResourceType.Image, fotoInf.ImagePublicID);
                        var deleteResult = delResponse.JsonObj.ToObject<CloudDeleteImage>();
                        if (deleteResult.deleted != null && delResponse.Deleted.Count == 1)
                        {
                            if (fotoInf.UserComments != null)
                            {
                                context.FotoComments.RemoveRange(fotoInf.UserComments);
                            }
                            context.Fotos.Remove(fotoInf);
                            context.SaveChanges();
                            resp.isSuccess = true;
                        }
                    }
                }
            }
            return resp;
        }

        public DropAlbumResponse DropAlbum(string session, Guid albumGUID)
        {
            var response = new DropAlbumResponse();
            var handler = new UsersHandler();

            int userID = handler.GetFullUserInfo(session).UserId;
            using (var context = new DataBaseContext())
            {
                var album = context.Albums.FirstOrDefault(x => x.Id == albumGUID);
                if (album == null)
                {
                    response.Comment = DropAlbumResponse.Errors.NOT_EXIST;
                }
                else
                {
                    if (album.CreateUserID != userID)
                    {
                        response.Comment = DropAlbumResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        var fotosPublicID = album.Fotos.Select(x => x.ImagePublicID).ToList();
                        if (!string.IsNullOrWhiteSpace(album.AlbumLabesPublicID) && !album.AlbumLabelUrl.Contains("system/template.jpg"))
                        {
                            fotosPublicID.Add(album.AlbumLabesPublicID);
                        }

                        var comments = new List<FotoComment>();
                        foreach (var item in album.Fotos)
                        {
                            comments.AddRange(item.UserComments);
                        }
                        context.FotoComments.RemoveRange(comments);
                        context.Fotos.RemoveRange(album.Fotos);
                        context.Albums.Remove(album);

                        context.SaveChanges();

                        if (fotosPublicID.Count > 0)
                        {
                            var account = new Account(
                                  WebAppSettings.AppSettings[AppSettingsEnum.cloud.ToString()],
                                  WebAppSettings.AppSettings[AppSettingsEnum.apiKey.ToString()],
                                  WebAppSettings.AppSettings[AppSettingsEnum.apiSecret.ToString()]);
                            Cloudinary cloudinary = new Cloudinary(account);

                            DelResResult delResponse = cloudinary.DeleteResources(ResourceType.Image, fotosPublicID.ToArray());
                            var deleteResult = delResponse.JsonObj.ToObject<CloudDeleteImage>();

                            if (deleteResult.deleted != null && delResponse.Deleted.Count > 0)
                            {
                                response.isSuccess = true;
                            }
                        }
                        else
                        {
                            response.isSuccess = true;
                        }
                    }
                }
            }

            return response;
        }

        public FotoCommentsResponse GetCommentsToFoto(string session, Guid fotoGUID)
        {
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;

            var resp = new FotoCommentsResponse();
            using (var context = new DataBaseContext())
            {
                var fotoInf = context.Fotos.FirstOrDefault(x => x.FotoGUID == fotoGUID);
                if (fotoInf == null)
                {
                    resp.Comment = DropFotoResponse.Errors.NOT_EXIST;
                }
                else
                {
                    bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, fotoInf.UploadUserID);

                    if (fotoInf.UploadUserID != userID && !friends)
                    {
                        resp.Comment = DropFotoResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        resp.isSuccess = true;

                        var comments = context.FotoComments.Where(x => x.Foto.FotoGUID == fotoInf.FotoGUID).OrderBy(x=>x.CommentWriteDate).ToList();
                        resp.FotoComments = new List<FotoCommentModel>();
                        comments.ForEach(comm => 
                        {
                            int? avatarID = context.Users.First(x => x.Id == comm.UserCommenter).AvatarId;
                            string avatarImgPath = context.Avatars.First(x => x.Id == avatarID).ImgPath;
                            var commModel = new FotoCommentModel()
                            {
                                UserPostedAvatarUri = Resize.ResizedUri(avatarImgPath, ModTypes.c_scale, 50),
                                PostDate = comm.CommentWriteDate.Ticks,
                                Text = comm.CommentText,
                                UserName = context.Users.First(x=>x.Id == comm.UserCommenter).UserFullInfo.Name,
                                UserSurName = context.Users.First(x => x.Id == comm.UserCommenter).UserFullInfo.SurName,
                                UserPostedID = comm.UserCommenter
                            };
                            resp.FotoComments.Add(commModel);
                        });
                    }
                }
            }

            return resp;
        }

        public PostCommentsResponse PostNewComments(string session, PostCommentToFoto model)
        {
            var response = new PostCommentsResponse();
            var handler = new UsersHandler();

            int userID = handler.GetFullUserInfo(session).UserId;
            using (var context = new DataBaseContext())
            {
                var foto = context.Fotos.FirstOrDefault(x => x.FotoGUID == model.FotoID);
                if (foto == null)
                {
                    response.Comment = DropAlbumResponse.Errors.NOT_EXIST;
                }
                else
                {
                    bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, foto.UploadUserID);

                    if (foto.UploadUserID != userID && !friends)
                    {
                        response.Comment = DropAlbumResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        var comment = new FotoComment()
                        {
                             CommentWriteDate = DateTime.Now,
                             UserCommenter = userID,
                             CommentText = model.CommentText,
                             Foto = foto
                        };
                        context.FotoComments.Add(comment);
                        context.SaveChanges();
                        response.isSuccess = true;
                    }
                }
            
            }
            return response;
        }
    }
}