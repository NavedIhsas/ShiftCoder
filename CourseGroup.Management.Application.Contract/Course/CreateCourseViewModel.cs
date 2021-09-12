using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseGroup;
using Shop.Management.Application.Contract.CourseLevel;
using Shop.Management.Application.Contract.CourseStatus;
using AccountManagement.Application.Contract.Account;

namespace Shop.Management.Application.Contract.Course
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(200,ErrorMessage = Validate.MaxLength)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(1000, ErrorMessage = Validate.MaxLength)]
        public string ShortDescription { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public double Price { get; set; }

        public IFormFile DemoPoster { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(20, ErrorMessage = Validate.MaxLength)]
        public string Code { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureName { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(150, ErrorMessage = Validate.MaxLength)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(200, ErrorMessage = Validate.MaxLength)]
        public string Slug { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = Validate.Required)]
        public long CourseGroupId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public long CourseLevelId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public long CourseStatusId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public long TeacherId { get; set; }
        public List<CourseGroupViewModel> CourseGroupSelectList { get; set; }
        public List<CourseStatusViewModel> CourseStatusSelectList { get; set; }
        public List<CourseLevelViewModel> CourseLevelSelectList { get; set; }
        public List<TeacherViewModel> TeacherSelectList { get; set; }


    }
}
