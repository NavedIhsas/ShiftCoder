using System.Collections.Generic;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace WebHost.Areas.UserPanel.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _order;
        private readonly IAccountRepository _account;
        public OrderViewModel List;
        public Account AccountList;

        public IndexModel(IOrderRepository order, IAccountRepository account)
        {
            _order = order;
            _account = account;
        }

        public void OnGet()
        {
            AccountList = _account.GetUserBy(User.Identity.Name);
        }
    }
}
