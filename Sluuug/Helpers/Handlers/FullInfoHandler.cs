using Context;
using Slug.Context;
using Slug.Context.Dto.UserFullInfo;
using Slug.Context.Tables;
using Slug.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Helpers
{
    public class FullInfoHandler
    {
        public User GetUserByInfoEnrtuGuid(Guid guid)
        {
            using (var context = new DataBaseContext())
            {
                var entryEducation = context.Educations
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryEducation != null)
                {
                    return entryEducation.User;
                }
               var entryWorkPlaces = context.WorkPlaces
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryWorkPlaces != null)
                {
                    return entryWorkPlaces.User;
                }
                var entryLifePlaces = context.LifePlaces
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryLifePlaces != null)
                {
                    return entryLifePlaces.User;
                }
                var entryMemorableEvents = context.MemorableEvents
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryMemorableEvents != null)
                {
                    return entryMemorableEvents.User;
                }

                return null;
            }
        }

        public void DropEntryByGuid(Guid guid)
        {
            using (var context = new DataBaseContext())
            {
                var entryEducation = context.Educations
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryEducation != null)
                {
                    context.Educations.Remove(entryEducation);
                }
                var entryWorkPlaces = context.WorkPlaces
                     .FirstOrDefault(x => x.EntryId == guid);
                if (entryWorkPlaces != null)
                {
                    context.WorkPlaces.Remove(entryWorkPlaces);
                }
                var entryLifePlaces = context.LifePlaces
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryLifePlaces != null)
                {
                    context.LifePlaces.Remove(entryLifePlaces);
                }
                var entryMemorableEvents = context.MemorableEvents
                    .FirstOrDefault(x => x.EntryId == guid);
                if (entryMemorableEvents != null)
                {
                    context.MemorableEvents.Remove(entryMemorableEvents);
                }

                context.SaveChanges();
            }
        }

        public bool AddEducationEntry(EducationModel model, string session)
        {
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                Education newEducation = new Education();
                newEducation.EntryId = Guid.NewGuid();
                newEducation.EducationType = model.EducationType;
                newEducation.Title = model.Title;
                newEducation.Start = model.Start;
                newEducation.CountryCode = int.Parse(model.Country);
                newEducation.SityCode = int.Parse(model.Sity);
                newEducation.Comment = model.Comment;
                newEducation.User = context.Users.First(x => x.Id == userID);

                if (model.UntilNow || model.End == null)
                {
                    newEducation.UntilNow = true;
                }
                else
                {
                    newEducation.End = model.End;
                }
                if (model.EducationType != EducationTypes.School)
                {
                    newEducation.Faculty = model.Faculty;
                    newEducation.Specialty = model.Specialty;
                }

                User user = context.Users.First(x => x.Id == userID);
                user.UserFullInfo.Educations.Add(newEducation);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddMemEventEntry(MemorableEventsModel model, string session)
        {
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                var newEducation = new MemorableEvents();
                newEducation.EntryId = Guid.NewGuid();
                newEducation.EventTitle = model.EventTitle;
                newEducation.DateEvent = model.DateEvent;
                newEducation.EventComment = model.EventComment;
                newEducation.User = context.Users.First(x => x.Id == userID);

                User user = context.Users.First(x => x.Id == userID);
                user.UserFullInfo.Events.Add(newEducation);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddLifePlacesEntry(LifePlacesModel model, string session)
        {
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                var lifePlace = new LifePlaces();
                lifePlace.EntryId = Guid.NewGuid();
                lifePlace.Start = model.Start;
                if (model.UntilNow == true || model.End == null)
                {
                    lifePlace.UntilNow = true;
                }
                else
                {
                    lifePlace.End = model.End;
                }

                lifePlace.Comment = model.Comment;
                lifePlace.CountryCode = int.Parse(model.Country);
                lifePlace.SityCode = int.Parse(model.Sity);
                lifePlace.User = context.Users.First(x => x.Id == userID);


                User user = context.Users.First(x => x.Id == userID);
                user.UserFullInfo.Places.Add(lifePlace);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddWorkPlacesEntry(WorkPlacesModel model, string session)
        {
            var handler = new UsersHandler();
            int userID = handler.GetFullUserInfo(session).UserId;

            using (var context = new DataBaseContext())
            {
                var workPlaces = new WorkPlaces();
                workPlaces.EntryId = Guid.NewGuid();
                workPlaces.Comment = model.Comment;
                workPlaces.CompanyTitle = model.CompanyTitle;
                workPlaces.CountryCode = int.Parse(model.Country);
                workPlaces.SityCode = int.Parse(model.Sity);
                workPlaces.Position = model.Position;
                workPlaces.Start = model.Start;
                if (model.UntilNow)
                {
                    workPlaces.UntilNow = true;
                }
                else
                {
                    workPlaces.End = model.End;
                }
                workPlaces.User = context.Users.First(x => x.Id == userID);

                User user = context.Users.First(x => x.Id == userID);
                user.UserFullInfo.Works.Add(workPlaces);
                context.SaveChanges();
                return true;
            }
        }


    }
}