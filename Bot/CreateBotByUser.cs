using Slug.Model.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    public class CreateBotByUser
    {
        public void Create(BaseRegistrationModel newUser)
        {
            var vkService = new VKServices.Auth(7170448, "ssuper10", "Quiputgbn12");
            var users = vkService.Search();
        }
    }
}
