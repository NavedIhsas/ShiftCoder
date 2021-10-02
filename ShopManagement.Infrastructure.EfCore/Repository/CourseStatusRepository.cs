using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.CourseStatus;
using ShopManagement.Domain.CourseStatusAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CourseStatusRepository : RepositoryBase<long, CourseStatus>, ICourseStatusRepository
    {
        private readonly ShopContext _context;

        public CourseStatusRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditCourseStatusViewModel GetDetails(long id)
        {
            return _context.CourseStatus.Select(x => new EditCourseStatusViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<CourseStatusViewModel> GetAllCourseStatus()
        {
            return _context.CourseStatus.Select(x => new CourseStatusViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().ToList();
        }

        public List<CourseStatusViewModel> SelectList()
        {
            return _context.CourseStatus.Select(x => new CourseStatusViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().ToList();
        }
    }
}