using Slug.Model.Registration;
using RemoteServices;
using System.Collections.Generic;
using System;
using SharedModels.Users;

namespace FakeUsers
{
    public class CreateFakeUser
    {
        Dictionary<int, int> SexFakeUser = new Dictionary<int, int>()
        {
            { 1, 2 },
            { 2, 1 }
        };

        public void Create(BaseRegistrationModel real, uint offset)
        {
            if (offset <= 990)
            {
                var vkService = new FakeUsersService(7170448, "ssuper10", "Quiputgbn12");
                int ageFrom = 0;
                int ageTo = 0;
                if (real.Sex == 0)
                {
                    ageFrom = calculateAgeFakeForMale(real.DateBirth.Year).Item1;
                    ageTo = calculateAgeFakeForMale(real.DateBirth.Year).Item2;
                }
                if (real.Sex == 1)
                {
                    ageFrom = calculateAgeFakeForFemale(real.DateBirth.Year).Item1;
                    ageTo = calculateAgeFakeForFemale(real.DateBirth.Year).Item2;
                }
                List<BaseUser> users = vkService.Search(SexFakeUser[real.Sex], ageFrom, ageTo, 495, 7, offset);
            }
        }

        Tuple<ushort, ushort> calculateAgeFakeForMale(int years)
        {
            if (years >= 16 && years <= 21)
                return new Tuple<ushort, ushort>(16, 55);
            if (years >= 22 && years <= 33)
                return new Tuple<ushort, ushort>(18, 33);
            if (years >= 34 && years <= 40)
                return new Tuple<ushort, ushort>(25, 38);
            if (years >= 41 && years <= 50)
                return new Tuple<ushort, ushort>(35, 45);
            if (years >= 51 && years <= 60)
                return new Tuple<ushort, ushort>(45, 50);
            else return new Tuple<ushort, ushort>(18, 35);
        }

        Tuple<ushort, ushort> calculateAgeFakeForFemale(int years)
        {
            if (years >= 16 && years <= 21)
                return new Tuple<ushort, ushort>(18, 25);
            if (years >= 22 && years <= 33)
                return new Tuple<ushort, ushort>(22, 30);
            if (years >= 34 && years <= 40)
                return new Tuple<ushort, ushort>(35, 45);
            if (years >= 41 && years <= 50)
                return new Tuple<ushort, ushort>(40, 50);
            if (years >= 51 && years <= 60)
                return new Tuple<ushort, ushort>(50, 60);
            else return new Tuple<ushort, ushort>(27, 37);
        }
    }
}
