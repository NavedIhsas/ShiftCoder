using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Course;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    public class CourseQuery : ICourseQuery
    {
        private readonly ShopContext _context;

        public CourseQuery(ShopContext context)
        {
            _context = context;
        }

        public List<CourseQueryModel> GetAllCourse(CourseQuerySearchModel searchQuery)
         
        {
            var query = _context.Courses.Select(x => new CourseQueryModel
            {
                Name = x.Name,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                File = x.File,
                Price = x.Price,
                Code = x.Code,
                UpdateDate = x.UpdateDate.ToFarsi(),
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CreationDate = x.CreationDate
            }).AsNoTracking().ToList();



            if (!string.IsNullOrWhiteSpace(searchQuery.Name))
                query = query.Where(x => x.Name.Contains(searchQuery.Name.Trim())).ToList();


            if (string.IsNullOrWhiteSpace(searchQuery.Filter)) return query;
            {
                query = searchQuery.Filter switch
                {
                    "maxPrice" => query.OrderByDescending(x => x.Price).ToList(),
                    "newest" => query.OrderByDescending(x => x.CreationDate).ToList(),
                    "minPrice" => query.OrderBy(x => x.Price).ToList(),
                    //"price"=>query.Where(x=>x.Price>0).ToList(),
                    //"free"=>query.Where(x=>x.Price==0).ToList(),
                    //"all"=>query,
                    _ => query
                };
            }
            var orderly = query.OrderByDescending(x => x.CreationDate).ToList();

            return orderly;
        }

        public List<CourseQueryModel> LatestCourses()
        {
            return _context.Courses.Select(x => new CourseQueryModel
            {

                Name = x.Name,
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                File = x.File,
                Price = x.Price,
                Code = x.Code,
                UpdateDate = x.UpdateDate.ToFarsi(),
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CreationDate = x.CreationDate,
            }).AsNoTracking().OrderByDescending(x => x.CreationDate).Take(8).ToList();
        }
    }
}
