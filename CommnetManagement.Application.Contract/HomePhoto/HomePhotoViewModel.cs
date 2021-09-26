using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;

namespace CommentManagement.Application.Contract.HomePhoto
{
   public class HomePhotoViewModel
    {
        public IFormFile Picture { get; set; }
        public string PictureStringFormat { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(300,ErrorMessage = Validate.MaxLength)]
        public string PictureAlt { get; set; }
        
        [MaxLength(300,ErrorMessage = Validate.MaxLength)]
        [Required(ErrorMessage = Validate.Required)]
        public string PictureTitle { get; set; }
      
        [MaxLength(300,ErrorMessage = Validate.MaxLength)]
        public string ButtonsColor { get; set; }
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
    }

   public interface IHomePhotoApplication
   {
       OperationResult Create(HomePhotoViewModel command);
       OperationResult Edit(HomePhotoViewModel command);
       List<HomePhotoViewModel> GetAllList();
       HomePhotoViewModel GetDetails(long id);
   }
}
