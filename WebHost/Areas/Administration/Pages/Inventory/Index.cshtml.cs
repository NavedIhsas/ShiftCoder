using System.Collections.Generic;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Management.Application.Contract.Course;

namespace WebHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        private readonly IInventoryApplication _inventory;
        private readonly ICourseApplication _course;

        public IndexModel(IInventoryApplication inventory, ICourseApplication course)
        {
            _inventory = inventory;
            _course = course;
        }

        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        public SelectList SelectCourse;
        public void OnGet(InventorySearchModel searchModel)
        {
            SelectCourse = new SelectList(_course.SelectCourses(), "Id", "Name");
            Inventory = _inventory.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var inventory = new CreateInventoryViewModel()
            {
                SelectCourses =_course.SelectCourses()
            };
            return Partial("./Create", inventory);
        }

        public JsonResult OnPostCreate(CreateInventoryViewModel command)
        {
            var inventory = _inventory.Create(command);
            return new JsonResult(inventory);
        }


        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventory.GetDetails(id);
            return Partial("./Edit", inventory);
        }

        public JsonResult OnPostEdit(EditInventoryViewModel command)
        {
            var inventory = _inventory.Edit(command);
            return new JsonResult(inventory);
        }

        public IActionResult OnGetInCrease()
        {
            return Partial("./InCrease", new IncreaseViewModel());
        }

        public JsonResult OnPostInCrease(IncreaseViewModel increase)
        {
            var increaseInventory = _inventory.IncreaseInventory(increase);
            return new JsonResult(increaseInventory);
        }
        public IActionResult OnGetReduce()
        {
            return Partial("./Reduce", new ReduceViewModel());
        }

        public JsonResult OnPostReduce(ReduceViewModel reduce)
        {
            var reduceInventory = _inventory.ReduceInventory(reduce);
            return new JsonResult(reduceInventory);
        }
    }
}
