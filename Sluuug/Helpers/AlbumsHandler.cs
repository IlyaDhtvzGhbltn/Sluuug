using Context;
using Slug.Context.Dto.Albums;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Tables;
using Slug.ImageEdit;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class AlbumsHandler
    {
        public CreateAlbumResponse CreateAlbum(string session, AlbumModel model, HttpPostedFileBase albumLabel)
        {
            Guid albumGUID = Guid.NewGuid();
            var handler = new UsersHandler();
            var userUploader = handler.GetFullUserInfo(session);

            string labelUri = "https://res.cloudinary.com/dlk1sqmj4/image/upload/v1557838909/system/template.jpg";
            if(albumLabel != null )
            {
                CloudImageUploadResult upl = SlugController.UploadImg(albumLabel, "/users/albums/" + albumGUID.ToString());
                labelUri = upl.SecureUrl.ToString();
            }
            try
            {
                using (var context = new DataBaseContext())
                {
                    var album = new Album()
                    {
                        Id = albumGUID,
                        AlbumLabelUrl = labelUri,
                        AuthorComment = model.AuthorComment,
                        CreateUserID = userUploader.UserId,
                        CreationDate = DateTime.Now,
                        Title = model.Title,
                        User = context.Users.First(x => x.Id == userUploader.UserId)
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
                                    AuthorComment = model.AuthorComment,
                                    UploadDate = DateTime.Now,
                                    UploadUserID = userUploaderID,
                                    Url = uploadCloud.SecureUrl.ToString()
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
                    if (album.CreateUserID != getUserId)
                    {
                        resp.Comment = AlbumPhotosResponse.Errors.NOT_ACCESS;
                        return resp;
                    }
                    else
                    {
                       resp.Photos = new List<FotoModel>();
                       album.Fotos.ToList().ForEach(foto => 
                       {
                           var fModel = new FotoModel()
                           {
                               Album = foto.AlbumID,
                               SmallFotoUri = Resize.ResizedUri(foto.Url, ModTypes.c_scale , 50),
                               FullFotoUri = foto.Url,
                               AuthorComment = foto.AuthorComment,
                               Title = foto.Title,
                               UploadDate = foto.UploadDate, 
                           };
                           resp.Photos.Add(fModel);
                       });

                        resp.isSucces = true;
                        resp.Count = album.Fotos.Count;
                        return resp; 
                    }
                }
            }
        }
    }
}