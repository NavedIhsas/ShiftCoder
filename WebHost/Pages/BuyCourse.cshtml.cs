using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Order;

namespace WebHost.Pages
{
    [Authorize]
    public class BuyCourseModel : PageModel
    {
        private readonly IOrderApplication _order;

        public BuyCourseModel(IOrderApplication order)
        {
            _order = order;
        }
     
        [TempData] public string Message { get; set; }
        public IActionResult OnGet(long id)
        {
            var order = _order.Create(User.Identity.Name, id);
            return Redirect("/UserPanel/Order/Orders/" + order);

        }
    }
}
