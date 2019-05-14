using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class FotoCommentModel
    {
        public string Text { get; set; }

        public DateTime PostDate { get; set; }

        public string UserPostedAvatarUri { get; set; }

        public int UserPostedID { get; set; }

        public string UserName { get; set; }

        public string UserSurName { get; set; }
    }
}