using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.ProvinceAgg;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Account;

namespace ShiftCoderQuery.Query
{
   public class AccountQuery:IAccountQuery
   {
       private readonly AccountContext _context;

       public AccountQuery(AccountContext context)
       {
           _context = context;
       }

       public UserInformationQueryModel UserInformation(string email)
       {
          var user= _context.Accounts.Where(x => x.Email == email)
               .Include(x=>x.City)
               .Include(x=>x.Role).Include(x=>x.Teachers)
               .Select(x => new UserInformationQueryModel
           {
               FullName = x.FullName,
               Email = x.Email,
               Phone = x.Phone,
               Skill = MapTeacher(x.Teachers),
               City = x.City.Name,
               Province = x.City.Province.Name,
               RoleName = x.Role.Name,
               AvatarName = x.Avatar,
               Gander = x.Gander,
               Id=x.Id,
               AboutMe = x.AboutMe,
               BirthDate = x.BirthDate.ToFarsi()
           }).Single();
          return user;
       }

       public List<ProvinceViewModel> ProvinceSelectList()
       {
           return _context.Provinces.Select(x => new ProvinceViewModel()
           {
               Id = x.Id,
               Name = x.Name
           }).AsNoTracking().ToList();
       }

       public List<CityViewModel> CitySelectList()
       {
           return _context.Cities.Select(x => new CityViewModel()
           {
               Id = x.Id,
               Name = x.Name
           }).ToList();
        }

       public List<SelectListItem> GetCitiesFormProvince(long provinceId)
       {
           var city= _context.Cities.Where(g =>g.ProvinceId == provinceId)
               .Select(g => new SelectListItem()
               {
                   Text = g.Name,
                   Value = g.Id.ToString()
               }).ToList();
           return city;
       }

        private static string MapTeacher(IEnumerable<Teacher> teachers)
       {
           return teachers.Select(x => x.Skills).FirstOrDefault();
       }

       public EditProfileQueryModel GetDetails(long id)
       {
           return _context.Accounts.Where(x => x.Id == id).Include(x=>x.City).ThenInclude(x=>x.Province).Select(x => new EditProfileQueryModel
           {
               FullName = x.FullName,
               Email = x.Email,
               Phone = x.Phone,
               Password = x.Password,
               Id = x.Id,
               Gander = x.Gander,
               RoleId = x.RoleId,
               ActiveCode = x.ActiveCode,
               AvatarName = x.Avatar,
               CityId = x.CityId,
               BirthDate = x.BirthDate.ToString(),
               ProvinceId = x.City.Province.Id,
               AboutMe = x.AboutMe
           }).FirstOrDefault();
       }

        public void EditProfile(EditProfileQueryModel command)
        {
            var user = _context.Accounts.Find(command.Id);
            if(user==null) return;
            user.Edit(command.FullName,command.Email,command.Phone,command.AvatarName,command.RoleId,command.CityId,command.Gander,command.BirthDate.ToGeorgianDateTime(),command.AboutMe);
            
            _context.Accounts.Update(user);
            _context.SaveChanges();
        }
    }
}
