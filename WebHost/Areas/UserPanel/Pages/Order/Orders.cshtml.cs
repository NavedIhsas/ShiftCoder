using System.Globalization;
using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Account;
using ShiftCoderQuery.Contract.Discount;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages.Order
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        private readonly IDiscountQuery _discount;

        private readonly IOrderRepository _order;
        private readonly IZarinPalFactory _zarinPal;
        private readonly IAccountQuery _user;
        public OrderViewModel List;
        public UserInformationQueryModel UserInformation { get; set; }
        public string Message;

        public OrdersModel(IOrderRepository order, IDiscountQuery discount, IZarinPalFactory zarinPal, IAccountQuery user)
        {
            _order = order;
            _discount = discount;
            _zarinPal = zarinPal;
            _user = user;
        }

        public void OnGet(long id, string type = "")
        {
            var currentUser = User.Identity.Name;
            ViewData["discountType"] = type;
            List = _order.GetOrderUser(currentUser, id);
            UserInformation = _user.UserInformation(currentUser);
        }

        public IActionResult OnGetFinallyOrder(long id)
        {
            var order = _order.OrderFinally(User.Identity.Name, id);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostUseDiscount(long orderId, string code)
        {
            var type = _discount.UseDiscount(orderId, code);
            ViewData["IsSuccess"] = true;
            return Redirect("/UserPanel/Order/Orders/" + orderId + "?type=" + type);
        }

        public IActionResult OnGetPay(long id)
        {
            var order = _order.Pay(User.Identity.Name, id);
            var paymentResponse =
                _zarinPal.CreatePaymentRequest(order.OrderSum.ToString(CultureInfo.InvariantCulture), "", "", "", id);
            return Redirect($"https://{_zarinPal.Prefix}.zarintPal.com/pg/startPay/{paymentResponse.Authority}");
        }
    }
}