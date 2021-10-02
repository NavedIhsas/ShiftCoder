using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using Shop.Management.Application.Contract.Order;
using Shop.Management.Application.Contract.UserCourse;

namespace WebHost.Areas.UserPanel.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAccountRepository _account;
        private readonly ICommentQuery _comment;
        private readonly ICourseQuery _course;
        public AccountManagement.Domain.Account.Agg.Account AccountList;

        public OrderViewModel List;
        public List<UserCourseViewModel> UserCourse;

        public IndexModel(ICommentQuery comment, IAccountRepository account, ICourseQuery course)
        {
            _comment = comment;
            _account = account;
            _course = course;
        }

        public void OnGet()
        {
            AccountList = _account.GetUserBy(User.Identity.Name);
            UserCourse = _course.GetUserCourseBy(User.Identity.Name);
        }

        public IActionResult OnGetComments()
        {
            var comment = _comment.GetUserComment(User.Identity.Name);
            return Partial("Comments", comment);
        }
    }
}