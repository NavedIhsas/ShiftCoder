using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Discount;
using ShiftCoderQuery.Contract.Discount.Enum;
using Shop.Management.Application.Contract.Order;
using Shop.Management.Application.Contract.OrderDetail;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        
        private readonly IOrderRepository _order;
        private readonly IDiscountQuery _discount;

        public OrdersModel(IOrderRepository order, IDiscountQuery discount)
        {
            _order = order;
            _discount = discount;
        }

        public string Message;
        public OrderViewModel List;
        public void OnGet(long id, string type = "")
        {
            ViewData["discountType"] = type.ToString();
            List = _order.GetOrderUser(User.Identity.Name,id);
        }

        public IActionResult OnGetFinallyOrder(long id)
        {
            
            var order = _order.OrderFinally(User.Identity.Name, id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostUseDiscount(long orderId, string code)
        {
            DiscountUseType type = _discount.UseDiscount(orderId, code);
            return Redirect("/UserPanel/Orders/"+ orderId +"?type=" + type.ToString());

        }
    }
}
