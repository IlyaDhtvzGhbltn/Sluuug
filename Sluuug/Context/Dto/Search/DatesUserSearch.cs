using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class DatesUserSearch
    {
        public DateTime UserMinDateOfBirth { get; set; }

        public DateTime UserMaxDateOfBirth { get; set; }

    }
}