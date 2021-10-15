using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.ProvinceAgg;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Account;

namespace ShiftCoderQuery.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly AccountContext _context;

        public AccountQuery(AccountContext context)
        {
            _context = context;
        }

        public UserInformationQueryModel UserInformation(string email)
        {
            var user = _context.Accounts.Where(x => x.Email == email)
                 .Include(x => x.City)
                 .Include(x => x.Role).Include(x => x.Teachers)
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
                     Id = x.Id,
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
            var city = _context.Cities.Where(g => g.ProvinceId == provinceId)
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

        public EditProfileQueryModel GetDetails(string email)
        {
            return _context.Accounts.Where(x => x.Email == email).Include(x => x.City).ThenInclude(x => x.Province).Select(x => new EditProfileQueryModel
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
            var imgName = command.Avatar?.FileName;

            if (command.Avatar != null)
            {
                //check for image
                if (command.Avatar.IsImage())
                {
                    //delete current avatar
                    var currentFile = $"wwwroot/FileUploader/UserAvatar/{user.Avatar}";
                    if (File.Exists(currentFile))
                        File.Delete(currentFile);

                    //resize to 315 X 315
                    #region 315 X 315

                    var image = Image.FromStream(command.Avatar.OpenReadStream());
                    var resized = new Bitmap(image, new Size(315, 315));

                    using var imageStream = new MemoryStream();
                    resized.Save(imageStream, ImageFormat.Jpeg);
                    var imageBytes = imageStream.ToArray();


                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/UserAvatar/", imgName);

                    using var streamImg = new FileStream(
                        path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                    streamImg.Write(imageBytes, 0, imageBytes.Length);

                    #endregion
                }

            }

            if (user == null) return;
            user.Edit(command.FullName, command.Email, command.Phone, imgName, command.RoleId, command.CityId, command.Gander, command.BirthDate.ToGeorgianDateTime(), command.AboutMe);

            _context.Accounts.Update(user);
            _context.SaveChanges();
        }

        public bool ChangePassword(string email,ChangePasswordViewModel command)
        {
            var user = _context.Accounts.FirstOrDefault(x => x.Email == email);
            if (user == null) return false;
            if (user.Password != command.OldPassword) return false;
            user.ChangePassword(command.NewPassword);
            _context.Update(user);
            _context.SaveChanges();
            return true;

        }
    }
}
