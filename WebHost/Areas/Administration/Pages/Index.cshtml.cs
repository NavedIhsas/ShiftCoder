using System.Collections.Generic;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INotificationRepository _notification;
        private readonly IOrderRepository _order;

        public IndexModel(INotificationRepository notification, IOrderRepository order)
        {
            _notification = notification;
            _order = order;
        }

        public List<OrderViewModel> List;

        public void OnGet()
        {
            List = _order.GetAllOrderForAdminPanel();
        }
    }
}
