using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using ColleagueDiscountManagementApplication.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagementInfrastructure.EfCore.Repository
{
   public class ColleagueRepository:RepositoryBase<long,ColleagueDiscount>,IColleagueRepository
   {
       private readonly DiscountContext _context;
       private readonly ShopContext _course;
        public ColleagueRepository(DiscountContext dbContext, DiscountContext context, ShopContext course) : base(dbContext)
        {
            _context = context;
            _course = course;
        }

        public EditColleagueDiscountViewModel GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscountViewModel
            {
                CourseId = x.CourseId,
                DiscountRate = x.DiscountRate,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> GetAllList()
        {
            var courseName = _course.Courses.Select(x => new { x.Name, x.Id }).ToList();
            var list= _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id=x.Id,
                CourseId=x.CourseId,
                DiscountRate = x.DiscountRate,
                IsRemove = x.IsRemove,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().ToList();

            foreach (var item in list)
                item.CourseName = courseName.FirstOrDefault(x => x.Id == item.CourseId)?.Name;


            var orderly = list.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }
    }
}
