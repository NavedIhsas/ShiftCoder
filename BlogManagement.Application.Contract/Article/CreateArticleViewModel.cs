﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.Article
{
    public class CreateArticleViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string Description { get; set; }

        [MaxLength(1000,ErrorMessage = Validate.MaxLength)]
        public string ShortDescription { get; set; }
        public int ShowOrder { get; set; }

        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string PictureAtl { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        public string Slug { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string MetaDescription { get; set; }

        [MaxLength(150, ErrorMessage = Validate.MaxLength)]
        public string CanonicalAddress { get; set; }
        public DateTime? PublishDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = Validate.Required)]
        public long CategoryId { get; set; }

        public bool IsPublish { get; set; }
        public List<ArticleCategoryViewModel> SelectList { get; set; }
    }
}