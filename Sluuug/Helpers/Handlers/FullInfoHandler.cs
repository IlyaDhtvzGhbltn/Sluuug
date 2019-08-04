using Context;
using Slug.Context;
using Slug.Context.Dto;
using Slug.Context.Dto.Albums;
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
                    .FirstOrDefault(x => x.Id == guid);
                if (entryEducation != null)
                {
                    return entryEducation.User;
                }
               var entryWorkPlaces = context.WorkPlaces
                    .FirstOrDefault(x => x.Id == guid);
                if (entryWorkPlaces != null)
                {
                    return entryWorkPlaces.User;
                }
                var entryMemorableEvents = context.ImportantEvents
                    .FirstOrDefault(x => x.Id == guid);
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
                    .FirstOrDefault(x => x.Id == guid);
                if (entryEducation != null)
                {
                    context.Educations.Remove(entryEducation);
                }
                var entryWorkPlaces = context.WorkPlaces
                     .FirstOrDefault(x => x.Id == guid);
                if (entryWorkPlaces != null)
                {
                    context.WorkPlaces.Remove(entryWorkPlaces);
                }

                var entryMemorableEvents = context.ImportantEvents
                    .FirstOrDefault(x => x.Id == guid);
                if (entryMemorableEvents != null)
                {
                    context.ImportantEvents.Remove(entryMemorableEvents);
                }

                context.SaveChanges();
            }
        }

        public bool AddEducationEntry(EducationModel model, string session)
        {
            var handler = new UsersHandler();
            int userId = handler.UserIdBySession(session);

            using (var context = new DataBaseContext())
            {
                Education newEducation = new Education();
                newEducation.Id = Guid.NewGuid();
                newEducation.EducationType = model.EducationType;
                newEducation.Title = model.Title;
                newEducation.Start = model.Start;
                newEducation.CountryCode = int.Parse(model.Country);
                newEducation.CityCode = int.Parse(model.City);
                newEducation.Comment = model.Comment;
                newEducation.User = context.Users.First(x => x.Id == userId);

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
                    //newEducation.Faculty = model.Faculty;
                    newEducation.Specialty = model.Specialty;
                }

                User user = context.Users.First(x => x.Id == userId);
                user.UserFullInfo.Educations.Add(newEducation);
                context.SaveChanges();
                return true;
            }
        }

        public bool AddMemEventEntry(MemorableEventsModel model, string session, IEnumerable<HttpPostedFileBase> uploadFiles)
        {
            var handler = new UsersHandler();
            int userId = handler.UserIdBySession(session);
            if (userId > 0)
            {
                using (var context = new DataBaseContext())
                {
                    var albumHandler = new AlbumsHandler();
                    CreateAlbumResponse resp = albumHandler.CreateEventsAlbum(model.EventTitle, userId);

                    if (uploadFiles.Count() > 0)
                    {
                        albumHandler.UploadToAlbum(session, resp.AlbumId, uploadFiles, "/users/events_albums/");
                    }

                    var newEvent = new ImportantEvent();
                    newEvent.Id = Guid.NewGuid();
                    newEvent.EventTitle = model.EventTitle;
                    newEvent.DateEvent = model.DateEvent;
                    newEvent.TextEventDescription = model.Text;
                    newEvent.User = context.Users.First(x => x.Id == userId);
                    newEvent.AlbumGuid = resp.AlbumId;

                    User user = context.Users.First(x => x.Id == userId);
                    user.UserFullInfo.Events.Add(newEvent);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }      

        public bool AddWorkPlacesEntry(WorkPlacesModel model, string session)
        {
            var handler = new UsersHandler();
            int userId = handler.UserIdBySession(session);

            using (var context = new DataBaseContext())
            {
                var workPlaces = new WorkPlaces();
                workPlaces.Id = Guid.NewGuid();
                workPlaces.Comment = model.Comment;
                workPlaces.CompanyTitle = model.CompanyTitle;
                workPlaces.CountryCode = int.Parse(model.Country);
                workPlaces.CityCode = int.Parse(model.City);
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
                workPlaces.User = context.Users.First(x => x.Id == userId);

                User user = context.Users.First(x => x.Id == userId);
                user.UserFullInfo.Works.Add(workPlaces);
                context.SaveChanges();
                return true;
            }
        }
    }
}