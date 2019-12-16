using System;
using Context;
using System.Linq;
using Slug.Context.Tables;
using Slug.Context.Dto.Posts;
using System.Collections.Generic;
using Slug.Helpers.BaseController;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using Slug.Model.FullInfo;

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

        public ProfilePostModel GetMorePosts(int userId, int currentPostsCount)
        {
            using (var context = new DataBaseContext())
            {
                return getMorePosts(userId, currentPostsCount, context);
            }
        }

        public ProfilePostModel GetMorePosts(int userId, int currentPostsCount, DataBaseContext context)
        {
            return getMorePosts(userId, currentPostsCount, context);
        }

        private ProfilePostModel getMorePosts(int userId, int currentPostsCount, DataBaseContext context)
        {
            var model = new ProfilePostModel();
            int oneTimePostsCountUpload = int.Parse(WebAppSettings.AppSettings[AppSettingsEnum.postsOnPage.ToString()]);
            int totalPostsCount = context.Posts.Count(x => x.UserPosted == userId);

            var oldPosts = context.Posts
                    .OrderByDescending(x => x.PublicDateTime)
                    .Where(x => x.UserPosted == userId)
                    .Skip(currentPostsCount)
                    .Take(oneTimePostsCountUpload)
                    .ToList();
            if (oldPosts.Count > 0)
            {
                model.Posts = new List<PostModel>();
                foreach (var post in oldPosts)
                {
                    model.Posts.Add(new PostModel()
                    {
                        PostTitle = post.Title,
                        PostText = post.Text,
                        PostedTime = post.PublicDateTime.ToString("dd.MM.yyyy HH:mm")
                    });
                }
            }
            model.TotalPostsCount = totalPostsCount;
            return model;
        }
    }
}