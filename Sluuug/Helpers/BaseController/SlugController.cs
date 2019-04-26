using Slug.Context;
using System.Web.Mvc;


namespace Slug.Helpers
{
    public class SlugController : Controller
    {
        public SessionsHandler SessionWorker { get; set; } = new SessionsHandler();
        public ActivationHandler ActivationMailWorker { get; set; } = new ActivationHandler();
        public UsersHandler UserWorker { get; set; } = new UsersHandler();
        public ConversationHandler ConverWorker { get; set; } = new ConversationHandler();
        public UsersDialogHandler DialogWorker { get; set; } = new UsersDialogHandler();
        public CryptoChatHandler CryptoChatWorker { get; set; } = new CryptoChatHandler();
        public VideoConferenceWorker VideoConferenceWorker { get; set; } = new VideoConferenceWorker();
    }
}