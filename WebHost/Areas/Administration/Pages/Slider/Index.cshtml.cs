using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.HomePhoto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Slider
{
    public class IndexModel : PageModel
    {
        private readonly ISliderApplication _application;

        public IndexModel(ISliderApplication application)
        {
            _application = application;
        }

        public List<SliderViewModel> List;
        public void OnGet()
        {
            List = _application.GetAllList();
        }

        [NeedPermission(Permission.CreatePhotoHomePage)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new SliderViewModel());
        }

        [NeedPermission(Permission.CreatePhotoHomePage)]
        public IActionResult OnPostCreate(SliderViewModel command)
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
        public IActionResult OnPostEdit(SliderViewModel command)
        {
            _application.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
