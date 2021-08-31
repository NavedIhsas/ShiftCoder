using System;

namespace ShiftCoderQuery.Contract.CourseGroup
{
   public class CourseGroupQueryModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public long? SubGroupId { get; set; }
        public string SubGroup { get; set; }
        public long Id { get; set; }
        public DateTime CreationDate { get; internal set; }
    }

   public class CourseGroupSearchQuery
    {
        public string Title { get; set; }
        public long Id { get; set; }

    }
}
