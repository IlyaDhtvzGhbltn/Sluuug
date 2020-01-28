using Slug.Model.Registration;
using RemoteServices;
using System.Collections.Generic;
using System;

namespace Bot
{
    public class CreateBotByUser
    {
        Dictionary<int, int> SexFakeUser = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 0, 1 }
        };

        public void Create(BaseRegistrationModel newUser, uint offset)
        {

            var vkService = new VKFakeUserService(7170448, "ssuper10", "Quiputgbn12");
            var users = vkService.Search(offset);
        }

        Tuple<ushort, ushort> calculateAgeFake(int years)
        {
            if (years >= 16 && years <= 21)
                return new Tuple<ushort, ushort>(1,1);

            return new Tuple<ushort, ushort>(1, 1);
        }
    }
}
