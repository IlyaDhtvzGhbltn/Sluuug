using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Search
{
    public class SearchCitiesModel
    {
        public ICollection<SearchCityItem> Cities { get; set; }
    }
}