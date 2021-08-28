using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Course;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
   public class CourseQuery:ICourseQuery
   {
       private readonly ShopContext _context;

       public CourseQuery(ShopContext context)
       {
           _context = context;
       }

       public List<CourseQueryModel> GetAllCourse()
       {
         return  _context.Courses.Select(x => new CourseQueryModel
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
               Slug = x.Slug
           }).AsNoTracking().ToList();
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
           }).AsNoTracking().OrderByDescending(x=>x.CreationDate).Take(8).ToList();
        }
   }
}
