using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages.Order
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _order;

        public IndexModel(IOrderRepository order)
        {
            _order = order;
        }

        public List<OrderViewModel> List { get; set; }
        public void OnGet()
        {
            List = _order.GetAllOrderUser(User.Identity.Name);
        }
    }
}
