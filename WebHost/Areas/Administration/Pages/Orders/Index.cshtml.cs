using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Domain.VisitAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.Administration.Pages.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICourseApplication _course;
        private readonly IOrderRepository _order;
        private readonly IVisitRepository _visit;
        public List<OrderViewModel> List;

        public SelectList SelectList;
        public List<Visit> Visit;

        public IndexModel(ICourseApplication course, IOrderRepository order, IVisitRepository visit)
        {
            _course = course;
            _order = order;
            _visit = visit;
        }

        [NeedPermission(Permission.SystemAdministratorOrders)]
        public void OnGet(CourseSearchModel searchModel)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            SelectList = new SelectList(_course.SelectCourses(), "Id", "Title");

            if (User.Identity == null) return;

            var email = User.Identity.Name;
            List = _order.GetAllOrderForAdminPanel(ipAddress, email);
            Visit = _visit.GetAllVisit();
        }
    }
}