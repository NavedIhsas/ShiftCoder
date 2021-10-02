using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagementInfrastructure.EfCore.Repository
{
    public class CustomerRepository : RepositoryBase<long, CustomerDiscount>, ICustomerRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _course;

        public CustomerRepository(DiscountContext dbContext, DiscountContext context, ShopContext course) :
            base(dbContext)
        {
            _context = context;
            _course = course;
        }

        public EditCustomerDiscountViewModel GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscountViewModel
            {
                CourseId = x.CourseId,
                DiscountRate = x.DiscountRate,
                Description = x.Description,
                StartTime = x.StartTime.ToString(),
                EndTime = x.EndTime.ToString(),
                Reason = x.Reason,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var course = _course.Courses.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                CourseId = x.CourseId,
                DiscountRate = x.DiscountRate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Id = x.Id,
                Reason = x.Reason,
                IsRemove = x.IsRemove
            }).ToList();

            if (searchModel.CourseId > 0)
                query = query.Where(x => x.CourseId == searchModel.CourseId).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.StartTime))
                query = query.Where(x => x.StartTime <= searchModel.StartTime.ToGeorgianDateTime()).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.EndTime))
                query = query.Where(x => x.EndTime >= searchModel.EndTime.ToGeorgianDateTime()).ToList();

            foreach (var item in query)
                item.CourseName = course.FirstOrDefault(x => x.Id == item.CourseId)?.Name;


            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public int? CustomerDiscountRate(long courseId)
        {
            return _context.CustomerDiscounts.FirstOrDefault(x => x.CourseId == courseId)?.DiscountRate;
        }
    }
}