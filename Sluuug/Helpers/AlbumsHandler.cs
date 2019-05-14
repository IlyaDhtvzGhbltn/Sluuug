using Context;
using Slug.Context.Dto.Albums;
using Slug.Context.Dto.UserWorker;
using Slug.Context.Tables;
using Slug.Model.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class AlbumsHandler
    {
        public CreateAlbumRespose CreateAlbum(string session, AlbumModel model, HttpPostedFileBase albumLabel)
        {
            Guid albumGUID = Guid.NewGuid();
            Context.UsersHandler handler = new Context.UsersHandler();
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
                    return new CreateAlbumRespose() { isSuccess = true };
                }
            }
            catch (Exception ex)
            {
                return new CreateAlbumRespose() { isSuccess = false, Comment = ex.Message };
            }
        }
    }
}