using Context;
using Slug.Context.Dto.Posts;
using Slug.Context.Tables;
using System;

namespace Slug.Helpers.Handlers.PrivateUserServices
{
    public class PostUserHandler
    {
        public bool SavePost(NewPostRequestModel request, int userId)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                PublicDateTime = DateTime.UtcNow,
                Text = request.Text,
                Title = request.Title,
                UserPosted = userId
            };
            using (var context = new DataBaseContext())
            {
                context.Posts.Add(post);
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}