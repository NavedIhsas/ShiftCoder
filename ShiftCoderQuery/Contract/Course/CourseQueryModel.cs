using System;
using System.Collections.Generic;
using ShiftCoderQuery.Contract.Comment;
using Shop.Management.Application.Contract.AfterCourse;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.CoursePrerequisite;
using Shop.Management.Application.Contract.CourseSuitable;

namespace ShiftCoderQuery.Contract.Course
{
   public class CourseQueryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string File { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public string UpdateDate { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string PosterImg { get; set; }
        public string CourseGroup { get; set; }
        public long CourseGroupId { get; set; }
        public long Id { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string CourseLevel { get; set; }
        public int CommentCount { get; set; }
        public string CourseStatus { get; set; }
        public List<CourseSuitableViewModel> SuitableCourse { get; set; }
        public List<AfterCourseViewModel> AfterCourse { get; set; }
        public List<CoursePrerequisiteViewModel> PrerequisiteCourse { get; set; }
        public  List<CourseEpisodeViewModel> EpisodeCourse { get; set; }
        public List<ShopManagement.Domain.CourseGroupAgg.CourseGroup> CourseGroups { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public List<CommentQueryModel> SubComments { get; set; }
      
        public DateTime CreationDate { get; internal set; }
        public int EpisodeCount { get; internal set; }
    }
}
