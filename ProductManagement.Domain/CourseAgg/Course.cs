using System;
using System.Collections.Generic;
using _0_FrameWork.Domain;
using ShopManagement.Domain.CourseEpisodeAgg;
using ShopManagement.Domain.CourseGroupAgg;
using ShopManagement.Domain.CourseLevelAgg;
using ShopManagement.Domain.CourseStatusAgg;
using ShopManagement.Domain.OrderDetailAgg;
using ShopManagement.Domain.UserCoursesAgg;

namespace ShopManagement.Domain.CourseAgg
{
   public class Course:EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }
        public string File { get; private set; }
        public double Price { get; private set; }
        public string Code { get; private set; }
        public DateTime? UpdateDate { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string DemoVideoPoster { get; private set; }
        public string Slug { get; private set; }
        public long CourseGroupId { get; private set; }
        public long CourseLevelId { get; private set; }
        public long CourseStatusId { get; private set; }
        public long TeacherId { get;private set; }

        public CourseGroup CourseGroup { get; private set; }
        public List<OrderDetail> OrderDetails { get; private set; }
        public CourseLevel CourseLevel { get; private set; }
        public CourseStatus CourseStatus { get; private set; }
        public List<CourseEpisode> CourseEpisodes { get;private set; }
        public List<UserCourse> UserCourses { get; set; }

        public Course(string name, string description, string shortDescription, string file, double price, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug, string code, long courseGroupId, long courseLevelId, long courseStatusId, string demoVideoPoster, long teacherId)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;
            if(!string.IsNullOrWhiteSpace(file))
                File = file;
            Price = price;
            if(!string.IsNullOrWhiteSpace(picture))
               Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            Code = code;
            CourseGroupId = courseGroupId;
            CourseLevelId = courseLevelId;
            CourseStatusId = courseStatusId;
            if(!string.IsNullOrWhiteSpace(demoVideoPoster))
              DemoVideoPoster = demoVideoPoster;
            TeacherId = teacherId;
            UpdateDate = null;
        }

        public Course(string demoVideoPoster, long teacherId)
        {
            DemoVideoPoster = demoVideoPoster;
            TeacherId = teacherId;
        }
       
        public void Edit(string name, string description, string shortDescription, string file, double price, string picture, string pictureAlt, string pictureTitle, string keyWords, string metaDescription, string slug, string code, long courseGroupId, long courseLevelId, long courseStatusId, string demoVideoPoster,long teacherId)
        {
            Name = name;
            Description = description;
            ShortDescription = shortDescription;

            if (file!=null)
                File = file;

            if(!string.IsNullOrWhiteSpace(picture))
              Picture = picture;

            Price = price;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
            Code = code;
            CourseGroupId = courseGroupId;
            CourseLevelId = courseLevelId;
            CourseStatusId = courseStatusId;
            if (!string.IsNullOrWhiteSpace(picture))
                DemoVideoPoster = demoVideoPoster;
            UpdateDate=DateTime.Now;
            TeacherId = teacherId;
        }
    }
}
