﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.Course;

namespace Shop.Management.Application.Contract.CoursePrerequisite
{
    public class CreateCoursePrerequisiteViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(1000, ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = Validate.Required)]
        public long CourseId { get; set; }
        public List<ArticleViewModel> SelectList { get; set; }
    }
}