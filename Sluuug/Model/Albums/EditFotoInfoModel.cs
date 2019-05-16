using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Model.Albums
{
    public class EditFotoInfoModel
    {
        public Guid PhotoGUID { get; set; }

        public string NewValue { get; set; }

        public EditMode EditMode { get; set; }
    }
}