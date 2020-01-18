using Slug.Context.Dto.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slug.Model.Registration
{
    public class OutRegisteringUserModel : BaseRegistrationModel
    {
        public long OutId { get; set; }
        public string Avatar200 { get; set; }
        public string Avatar100 { get; set; }
        public string Avatar50 { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Status { get; set; }

        public int? ReferalUserId { get; set; }
    }
}