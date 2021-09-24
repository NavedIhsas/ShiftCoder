using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;
using Shop.Management.Application.Contract.Course;

namespace Shop.Management.Application.Contract.CourseEpisode
{
    public class CreateCourseEpisodeViewModel
    {
        public IFormFile File { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(1000,ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }
        public bool IsFree { get; set; }

        [Range(0,long.MaxValue,ErrorMessage = Validate.Required)]
        public long CourseId { get; set; }
        public List<CourseViewModel> CoursesSelectList { get; set; }

    }
}