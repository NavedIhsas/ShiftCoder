using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;

namespace ShiftCoderQuery.Contract.Account
{
   public class EditProfileQueryModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        public string FullName { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نمیباشد")]
        public string Email { get; set; }

        [MaxLength(20, ErrorMessage = Validate.MaxLength)]
        public string Phone { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        public string Password { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
        public long RoleId { get; set; }
        public long Id { get; set; }
        public string ActiveCode { get; set; }
        public string AvatarName { get; set; }
        public IFormFile Avatar { get; set; }
        public long? CityId { get; set; }
        public long ProvinceId { get; set; }
        public string Gander { get; set; }
        public string BirthDate { get; set; }
        public string AboutMe { get; set; }
        public List<ProvinceViewModel> ProvinceSelectList { get; set; }
        public List<CityViewModel> CitySelectList { get; set; }
        public UserInformationQueryModel UserInformation { get; set; }
    }

   public class UserInformationQueryModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skill { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string RoleName { get; set; }
        public string AvatarName { get; set; }
        public string Gander { get; set; }
        public string BirthDate { get; set; }
        public string AboutMe { get; set; }
        public long Id { get; set; }
       
    }

   public class ProvinceViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

   public class CityViewModel
   {
       public long Id { get; set; }
       public string Name { get; set; }
   }
}
