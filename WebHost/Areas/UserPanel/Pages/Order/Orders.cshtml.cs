using System.Globalization;
using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Discount;
using ShiftCoderQuery.Contract.Discount.Enum;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages.Order
{
    [Authorize]
    public class OrdersModel : PageModel
    {

        private readonly IOrderRepository _order;
        private readonly IDiscountQuery _discount;
        private readonly IZarinPalFactory _zarinPal;

        public OrdersModel(IOrderRepository order, IDiscountQuery discount, IZarinPalFactory zarinPal)
        {
            _order = order;
            _discount = discount;
            _zarinPal = zarinPal;
        }

        public string Message;
        public OrderViewModel List;
        public void OnGet(long id, string type = "")
        {
            ViewData["discountType"] = type.ToString();
            List = _order.GetOrderUser(User.Identity.Name, id);
        }

        public IActionResult OnGetFinallyOrder(long id)
        {
           var order= _order.OrderFinally(User.Identity.Name, id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostUseDiscount(long orderId, string code)
        {
            DiscountUseType type = _discount.UseDiscount(orderId, code);
            return Redirect("/UserPanel/Orders/" + orderId + "?type=" + type.ToString());
        }

        public IActionResult OnGetPay(long id)
        {
            var order = _order.Pay(User.Identity.Name, id);
            var paymentResponse = _zarinPal.CreatePaymentRequest(order.OrderSum.ToString(CultureInfo.InvariantCulture), "","","",id);
            return Redirect($"https://{_zarinPal.Prefix}.zarintPal.com/pg/startPay/{paymentResponse.Authority}");

        }

       
    }
}
