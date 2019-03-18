using Slug.Context;
using System.Web.Mvc;


namespace Slug.Helpers
{
    public class SlugController : Controller
    {
        public SessionWorker SessionWorker { get; set; }
        public ActivationMailWorker ActivationMailWorker { get; set; }
        public UserWorker UserWorker { get; set; }

        public SlugController()
        {
            SessionWorker = new SessionWorker();
            ActivationMailWorker = new ActivationMailWorker();
            UserWorker = new UserWorker();
        }
    }
}