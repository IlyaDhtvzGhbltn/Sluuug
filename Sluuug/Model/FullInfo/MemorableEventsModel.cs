using Slug.Context.Dto.UserFullInfo;
using Slug.Model.Albums;
using Slug.Model.FullInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Users
{
    public class MemorableEventsModel
    {
        public Guid Id { get; set; }

        public Guid BindedAlbumId { get; set; }

        public int UserId { get; set; }

        public string EventTitle { get; set; }

        public string Text { get; set; }

        public string DateEventFormat { get; set; }

        public DateTime DateEvent { get; set; }

        public List<FotoModel> Photos { get; set; }

    }
}