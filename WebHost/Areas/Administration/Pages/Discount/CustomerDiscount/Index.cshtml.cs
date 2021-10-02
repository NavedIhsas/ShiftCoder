using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Management.Application.Contract.Course;

namespace WebHost.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerDiscountApplication _application;
        private readonly ICourseApplication _course;
        public List<CustomerDiscountViewModel> List;
        public CustomerDiscountSearchModel SearchModel;
        public SelectList SelectList;

        public IndexModel(ICustomerDiscountApplication application, ICourseApplication course)
        {
            _application = application;
            _course = course;
        }

        [TempData] public string Message { get; set; }

        [NeedPermission(Permission.ListCostumerDiscount)]
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            List = _application.Search(searchModel);
            SelectList = new SelectList(_course.SelectCourses(), "Id", "Name");
        }

        [NeedPermission(Permission.CreateCostumerDiscount)]
        public IActionResult OnGetCreate()
        {
            var select = new CreateCustomerDiscountViewModel
            {
                SelectList = _course.SelectCourses()
            };
            return Partial("./Create", select);
        }

        public JsonResult OnPostCreate(CreateCustomerDiscountViewModel command)
        {
            var create = _application.Create(command);
            return new JsonResult(create);
        }


        [NeedPermission(Permission.EditCostumerDiscount)]
        public IActionResult OnGetEdit(long id)
        {
            var getDetails = _application.GetDetails(id);
            getDetails.SelectList = _course.SelectCourses();
            return Partial("./Edit", getDetails);
        }

        public JsonResult OnPostEdit(EditCustomerDiscountViewModel command)
        {
            var edit = _application.Edit(command);
            return new JsonResult(edit);
        }

        [NeedPermission(Permission.DeleteCostumerDiscount)]
        public IActionResult OnGetRemove(long id)
        {
            var getDetails = _application.Remove(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return RedirectToPage("./Index");
        }

        [NeedPermission(Permission.RestoreCostumerDiscount)]
        public IActionResult OnGetRestore(long id)
        {
            var getDetails = _application.Restore(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return RedirectToPage("./Index");
        }
    }
}