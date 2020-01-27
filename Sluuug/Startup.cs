using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Slug.Startup))]

namespace Slug
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var bot = new Bot.CreateBotByUser();
            bot.Create(new Model.Registration.BaseRegistrationModel());
            app.MapSignalR();
        }
    }
}
