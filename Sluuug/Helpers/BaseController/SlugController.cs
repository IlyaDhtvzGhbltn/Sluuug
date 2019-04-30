using Slug.Context;
using System.Web.Mvc;


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
    }
}