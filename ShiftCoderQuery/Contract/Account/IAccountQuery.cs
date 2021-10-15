using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShiftCoderQuery.Contract.Account
{
    public interface IAccountQuery
    {
        EditProfileQueryModel GetDetails(string email);
        void EditProfile(EditProfileQueryModel command);
        UserInformationQueryModel UserInformation(string email);
        List<ProvinceViewModel> ProvinceSelectList();
        List<CityViewModel> CitySelectList();
        List<SelectListItem> GetCitiesFormProvince(long provinceId);

        bool ChangePassword(string email,ChangePasswordViewModel command);
    }
}