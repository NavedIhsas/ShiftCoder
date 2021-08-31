using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.CoursePrerequisite;
using ShopManagement.Domain.CoursePrerequisiteAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class PrerequisiteRepository : RepositoryBase<long, CoursePrerequisite>, IPrerequisiteRepository
    {
        private readonly ShopContext _shopContext;

        public PrerequisiteRepository(ShopContext dbContext, ShopContext shopContext) : base(dbContext)
        {
            _shopContext = shopContext;
        }

        public List<CoursePrerequisiteViewModel> GetAllList()
        {
            return _shopContext.CoursePrerequisites.Include(x=>x.Courses).Select(x => new CoursePrerequisiteViewModel
            {
                Id = x.Id,
                Title = x.Title,
                IsRemove = x.IsRemove,
                CourseName=x.Courses.Name
            }).AsNoTracking().ToList();
        }

        public EditCoursePrerequisiteViewModel GetDetails(long id)
        {
            return _shopContext.CoursePrerequisites.Select(x => new EditCoursePrerequisiteViewModel()
            {
                Title = x.Title,
                Id = x.Id,
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}