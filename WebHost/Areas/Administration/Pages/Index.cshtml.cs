using System.Collections.Generic;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.Administration.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _order;
        private readonly ICommentQuery _comment;

        public IndexModel( IOrderRepository order, ICommentQuery comment)
        {
            _order = order;
            _comment = comment;
        }

        public List<OrderViewModel> List;
        public List<CommentManagement.Domain.CourseCommentAgg.Comment> CommentList;

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
