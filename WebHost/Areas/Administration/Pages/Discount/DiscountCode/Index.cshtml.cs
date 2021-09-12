using System.Collections.Generic;
using ColleagueDiscountManagementApplication.Contract.DiscountCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Discount.DiscountCode
{
    public class IndexModel : PageModel
    {
        private readonly IDiscountCodeApplication _application;

        public IndexModel(IDiscountCodeApplication application)
        {
            _application = application;
        }

        public DiscountCodeSearchModel SearchModel;
        public List<DiscountCodeViewModel> List;

        public void OnGet(DiscountCodeSearchModel search)
        {
            List = _application.Search(search);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateDiscountCodeViewModel());
        }

        public JsonResult OnPostCreate(CreateDiscountCodeViewModel command)
        {
            var discount = _application.Create(command);
            return new JsonResult(discount);
        }

        public IActionResult OnGetEdit(long id)
        {
            var edit = _application.GetDetails(id);
            return Partial("./Edit", edit);
        }

        public JsonResult OnPostEdit(EditDiscountCodeViewModel command)
        {
            var discount = _application.Edit(command);
            return new JsonResult(discount);
        }
    }
}
