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
using System.Text.RegularExpressions;
using Slug.Helpers.Handlers.PrivateUserServices;

namespace Slug.Helpers
{
    public class SlugController : Controller
    {
        public static Regex ValidateSymbols = new Regex(@"'");

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
        public PostUserHandler PostUserHandler { get; set; } = new PostUserHandler();

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
            HttpCookie session = request.Cookies[WebAppSettings.AppSettings[AppSettingsEnum.appSession.ToString()]];
            if (session != null)
            {
                return session.Value;
            }
           return null;
        }

        public static string GetIPAddress(HttpRequest Request)
        {
            if (Request.Headers["CF-CONNECTING-IP"] != null) return Request.Headers["CF-CONNECTING-IP"].ToString();

            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    string[] addresses = ipAddress.Split(',');
                    if (addresses.Length != 0)
                    {
                        return addresses[0];
                    }
                }
            }

            return Request.UserHostAddress;
        }
    }
}