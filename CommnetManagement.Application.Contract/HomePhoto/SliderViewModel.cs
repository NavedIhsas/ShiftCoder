using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;

namespace CommentManagement.Application.Contract.HomePhoto
{
   public class SliderViewModel
    {
        public IFormFile Picture { get; set; }
        public string PictureStringFormat { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(150,ErrorMessage = Validate.MaxLength)]
        public string PictureAlt { get; set; }
        
        [MaxLength(150, ErrorMessage = Validate.MaxLength)]
        [Required(ErrorMessage = Validate.Required)]
        public string PictureTitle { get; set; }
      
        [MaxLength(20,ErrorMessage = Validate.MaxLength)]
        public string ButtonText { get; set; }
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }

        [MaxLength(70, ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }

        [MaxLength(50, ErrorMessage = Validate.MaxLength)]
        public string ShortTitle { get; set; }

        [MaxLength(500, ErrorMessage = Validate.MaxLength)]
        public string ButtonLink { get; set; }

    }
}
