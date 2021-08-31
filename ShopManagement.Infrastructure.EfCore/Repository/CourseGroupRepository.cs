using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.CourseGroup;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CourseGroupRepository:RepositoryBase<long,Domain.CourseGroupAgg.CourseGroup>,ICourseGroupRepository
    {
        private readonly ShopContext _context;
        public CourseGroupRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditCourseGroupViewModel GetDetails(long id)
        {
            var getDetails = _context.CourseGroups.Select(x => new EditCourseGroupViewModel
            {
                Title = x.Title,
                Description = x.Description,
                IsRemove = x.IsRemove,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Id = x.Id,
                SubGroupId = x.SubGroupId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
            return getDetails;
        }

        public List<CourseGroupViewModel> Search(CourseGroupSearchModel searchModel)
        {
            var query = _context.CourseGroups.Include(x=>x.SubGroup).Select(x => new CourseGroupViewModel
            {
                Id = x.Id,
                Title = x.Title,
                IsRemove = x.IsRemove,
                CreationDate = x.CreationDate.ToFarsi(),
               SubGroup = x.SubGroup.Title,
             SubGroupId = x.SubGroup.Id,
                CourseCount = 0
            }).AsNoTracking().ToList();

            if(!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title)).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();

            return orderly;
        }

        public List<CourseGroupViewModel> SelectList()
        {
            return _context.CourseGroups.Select(x => new CourseGroupViewModel()
            {
                Title = x.Title,
                Id = x.Id,
            }).AsNoTracking().ToList();
        }

        public string GetSlug(long id)
        {
           return _context.CourseGroups.Select(x => new { x.Slug, x.Id }).FirstOrDefault(x => x.Id == id)?.Slug;
        }
    }
}
