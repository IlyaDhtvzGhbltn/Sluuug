using CloudinaryDotNet;
using Slug.Context;
using Slug.Context.Dto.UserWorker;
using Slug.Helpers.BaseController;
using System.Web;
using System.Web.Mvc;
using WebAppSettings = System.Web.Configuration.WebConfigurationManager;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System.IO;
using Slug.Context.Dto.Cloudinary;

namespace Slug.Helpers
{
    public class SlugController : Controller
    {
        public SessionsHandler SessionHandler { get; set; } = new SessionsHandler();
        public ActivationHandler ActivationMailHandler { get; set; } = new ActivationHandler();
        public UsersHandler UsersHandler { get; set; } = new UsersHandler();
        public ConversationHandler ConversationHandler { get; set; } = new ConversationHandler();
        public UsersDialogHandler DialogsHandler { get; set; } = new UsersDialogHandler();
        public CryptoChatHandler CryptoChatHandler { get; set; } = new CryptoChatHandler();
        public VideoConferenceHandler VideoConferenceHandler { get; set; } = new VideoConferenceHandler();
        public SearchHandler SearchHandler { get; set; } = new SearchHandler();
        public FullInfoHandler FullInfoHandler { get; set; } = new FullInfoHandler();
        public AlbumsHandler AlbumsHandler { get; set; } = new AlbumsHandler();

        public static CloudImageUploadResult UploadImg(HttpPostedFileBase upload, string UploadFolder = "/users/avatars")
        {
            string fileName = System.IO.Path.GetFileName(upload.FileName);
            Account account = new Account(
              WebAppSettings.AppSettings[AppSettingsEnum.cloud.ToString()],
              WebAppSettings.AppSettings[AppSettingsEnum.apiKey.ToString()],
              WebAppSettings.AppSettings[AppSettingsEnum.apiSecret.ToString()]);

            Cloudinary cloudinary = new Cloudinary(account);
            FileDescription desc = new FileDescription(fileName, upload.InputStream);
            var uploadParams = new ImageUploadParams()
            {
                Folder = UploadFolder,
                File = desc,               
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            CloudImageUploadResult result = JsonConvert.DeserializeObject<CloudImageUploadResult>(uploadResult.JsonObj.Root.ToString());
            return result;
        }

        public static string GetCookiesValue(HttpRequestBase request)
        {
           string session = request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]].Value;
           return session;
        }
    }
}