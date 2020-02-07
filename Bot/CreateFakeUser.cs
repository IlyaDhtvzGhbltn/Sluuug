﻿using RemoteServices;
using System.Collections.Generic;
using System;
using SharedModels.Users;
using VKServices.LocationAdapter;
using SharedModels.Enums;
using Slug.Extension;
using System.Linq;
using SharedModels.Users.Registration;
using System.Threading.Tasks;

namespace FakeUsers
{
    public class CreateFakeUser
    {
        Random rnd = new Random();

        Dictionary<SexEnum, int> VkSexFakeUser = new Dictionary<SexEnum, int>()
        {
            { SexEnum.woman, 2 },
            { SexEnum.man, 1 }
        };

        public List<FakeUserModel> GetFakeUsersFromVk(BaseRegistrationModel real, uint offset)
        {
            if (offset <= 990)
            {
                var vkService = new RemoteUsersService(7170448, "ssuper10", "Quiputgbn12");
                int ageFrom = 0;
                int ageTo = 0;
                if (real.Sex == 0)
                {
                    ageFrom = calculateAgeFakeForMale(real.DateBirth.FullYearsElapsed()).Item1;
                    ageTo = calculateAgeFakeForMale(real.DateBirth.FullYearsElapsed()).Item2;
                }
                if (real.Sex == 1)
                {
                    ageFrom = calculateAgeFakeForFemale(real.DateBirth.FullYearsElapsed()).Item1;
                    ageTo = calculateAgeFakeForFemale(real.DateBirth.FullYearsElapsed()).Item2;
                }

                var vkAdapter = new CityAdapter();
                int vkCityCode = (int)vkAdapter.GetCityId(
                    vkService.service, 
                    real.CountryCode, 
                    real.CityTitle);

                List <FakeUserModel> users = vkService.Search(
                    VkSexFakeUser[(SexEnum)real.Sex],
                    ageFrom, 
                    ageTo,
                    vkCityCode,
                    vkAdapter.GetCountryId(real.CountryCode), 
                    real.CityTitle,
                    real.CountryTitle,
                    offset);
                users.ForEach(user => 
                {
                    user.userSearchAge = calculateFakeUserSearchAge(real.DateBirth);
                    user.userSearchSex = (SexEnum)real.Sex;
                    user.purpose = calculateFakeUserDatingPurpose(real.DateBirth, (SexEnum)real.Sex);
                });
                users.ForEach(user => 
                {
                    if (user.purpose == DatingPurposeEnum.Sex)
                        user.Vip = true;
                });

                return users;
            }
            return null;
        }

        Tuple<ushort, ushort> calculateAgeFakeForMale(int years)
        {
            if (years >= 0 && years <= 21)
                return new Tuple<ushort, ushort>(16, 55);
            if (years >= 22 && years <= 33)
                return new Tuple<ushort, ushort>(18, 33);
            if (years >= 34 && years <= 40)
                return new Tuple<ushort, ushort>(25, 38);
            if (years >= 41 && years <= 50)
                return new Tuple<ushort, ushort>(35, 45);
            if (years >= 51 && years <= 60)
                return new Tuple<ushort, ushort>(45, 50);
            else return new Tuple<ushort, ushort>(18, 55);
        }

        Tuple<ushort, ushort> calculateAgeFakeForFemale(int years)
        {
            if (years >= 0 && years <= 21)
                return new Tuple<ushort, ushort>(18, 25);
            if (years >= 22 && years <= 33)
                return new Tuple<ushort, ushort>(22, 30);
            if (years >= 34 && years <= 40)
                return new Tuple<ushort, ushort>(35, 45);
            if (years >= 41 && years <= 50)
                return new Tuple<ushort, ushort>(40, 50);
            if (years >= 51 && years <= 60)
                return new Tuple<ushort, ushort>(50, 60);
            else return new Tuple<ushort, ushort>(40, 80);
        }

        AgeEnum calculateFakeUserSearchAge(DateTime realUserBirthDate)
        {
            int realUserAge = realUserBirthDate.FullYearsElapsed();
            if (realUserAge >= 0 && realUserAge <= 26)
                return (AgeEnum)rnd.Next(0, 4);
            if (realUserAge >= 27 && realUserAge <= 40)
                return (AgeEnum)rnd.Next(1, 5);
            if (realUserAge >= 41 && realUserAge <= 49)
                return (AgeEnum)rnd.Next(3, 5);
            if (realUserAge >= 50 && realUserAge <= 59)
                return (AgeEnum)rnd.Next(4, 6);
            if (realUserAge >= 60 && realUserAge <= 69)
                return (AgeEnum)rnd.Next(6, 8);
            else
                return (AgeEnum)rnd.Next(5, 8);
        }

        DatingPurposeEnum calculateFakeUserDatingPurpose(DateTime realUserBirthDate, SexEnum realSex)
        {
            int purpose = rnd.Next(0, 3);
            return (DatingPurposeEnum)purpose;
        }
    }
}
