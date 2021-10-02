using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.Administration.Pages
{
    [Authorize]
    [NeedPermission(Permission.AdministrationHomepage)]
    public class IndexModel : PageModel
    {
        private readonly ICommentQuery _comment;
        private readonly IOrderRepository _order;
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> CommentList;

        public List<OrderViewModel> List;

        public IndexModel(IOrderRepository order, ICommentQuery comment)
        {
            _order = order;
            _comment = comment;
        }

        [NeedPermission(Permission.AdministrationHomepage)]
        public IActionResult OnGet()
        {
            var email = User.Identity.Name;
            if (email == null) return NotFound();

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            List = _order.GetAllOrderForAdminPanel(ipAddress, email);
            CommentList = _comment.GetAll();
            return Page();
        }
    }
}