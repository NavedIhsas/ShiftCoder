using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace AccountManagement.Application.Contract.Role
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100,ErrorMessage = Validate.MaxLength)]
        public string Name { get; set; }

        public List<int> Permissions { get; set; }
    }
}