using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IAuthHelper _authHelper;
        public AccountRepository(AccountContext dbContext, AccountContext context, IAuthHelper authHelper) : base(dbContext)
        {
            _context = context;
            _authHelper = authHelper;
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
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Eml = x.Email,
                Email = x.Email.Substring(0, Math.Min(x.Email.Length, 15)) + "...",
                IsActive = x.IsActive,
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

        public Account GetUserBy(string email)
        {
            return _context.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public OperationResult Login(LoginViewModel login)
        {
            var operation = new OperationResult();

            var currentUser = GetUserBy(login.Email.ToLower().Trim());
            if (currentUser == null) return operation.Failed(ApplicationMessage.LoginError);

            var userLogin = _context.Accounts.Any(x => x.Email.ToLower().Trim()
                == login.Email.ToLower().Trim() && x.Password == login.Password);
            if (userLogin == false) return operation.Failed(ApplicationMessage.LoginError);

            var authModel = new AuthHelperViewModel()
            {
                RoleId = currentUser.RoleId,
                Email = currentUser.Email,
                AccountId = currentUser.Id,
                Fullname = currentUser.FullName,
            };
            _authHelper.Signin(authModel);

            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }
    }
}
