using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.Course;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CourseRepository : RepositoryBase<long, Course>, ICourseRepository
    {
        private readonly ShopContext _context;

        public CourseRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditCourseViewModel GetDetails(long id)
        {
            var course = _context.Courses.Select(x => new EditCourseViewModel
            {
                Name = x.Name,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                FileName = x.File,
                Price = x.Price,
                Code = x.Code,
                PictureName = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CourseGroupId = x.CourseGroupId,
                CourseLevelId = x.CourseLevelId,
                CourseStatusId = x.CourseStatusId,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
            return course;
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            var query = _context.Courses.Include(x=>x.CourseGroup).Select(x => new CourseViewModel
            {
                Name = x.Name,
                Picture = x.Picture,
                Code = x.Code,
                CourseGroupId = x.CourseGroup.Id,
                Id = x.Id,
                CourseGroup = x.CourseGroup.Title,
                UpdateDate = x.UpdateDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi(),
                Price = x.Price,
            }).AsNoTracking().ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name.Trim())).ToList();

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code.Trim())).ToList();

            if (searchModel.CourseGroupId > 0)
                query = query.Where(x => x.CourseGroupId == searchModel.CourseGroupId).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public List<CourseViewModel> SelectCourses()
        {
            return _context.Courses.Select(x => new CourseViewModel()
            {
                Name = x.Name,
                Id = x.Id,
            }).AsNoTracking().ToList();
        }
    }
}
