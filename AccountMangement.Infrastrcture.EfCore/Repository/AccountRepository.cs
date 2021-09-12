﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using _0_Framework.Application;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        public AccountRepository(AccountContext dbContext, AccountContext context, IAuthHelper authHelper, IPasswordHasher passwordHasher) : base(dbContext)
        {
            _context = context;
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
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
                Id = x.Id,
                //  Teacher = MapTeacher(x.Teachers)

            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        private static List<TeacherViewModel> MapTeacher(IEnumerable<Teacher> teachers)
        {
            return teachers.Select(x => new TeacherViewModel()
            {
                AccountId = x.AccountId,
                Bio = x.Bio,
                Resumes = x.Resumes,
                Skills = x.Skills,
            }).ToList();
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

        public long GetUserIdBy(string email)
        {
            // ReSharper disable once PossibleNullReferenceException
            return _context.Accounts.FirstOrDefault(x => x.Email == email).Id;
        }

        public Account GetUserBy(string email)
       => _context.Accounts.FirstOrDefault(x => x.Email == email);

        public OperationResult Login(LoginViewModel login)
        {
            var operation = new OperationResult();

            var user = _context.Accounts.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password);
            if (user == null) return operation.Failed(ApplicationMessage.LoginError);

            var authModel = new AuthHelperViewModel(user.Id, user.RoleId, user.FullName, user.Email);
            _authHelper.Signin(authModel);
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> SelectList()
        {
            return _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
            }).ToList();
        }
    }
}
