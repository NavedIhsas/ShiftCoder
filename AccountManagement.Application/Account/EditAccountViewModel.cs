using System.Collections.Generic;

namespace AccountManagement.Application.Contract.Account
{
    public class EditAccountViewModel : RegisterUserViewModel
    {
        public EditAccountViewModel()
        {
            Teacher = new TeacherViewModel();
        }

        public List<TeacherViewModel> Teachers { get; set; }

        public long Id { get; set; }
    }
}