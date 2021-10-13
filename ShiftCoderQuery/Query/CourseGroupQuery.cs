using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.CourseGroup;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    public class CourseGroupQuery : ICourseGroupQuery
    {
        private readonly ShopContext _context;

        public CourseGroupQuery(ShopContext context)
        {
            _context = context;
        }

        public List<CourseGroupQueryModel> GetAllCourseGroup()
        {
            return _context.CourseGroups.Where(x => !x.IsRemove).Include(x => x.SubGroup).Select(x =>
                new CourseGroupQueryModel
                {
                    Title = x.Title,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    SubGroupId = x.SubGroupId,
                    SubGroup = x.SubGroup.Title,
                    Id = x.Id,
                    Picture = x.Picture,
                    PictureTitle = x.PictureTitle,
                    PictureAlt = x.PictureAlt,
                    CreationDate = x.CreationDate,
                    CourseCount = x.Courses.Count
                }).AsNoTracking().OrderBy(x=>x.CreationDate).ToList();
        }


        public List<LatestCourseGroupViewModel> LatestCourseGroup()
        {
            return _context.CourseGroups.Where(x => !x.IsRemove).OrderByDescending(x => x.CreationDate).Select(x => new LatestCourseGroupViewModel
            {
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Title = x.Title,
                Slug = x.Slug,
                CourseCount = x.Courses.Count
            }).AsNoTracking().ToList();
        }


        public List<LatestCourseGroupViewModel> GetSixGroup()
        {
            return _context.CourseGroups.Where(x => !x.IsRemove).Where(x => x.SubGroup == null).Select(x =>
                new LatestCourseGroupViewModel
                {
                    Title = x.Title,
                    Slug = x.Slug,
                    CourseCount = x.Courses.Count
                }).Take(6).AsNoTracking().ToList();
        }


        public List<CourseGroupQueryModel> SearchQuery(CourseGroupSearchQuery categories)
        {
            var query = _context.CourseGroups.Select(x => new CourseGroupQueryModel
            {
                Title = x.Title,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                SubGroupId = x.SubGroupId,
                Id = x.Id,
                CreationDate = x.CreationDate
            }).AsNoTracking().ToList();
            if (!string.IsNullOrWhiteSpace(categories.Title))
                query = query.Where(x => x.Title.Contains(categories.Title)).ToList();

            var orderly = query.OrderByDescending(x => x.CreationDate).ToList();
            return orderly;
        }
    }
}