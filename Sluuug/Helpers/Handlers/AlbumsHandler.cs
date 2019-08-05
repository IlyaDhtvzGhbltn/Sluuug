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
using Slug.Model.Users.Relations;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            MyProfileModel userUploader = handler.GetCurrentProfileInfo(session);
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
                            PhotoDescription = calledFoto.Description,
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
            MyProfileModel userUploader = handler.GetCurrentProfileInfo(session);
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
                                FriendModel userCommenter = handler.GetFullUserInfo(comment.UserCommenter);
                                var userComment = new FotoCommentModel()
                                {
                                     Text = comment.CommentText,
                                     UserName = userCommenter.Name,
                                     UserSurName = userCommenter.SurName,
                                     UserPostedAvatarResizeUri = Resize.ResizedAvatarUri( userCommenter.AvatarResizeUri, ModTypes.c_scale, 40, 40),
                                     UserPostedID = userCommenter.UserId,
                                     DateFormat = comment.CommentWriteDate.ToLongDateString()
                                };
                                comments.Add(userComment);
                            });

                            var fotoModel = new FotoModel()
                            {
                                 Album = calledAlbum.Id,
                                 PhotoDescription = foto.Description,
                                 Title = foto.Title,
                                 ID = foto.FotoGUID,
                                 SmallFotoUri = Resize.ResizedAvatarUri(foto.Url, ModTypes.c_scale, 50, 500), 
                                 UploadDate = foto.UploadDate,
                                 FotoComments = comments
                            };
                            fotos.Add(fotoModel);
                        });

                        AlbumModel model = new AlbumModel()
                        {
                             AlbumLabelUrl = calledAlbum.AlbumLabelUrl,
                             AlbumDescription = calledAlbum.Description,
                             CreationTime = calledAlbum.CreationDate,
                             AlbumTitle = calledAlbum.Title,
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
            var userUploader = handler.GetCurrentProfileInfo(session);

            string labelUri = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1563899112/system/template.jpg";
            string labePubID = null;
            long originalHeight = 0;
            long originalWidth = 0;
            if(albumLabel != null )
            {
                CloudImageUploadResult upl = SlugController.UploadImg(albumLabel, "/users/albums/" + albumGUID.ToString());
                labelUri = upl.SecureUrl.ToString();
                labePubID = upl.PublicId;
                originalHeight = upl.Height;
                originalWidth = upl.Width;
            }
            string albumDescription = model.AlbumDescription == null ? model.AlbumDescription = "..." : model.AlbumDescription;
            try
            {
                using (var context = new DataBaseContext())
                {
                    var album = new Album()
                    {
                        Id = albumGUID,
                        AlbumLabelUrl = labelUri,
                        Description = albumDescription,
                        CreateUserID = userUploader.UserId,
                        CreationDate = DateTime.Now,
                        Title = model.AlbumTitle,
                        User = context.Users.First(x => x.Id == userUploader.UserId),
                        AlbumLabesPublicID = labePubID,
                        LabelOriginalHeight = originalHeight,
                        LabelOriginalWidth = originalWidth
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

        public CreateAlbumResponse CreateEventsAlbum(string eventTitle, int userCreator)
        {
            Guid albumGUID = Guid.NewGuid();
            using (var context = new DataBaseContext())
            {
                var album = new Album()
                {
                    Id = albumGUID,
                    AlbumLabelUrl = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1563899112/system/template.jpg",
                    CreateUserID = userCreator,
                    CreationDate = DateTime.Now,
                    Title = eventTitle + "_event",
                    User = context.Users.First(x => x.Id == userCreator),
                };
                context.Albums.Add(album);
                context.SaveChanges();
                return new CreateAlbumResponse()
                {
                    isSuccess = true,
                    AlbumId = album.Id
                };
            }
        }


        public UploadAlbumResponse UploadToAlbum(string session, Guid albumId, IEnumerable<HttpPostedFileBase> uploadFiles, string uploadPath = "/users/albums/")
        {
            var result = new UploadAlbumResponse();

            var handler = new UsersHandler();
            int userUploaderID = handler.UserIdBySession(session);
            using (var context = new DataBaseContext())
            {
                var album = context.Albums.FirstOrDefault(x => x.Id == albumId);
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
                        if (uploadFiles.Count() != 0)
                        {
                            foreach (var file in uploadFiles)
                            {
                                var uploadCloud = SlugController.UploadImg(file, uploadPath + albumId.ToString());
                                var uplFoto = new Foto()
                                {
                                    AlbumID = albumId,
                                    FotoGUID = Guid.NewGuid(),
                                    Title = "...",
                                    Description = "...",
                                    UploadDate = DateTime.Now,
                                    UploadUserID = userUploaderID,
                                    Url = uploadCloud.SecureUrl.ToString(),
                                    ImagePublicID = uploadCloud.PublicId,
                                    Height = uploadCloud.Height,
                                    Width = uploadCloud.Width

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

        public AlbumPhotosResponse ExpandAlbum(string session, Guid albumID)
        {
            var resp = new AlbumPhotosResponse();
            var usersHandler = new UsersHandler();
            int getUserId = usersHandler.UserIdBySession(session);

            using (var context = new DataBaseContext())
            {
                var album = context.Albums
                    .Where(x => x.Id == albumID)
                    .Select(col => new { col.Title, col.CreateUserID, col.Fotos })
                    .FirstOrDefault();
                if (album == null)
                {
                    resp.Comment = AlbumPhotosResponse.Errors.NOT_EXIST;
                    return resp;
                }
                else
                {
                    bool isFriendlyAlbum = FriendshipChecker.CheckUsersFriendshipByIDs(getUserId, album.CreateUserID);


                    if (!isFriendlyAlbum && album.CreateUserID != getUserId)
                    {
                        resp.Comment = AlbumPhotosResponse.Errors.NOT_ACCESS;
                        return resp;
                    }
                    else
                    {
                        resp.Photos = new List<FotoModel>();
                        album.Fotos
                            .OrderBy(x => x.UploadDate)
                            .Select(col => new { col.AlbumID, col.Url, col.Title, col.FotoGUID, col.Description, col.Height, col.Width })
                            .ToList()
                            .ForEach(foto =>
                        {
                            var fModel = new FotoModel()
                            {
                                Album = foto.AlbumID,
                                SmallFotoUri = Resize.ResizedAvatarUri(foto.Url, ModTypes.c_scale, 60, 50),
                                FullFotoUri = Resize.ResizedFullPhoto(foto.Url, foto.Height, foto.Width),
                                DownloadFotoUri = foto.Url,
                                PhotoDescription = foto.Description,
                                Title = foto.Title,
                                ID = foto.FotoGUID,
                                //UploadDate = foto.UploadDate,
                                //PositiveRating = foto.PositiveRating,
                                //NegativeRating = foto.NegativeRating
                            };
                            resp.Photos.Add(fModel);
                        });
                        int photosInAlbum = album.Fotos.Count;
                        if (photosInAlbum > 0)
                        {
                            Guid firstPhotoId = resp.Photos.First().ID;
                            List<FotoComment> photoComments = context.Fotos.Where(x => x.FotoGUID == firstPhotoId).First().UserComments;
                            resp.Photos.First(x => x.ID == firstPhotoId).FotoComments = new List<FotoCommentModel>();

                            photoComments.ForEach(comment =>
                            {
                                User commenter = context.Users.FirstOrDefault(x => x.Id == comment.UserCommenter);
                                string commenterAvatar = context.Avatars.First(x => x.Id == commenter.AvatarId).ImgPath;
                                resp.Photos.First(x => x.ID == firstPhotoId).FotoComments.Add(new FotoCommentModel()
                                {
                                    Text = comment.CommentText,
                                    DateFormat = comment.CommentWriteDate.ToString("ddd d MMM HH:mm", CultureInfo.CreateSpecificCulture("ru-RU")),
                                    UserName = commenter.UserFullInfo.Name,
                                    UserSurName = commenter.UserFullInfo.SurName,
                                    UserPostedAvatarResizeUri = Resize.ResizedAvatarUri(commenterAvatar, ModTypes.c_scale, 55, 55),
                                    UserPostedID = comment.UserCommenter,
                                });
                            });
                        }
                        resp.Succes = true;
                        resp.PhotosCount = photosInAlbum;
                        return resp;
                    }
                }
            }
        }

        public EditFotoResponse EditFotoInfo(string session, EditFotoInfoModel model)
        {
            var resp = new EditFotoResponse();
            var userHandler = new UsersHandler();
            int editorID = userHandler.UserIdBySession(session);
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
            var userHandler = new UsersHandler();
            int userID = userHandler.UserIdBySession(session);
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
            var userHandler = new UsersHandler();

            int userID = userHandler.UserIdBySession(session);
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
            var usersHandler = new UsersHandler();
            int userID = usersHandler.UserIdBySession(session);

            var resp = new FotoCommentsResponse();
            using (var context = new DataBaseContext())
            {
                Foto photoInf = context.Fotos.FirstOrDefault(x => x.FotoGUID == fotoGUID);
                if (photoInf == null)
                {
                    resp.Comment = DropFotoResponse.Errors.NOT_EXIST;
                }
                else
                {
                    bool friends = FriendshipChecker.IsUsersAreFriendsBySessionANDid(session, photoInf.UploadUserID);

                    if (photoInf.UploadUserID != userID && !friends)
                    {
                        resp.Comment = DropFotoResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        resp.isSuccess = true;
                        resp.PhotoDescription = photoInf.Description;
                        resp.PhotoTitle = photoInf.Title;
                        resp.PhotoDownloadLink = photoInf.Url;
                        resp.PhotoID = photoInf.FotoGUID;

                        var comments = context.FotoComments.Where(x => x.Foto.FotoGUID == photoInf.FotoGUID).OrderBy(x=>x.CommentWriteDate).ToList();
                        resp.FotoComments = new List<FotoCommentModel>();
                        comments.ForEach(comm => 
                        {
                            int? avatarID = context.Users.First(x => x.Id == comm.UserCommenter).AvatarId;
                            string avatarImgPath = context.Avatars.First(x => x.Id == avatarID).ImgPath;
                            var commModel = new FotoCommentModel()
                            {
                                UserPostedAvatarResizeUri = Resize.ResizedAvatarUri(avatarImgPath, ModTypes.c_scale, 50, 50),
                                PostDate = comm.CommentWriteDate.Ticks,
                                Text = comm.CommentText,
                                UserName = context.Users.First(x=>x.Id == comm.UserCommenter).UserFullInfo.Name,
                                UserSurName = context.Users.First(x => x.Id == comm.UserCommenter).UserFullInfo.SurName,
                                UserPostedID = comm.UserCommenter,
                                DateFormat = comm.CommentWriteDate.ToString("ddd d MMM HH:mm", CultureInfo.CreateSpecificCulture("ru-RU")),

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
            if (string.IsNullOrWhiteSpace(model.CommentText))
            {
                return new PostCommentsResponse()
                {
                    isSuccess = false,
                    Comment = PostCommentsResponse.Errors.EMPTY_VALUE
                };
            }
            var response = new PostCommentsResponse();
            var handler = new UsersHandler();

            int userId = handler.UserIdBySession(session);
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

                    if (foto.UploadUserID != userId && !friends)
                    {
                        response.Comment = DropAlbumResponse.Errors.NOT_ACCESS;
                    }
                    else
                    {
                        var comment = new FotoComment()
                        {
                             CommentWriteDate = DateTime.Now,
                             UserCommenter = userId,
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