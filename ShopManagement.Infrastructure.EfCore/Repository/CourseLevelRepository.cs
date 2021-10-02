using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.CourseLevel;
using ShopManagement.Domain.CourseLevelAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CourseLevelRepository : RepositoryBase<long, CourseLevel>, ICourseLevelRepository
    {
        private readonly ShopContext _context;

        public CourseLevelRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditCourseLevelViewModel GetDetails(long id)
        {
            return _context.CourseLevels.Select(x => new EditCourseLevelViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<CourseLevelViewModel> GetAllCourseLevel()
        {
            return _context.CourseLevels.Select(x => new CourseLevelViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().ToList();
        }

        public List<CourseLevelViewModel> SelectList()
        {
            return _context.CourseLevels.Select(x => new CourseLevelViewModel
            {
                Title = x.Title,
                Id = x.Id
            }).AsNoTracking().ToList();
        }
    }
}