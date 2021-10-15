using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Account;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages.Order
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _order;
        private readonly IAccountQuery _user;
        public IndexModel(IOrderRepository order, IAccountQuery user)
        {
            _order = order;
            _user = user;
        }

        public List<OrderViewModel> List { get; set; }
        public UserInformationQueryModel UserInformation { get; set; }
        public void OnGet()
        {
            var currentUser = User.Identity.Name;
            List = _order.GetAllOrderUser(currentUser);
            UserInformation = _user.UserInformation(currentUser);
        }
    }
}