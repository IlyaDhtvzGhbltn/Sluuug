using Context;
using Slug.Context.Tables;
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
    }
}