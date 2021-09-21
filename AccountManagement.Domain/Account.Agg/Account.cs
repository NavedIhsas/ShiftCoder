using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.Account.Agg
{
   public class Account:EntityBase
    {
        public string FullName { get;private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public string Avatar { get; private set; }
        public bool IsActive { get; private set; }
        public bool EmailConfirm { get; private set; }
        public bool IsDelete { get;private set; }
        public long RoleId { get;private set; }
        public string ActiveCode { get;private set; }
        public Role Role { get;private set; }
        public List<Teacher> Teachers { get;private set; }
        public Account(string activeCode)
        {
            ActiveCode = activeCode;
        }

        public Account(string fullName, string email, string phone, string password, 
            string avatar, long roleId, string activeCode, List<Teacher> teachers)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            Password = password;
            if(!string.IsNullOrWhiteSpace(avatar))
             Avatar = avatar;
            Teachers = teachers;
            ActiveCode = activeCode;
            RoleId = roleId == 0 ? 10005 : roleId;
            IsActive = true;
            EmailConfirm = false;
            IsDelete = false;
        }

        public Account(string fullName, string email, string phone, string password,
            string avatar, long roleId, string activeCode)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            Password = password;
            ActiveCode = activeCode;
            if (!string.IsNullOrWhiteSpace(avatar))
                Avatar = avatar;
            RoleId = roleId == 0 ? 10005 : roleId;
            IsActive = true;
            EmailConfirm = false;
            IsDelete = false;
        }


        public void Edit(string fullName, string email, string phone, string avatar, long roleId, List<Teacher> teachers, string activeCode)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            if (!string.IsNullOrWhiteSpace(avatar))
                Avatar = avatar;
            RoleId = roleId;
            Teachers = teachers;
            ActiveCode = activeCode;
        }

        public void Edit(string fullName, string email, string phone, string avatar, long roleId)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            if (!string.IsNullOrWhiteSpace(avatar))
                Avatar = avatar;
            RoleId = roleId;
        }

        public void DeActive(long id)=> IsActive = false;
        public void Active(long id)=> IsActive = true;
        public void ChangePassword(string password)=> Password = password;
        public void ConfirmEmail(string activeCode) => EmailConfirm = true;
        public void ChangeActiveCode(long id) => ActiveCode=NameGenerator.UniqCode();

    }
}
