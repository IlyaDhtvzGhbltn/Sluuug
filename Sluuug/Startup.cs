using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Slug.Startup))]

namespace Slug
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
