﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contract.Account
{
    public class RegisterUserViewModel
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

        public IFormFile Avatar { get; set; }
        public long RoleId { get; set; }
        public List<RoleViewModel> SelectList { get; set; }
        public TeacherViewModel Teacher { get; set; }
        public long Id { get; set; }
        public string ActiveCode { get; set; }
        public string AvatarName { get; set; }
    }
}