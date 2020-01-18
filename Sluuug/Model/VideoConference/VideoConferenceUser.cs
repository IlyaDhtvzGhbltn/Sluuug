using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.VideoConference
{
    public class VideoConferenceUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IsOnline { get; set; }
        public bool IsVip { get; set; }
        public bool AvaliableVipContact { get; set; }
    }
}