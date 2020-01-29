using System;
using System.Threading.Tasks;
using FakeUsers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Slug.Startup))]

namespace Slug
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var bot = new CreateFakeUser();
            bot.Create(new Model.Registration.BaseRegistrationModel(), 10);
            app.MapSignalR();
        }
    }
}
