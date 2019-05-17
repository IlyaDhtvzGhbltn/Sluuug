using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Slug.Context.Dto.Cloudinary
{
    public class Deleted
    {
        public string __invalid_name__ { get; set; }
    }

    public class __invalid_type__
    {
        public int original { get; set; }
        public int derived { get; set; }
    }

    public class DeletedCounts
    {
        public __invalid_type__ __invalid_name__ { get; set; }
    }

    public class CloudDeleteImage
    {
        public Deleted deleted { get; set; }
        public DeletedCounts deleted_counts { get; set; }
        public bool partial { get; set; }
    }
}