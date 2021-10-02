using System;
using System.Collections.Generic;
using ShiftCoderQuery.Contract.Comment;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.UserCourse;
using ShopManagement.Domain.OrderDetailAgg;

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
        public string CourseGroupSlug { get; set; }
        public string PictureTitle { get; set; }
        public List<string> Keywords { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string PosterImg { get; set; }
        public string CourseGroup { get; set; }
        public string TeacherName { get; set; }
        public string TeacherBio { get; set; }
        public string TeacherResume { get; set; }
        public string TeacherSkills { get; set; }
        public string TeacherAvatar { get; set; }

        // ToDo check this for: reference form here to domain
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> CommentList { get; set; }

        public List<UserCourseViewModel> UserCourse { get; set; }
        public List<CourseQueryModel> CourseTeacher { get; set; }
        public long CourseGroupId { get; set; }
        public long Id { get; set; }
        public string CourseLevel { get; set; }
        public int? VisitCount { get; set; }
        public string CourseStatus { get; set; }
        public List<CourseEpisodeViewModel> EpisodeCourse { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public DateTime CreationDate { get; internal set; }
        public int EpisodeCount { get; internal set; }
        public long TeacherId { get; internal set; }
    }

    public class GetCourseGroupViewModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Slug { get; set; }
        public double Price { get; set; }
        public TimeSpan TotalTime { get; set; }
        public List<UserCourseViewModel> UserCourse { get; set; }
        public long Id { get; internal set; }
    }

    public class LatestCourseViewModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Slug { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public TimeSpan TotalTime { get; set; }
        public List<UserCourseViewModel> UserCourse { get; set; }
    }

    public class GetPopularCourseViewModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Slug { get; set; }
        public double Price { get; set; }
        public TimeSpan TotalTime { get; set; }
        public List<UserCourseViewModel> UserCourse { get; set; }
        public long Id { get; internal set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class GetAllCourseQueryModel
    {
        public string Name { get; set; }
        public long Id { get; set; }
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
        public List<string> Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string CourseGroup { get; set; }
        public long CourseGroupId { get; set; }
        public long? SubGroupId { get; set; }

        public string TeacherName { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public TimeSpan TotalTime { get; set; }
        public DateTime CreationDate { get; internal set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<UserCourseViewModel> UserCourse { get; set; }
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> Comments { get; set; }
        public long TeacherId { get; internal set; }
        public List<GetAllCourseQueryModel> Courses { get; set; }

        public List<bool> Visits { get; set; }
    }

    public class VisitViewModel
    {
        public int Type { get; set; }
        public long OwnerId { get; set; }
        public int NumberOfVisit { get; set; }
        public long Id { get; set; }
        public List<GetAllCourseQueryModel> Courses { get; set; }
    }

    public class CoursePaginationViewModel
    {
        public CoursePaginationViewModel()
        {
            Courses = new List<GetAllCourseQueryModel>();
        }

        public List<GetAllCourseQueryModel> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}