using System;
using System.Collections.Generic;

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
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public DateTime CreationDate { get; internal set; }
        public List<SubCourseGroupViewModel> SubGroups { get; set; }
        public int CourseCount { get; internal set; }
    }

   public class LatestCourseGroupViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int CourseCount { get; set; }
    }

    public class SubCourseGroupViewModel
    {
        public long Id { get; set; }
    }

    public class CourseGroupSearchQuery
    {
        public string Title { get; set; }
        public long Id { get; set; }

    }
}
