using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShiftCoderQuery.Contract.Account;

namespace WebHost.Areas.UserPanel.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAccountQuery _user;
        private readonly IAccountRepository _account;
        public UserInformationQueryModel Information;

        [BindProperty]
        public EditProfileQueryModel Edit { get; set; }
        public IndexModel(IAccountQuery user, IAccountRepository account)
        {
            _user = user;
            _account = account;
        }

        public void OnGet()
        {

            Information = _user.UserInformation(User.Identity.Name);
        }

        public IActionResult OnGetEdit(long id)
        {
            var user=  _user.GetDetails(id);
            user.CitySelectList = _user.CitySelectList();
            user.ProvinceSelectList = _user.ProvinceSelectList();
            return Partial("EditProfile", user);
        }

        public IActionResult OnPostEdit()
        {
           _user.EditProfile(Edit);
          return RedirectToPage("/Account/Index");
        }

        public JsonResult OnGetChangeSubGroup(long id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.AddRange(_user.GetCitiesFormProvince(id));
            return new JsonResult((new SelectList(list, "Value", "Text")));
        }
    }
}
