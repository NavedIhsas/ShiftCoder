using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.CourseStatus;
using Shop.Management.Application.Contract.CourseSuitable;
using ShopManagement.Domain.CourseSuitableAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CourseSuitableRepository : RepositoryBase<long, CourseSuitable>, ICourseSuitableRepository
    {
        private readonly ShopContext _shopContext;

        public CourseSuitableRepository(ShopContext dbContext, ShopContext shopContext) : base(dbContext)
        {
            _shopContext = shopContext;
        }

        public List<CourseSuitableViewModel> GetAllList()
        {
            return _shopContext.CourseSuitableList.Include(x=>x.Courses).Select(x => new CourseSuitableViewModel
            {
                Id = x.Id,
                Title = x.Title,
                IsRemove = x.IsRemove,
                CourseName = x.Courses.Name
            }).AsNoTracking().ToList();
        }

        public EditSuitableViewModel GetDetails(long id)
        {
            return _shopContext.CourseSuitableList.Select(x => new EditSuitableViewModel()
            {
                Title = x.Title,
                Id = x.Id,
                CourseId = x.CourseId
            }).FirstOrDefault(x => x.Id == id);
        }

    }
}