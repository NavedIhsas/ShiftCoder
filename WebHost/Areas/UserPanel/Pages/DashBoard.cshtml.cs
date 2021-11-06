using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShiftCoderQuery.Contract.Account;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using Shop.Management.Application.Contract.UserCourse;

namespace WebHost.Areas.UserPanel.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAccountQuery _user;
        private readonly ICommentQuery _comment;
        private readonly ICourseQuery _course;
        
        [BindProperty]
        public EditProfileQueryModel Edit { get; set; }
        public List<UserCourseViewModel> UserCourse;
        public UserInformationQueryModel Information;
        public IndexModel(IAccountQuery user, IAccountRepository account, ICourseQuery course, ICommentQuery comment)
        {
            _user = user;
            _course = course;
            _comment = comment;
        }

        public void OnGet()
        {
            Information = _user.UserInformation(User.Identity.Name);
            UserCourse = _course.GetUserCourseBy(User.Identity.Name);
        }

        public IActionResult OnGetEdit()
        {
            var currentUser = User.Identity.Name;
            var user=  _user.GetDetails(currentUser);
            user.UserInformation = _user.UserInformation(currentUser);
            user.CitySelectList = _user.CitySelectList();
            user.ProvinceSelectList = _user.ProvinceSelectList();
            return Partial("EditProfile", user);
        }

        public IActionResult OnPostEdit()
        {
           _user.EditProfile(Edit);
          return Redirect("/UserPanel/DashBoard");
        }

        public IActionResult OnGetComments()
        {
            var comment = _comment.GetUserComment(User.Identity.Name);
            return Partial("Comments", comment);
        }
        public JsonResult OnGetChangeSubGroup(long id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(_user.GetCitiesFormProvince(id));
            return new JsonResult((new SelectList(list, "Value", "Text")));
        }
    }
}
