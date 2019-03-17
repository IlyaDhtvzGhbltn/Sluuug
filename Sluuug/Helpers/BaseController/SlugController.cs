using Slug.Context;
using System.Web.Mvc;


namespace Slug.Helpers
{
    public class SlugController : Controller
    {
        public SessionWorker SessionWorker { get; set; } = new SessionWorker();
        public ActivationMailWorker ActivationMailWorker { get; set; } = new ActivationMailWorker();
        public UserWorker UserWorker { get; set; } = new UserWorker();
        public ConversationWorker ConverWorker { get; set; } = new ConversationWorker();
        public DialogWorker DialogWorker { get; set; } = new DialogWorker();
    }
}