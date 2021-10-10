using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly IAuthHelper _authHelper;
        private readonly AccountContext _context;
        private readonly IRoleRepository _role;

        public AccountRepository(AccountContext dbContext, AccountContext context, IAuthHelper authHelper,
            IRoleRepository role) : base(dbContext)
        {
            _context = context;
            _authHelper = authHelper;
            _role = role;
        }

        public EditAccountViewModel GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccountViewModel
            {
                FullName = x.FullName,
                Email = x.Email,
                Phone = x.Phone,
                Password = x.Password,
                RoleId = x.RoleId,
                ActiveCode = x.ActiveCode,
                AvatarName = x.Avatar,
                Id = x.Id
                //  Teacher = MapTeacher(x.Teachers)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public BlockUserViewModel GetUserForBlock(long id)
        {
            return _context.Accounts.Select(x => new BlockUserViewModel
            {
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public BlockUserViewModel GetUserForUnblock(long id)
        {
            return _context.Accounts.IgnoreQueryFilters().Select(x => new BlockUserViewModel
            {
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void ConfirmUnblockUser(long id)
        {
            var user = _context.Accounts.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);
            user.Active(id);
            _context.Accounts.Update(user);
            _context.SaveChanges();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Eml = x.Email,
                Email = x.Email.Substring(0, Math.Min(x.Email.Length, 15)) + "...",
                EmailConfirm = x.EmailConfirm,
                RoleName = x.Role.Name,
                RoleId = x.RoleId,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();

            if (searchModel.RoleId > 0)
                query = query.Where(x => x.RoleId == searchModel.RoleId).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Eml.Trim().ToLower().Contains(searchModel.Email.Trim().ToLower())).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.FullName.ToLower().Contains(searchModel.FullName.Trim().ToLower())).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();

            return orderly;
        }

        public long GetUserIdBy(string email)
        {
            return _context.Accounts.Single(x => x.Email == FixedText.FixEmail(email)).Id;
        }

        public Account? GetUserBy(string email)
        {
            return _context.Accounts.SingleOrDefault(x => x.Email == FixedText.FixEmail(email));
        }
        public Account GetUserBy(long? id)
        {
            return _context.Accounts.Find(id);
        }


        public Account GetUserBy(long id)
        {
            return _context.Accounts.Find(id);
        }

        public Account GetUserByActiveCode(string activeCode)
        {
            return _context.Accounts.SingleOrDefault(x => x.ActiveCode == activeCode);
        }


        public List<AccountViewModel> ShowBlockedUser()
        {
            return _context.Accounts.IgnoreQueryFilters().Where(x => !x.IsActive).Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                CreationDate = x.CreationDate.ToFarsi(),
                Email = x.Email
            }).ToList();
        }

        public bool Login(LoginViewModel login)
        {
            var user = _context.Accounts.SingleOrDefault(x =>
                x.Email == FixedText.FixEmail(login.Email) && x.Password == login.Password);
            if (user == null || user.IsActive == false) return false;

            var permission = _role.GetById(user.RoleId).Permissions.Select(x => x.Code).ToList();

            var authModel = new AuthHelperViewModel(user.Id, user.RoleId, user.FullName, user.Email,login.RememberMe, permission);
            _authHelper.Signin(authModel);
            return true;
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }


        public bool EmailConfirm(string activeCode)
        {
            var user = _context.Accounts.SingleOrDefault(x => x.ActiveCode == activeCode);
            if (user == null || user.EmailConfirm) return false;

            user.ConfirmEmail(activeCode);
            user.ChangeActiveCode(user.Id);
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }

        public List<AccountViewModel> SelectList()
        {
            return _context.Accounts.Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName
            }).ToList();
        }

        private static List<TeacherViewModel> MapTeacher(IEnumerable<Teacher> teachers)
        {
            return teachers.Select(x => new TeacherViewModel
            {
                AccountId = x.AccountId,
                Bio = x.Bio,
                Resumes = x.Resumes,
                Skills = x.Skills
            }).ToList();
        }
    }
}