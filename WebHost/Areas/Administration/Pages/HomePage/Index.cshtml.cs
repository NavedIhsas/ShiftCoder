using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.HomePhoto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Course;

namespace WebHost.Areas.Administration.Pages.HomePage
{
    public class IndexModel : PageModel
    {
        private readonly IHomePhotoApplication _application;

        public IndexModel(IHomePhotoApplication application)
        {
            _application = application;
        }

        public List<HomePhotoViewModel> List;
        public void OnGet()
        {
            List = _application.GetAllList();
        }

        [NeedPermission(Permission.CreatePhotoHomePage)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new HomePhotoViewModel());
        }

        [NeedPermission(Permission.CreatePhotoHomePage)]
        public IActionResult OnPostCreate(HomePhotoViewModel command)
        {
            _application.Create(command);
            return RedirectToPage("Index");
        }

        [NeedPermission(Permission.ChangePhotoHomePage)]
        public IActionResult OnGetEdit(long id)
        {
            var home = _application.GetDetails(id);
            return Partial("./Edit", home);
        }

        [NeedPermission(Permission.ChangePhotoHomePage)]
        public IActionResult OnPostEdit(HomePhotoViewModel command)
        {
            _application.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
