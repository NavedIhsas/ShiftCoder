using System;
using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class TeacherRepository : RepositoryBase<long, Teacher>, ITeacherRepository
    {
        private readonly AccountContext _context;

        public TeacherRepository(AccountContext dbContext, AccountContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditTeacherViewModel GetTeacherDetails(long id)
        {
            return _context.Teachers.Select(x => new EditTeacherViewModel
            {
                Bio = x.Bio,
                Type = x.Type,
                Skills = x.Skills,
                Resumes = x.Resumes,
                Id = x.Id,
                AccountName = x.Account.FullName,
                AccountId = x.AccountId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<TeacherViewModel> GetAllTeachers()
        {
            return _context.Teachers.Include(x => x.Account).Select(x => new TeacherViewModel
            {
                Bio = x.Bio.Substring(0, Math.Min(x.Bio.Length, 30)) + "...",
                Skills = x.Skills,
                Resumes = x.Resumes.Substring(0, Math.Min(x.Resumes.Length, 30)) + "...",
                Id = x.Id,
                Type = x.Type,
                AccountName = x.Account.FullName
            }).OrderByDescending(x => x.Id).ToList();
        }

        public List<TeacherViewModel> SelectList()
        {
            return _context.Teachers.Where(x => x.Type == ThisType.Teacher).Select(x => new TeacherViewModel
            {
                Id = x.Id,
                AccountName = x.Account.FullName
            }).ToList();
        }

        public List<TeacherViewModel> SelectListForArticles()
        {
            return _context.Teachers.Where(x => x.Type == ThisType.Blogger).Select(x => new TeacherViewModel
            {
                Id = x.Id,
                AccountName = x.Account.FullName
            }).ToList();
        }

        public Teacher GetTeacherBy(long id)
        {
            return _context.Teachers.Find(id);
        }

        public Teacher GetBloggerBy(long id)
        {
            return _context.Teachers.Where(x => x.Type == ThisType.Blogger)
                .Include(x => x.Account)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Teacher> GetAllBlogger()
        {
            return _context.Teachers.Include(x => x.Account).Where(x => x.Type == ThisType.Blogger).ToList();
        }

        public void DeleteTeacher(long id)
        {
            var deleteTeacher = GetTeacherBy(id);
            _context.Teachers.Remove(_context.Teachers.Find(deleteTeacher.Id));
            _context.SaveChanges();
        }
    }
}