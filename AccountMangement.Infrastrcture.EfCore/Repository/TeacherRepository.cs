using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context.Teachers.Select(x => new EditTeacherViewModel()
            {
                Bio = x.Bio,
                Skills = x.Skills,
                Resumes = x.Resumes,
                Id = x.Id,
                AccountName = x.Account.FullName,
                AccountId = x.AccountId,
            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<TeacherViewModel> GetAllTeachers()
        {
            return _context.Teachers.Include(x=>x.Account).Select(x => new TeacherViewModel()
            {
               Bio = x.Bio.Substring(0,Math.Min(x.Bio.Length,30))+"...",
               Skills = x.Skills,
               Resumes = x.Resumes.Substring(0,Math.Min(x.Resumes.Length,30))+"...",
               Id = x.Id,
               AccountName = x.Account.FullName
            }).OrderByDescending(x=>x.Id).ToList();
        }

        public List<TeacherViewModel> SelectList()
        {
            return _context.Teachers.Select(x => new TeacherViewModel()
            {
                Id = x.Id,
                AccountName = x.Account.FullName
            }).ToList();
        }
    }
}