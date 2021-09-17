using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
   public class TeacherViewModel
    {
        [MaxLength(250,ErrorMessage = Validate.MaxLength)]
        public string Skills { get;  set; }

        [MaxLength(500, ErrorMessage = Validate.MaxLength)]
        public string Bio { get;  set; }

        [MaxLength(1000, ErrorMessage = Validate.MaxLength)]
        public string Resumes { get;  set; }

        [Range(1,int.MaxValue,ErrorMessage = Validate.Required)]
        public long AccountId { get;  set; }
        public long Id { get; set; }
        public string AccountName { get; set; }
        public int Type { get; set; }
        public List<AccountViewModel> SelectList { get; set; }
    }

   public class EditTeacherViewModel : TeacherViewModel
    {
       
    }
}
