using RemoteServices;
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
        Dictionary<SexEnum, int> SexFakeUser = new Dictionary<SexEnum, int>()
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
                    ageFrom = calculateAgeFakeForMale(real.DateBirth.Year).Item1;
                    ageTo = calculateAgeFakeForMale(real.DateBirth.Year).Item2;
                }
                if (real.Sex == 1)
                {
                    ageFrom = calculateAgeFakeForFemale(real.DateBirth.FullYearsElapsed()).Item1;
                    ageTo = calculateAgeFakeForFemale(real.DateBirth.FullYearsElapsed()).Item2;
                }

                List<FakeUserModel> users = vkService.Search(
                    SexFakeUser[(SexEnum)real.Sex],
                    ageFrom, 
                    ageTo, 
                    real.CityCode, 
                    real.CountryCode, 
                    real.CityTitle,
                    real.CountryTitle,
                    offset);
                users.ForEach(user => 
                {
                    user.userSearchAge = calculateFakeUserSearchAge(real.DateBirth);
                    user.userSearchSex = (SexEnum)real.Sex;
                    user.purpose = calculateFakeUserDatingPurpose(real.DateBirth, (SexEnum)real.Sex);
                });
                users[0].Vip = true;
                if (users.Count >= 3)
                {
                    users[1].Vip = true;
                    users[2].Vip = true;
                }
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
            else return new Tuple<ushort, ushort>(18, 35);
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
            else return new Tuple<ushort, ushort>(27, 37);
        }

        AgeEnum calculateFakeUserSearchAge(DateTime realUserBirthDate)
        {
            int realUserAge = realUserBirthDate.FullYearsElapsed();
            if (realUserAge >= 0 && realUserAge <= 20)
                return AgeEnum.from16to20;
            if (realUserAge >= 21 && realUserAge <= 26)
                return AgeEnum.from21to26;
            if (realUserAge >= 27 && realUserAge <= 32)
                return AgeEnum.from27to32;
            if (realUserAge >= 33 && realUserAge <= 40)
                return AgeEnum.from33to40;
            if (realUserAge >= 41 && realUserAge <= 49)
                return AgeEnum.from41to49;
            if (realUserAge >= 50 && realUserAge <= 59)
                return AgeEnum.from50to59;
            if (realUserAge >= 60 && realUserAge <= 69)
                return AgeEnum.from60to69;
            else
                return AgeEnum.from21to26;
        }

        DatingPurposeEnum calculateFakeUserDatingPurpose(DateTime realUserBirthDate, SexEnum realSex)
        {
            int realUserAge = realUserBirthDate.FullYearsElapsed();
            if (realSex == SexEnum.man)
            {
                if (realUserAge >= 0 && realUserAge <= 22)
                    return DatingPurposeEnum.Sex;
                if (realUserAge >= 23 && realUserAge <= 28)
                    return DatingPurposeEnum.SeriousRelationship;
                if (realUserAge >= 29 && realUserAge <= 50)
                    return DatingPurposeEnum.Sex;
                else return DatingPurposeEnum.Communication;
            }
            else
            {
                return DatingPurposeEnum.SeriousRelationship;
            }
        }
    }
}
