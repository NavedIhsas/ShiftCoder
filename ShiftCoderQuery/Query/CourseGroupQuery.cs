using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.CourseGroup;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
  public  class CourseGroupQuery:ICourseGroupQuery
  {
      private readonly ShopContext _context;

      public CourseGroupQuery(ShopContext context)
      {
          _context = context;
      }
      public List<CourseGroupQueryModel> GetAllCourseGroup()
      {
         return _context.CourseGroups.Where(x=>!x.IsRemove).Include(x=>x.SubGroup).Select(x => new CourseGroupQueryModel
          {
              Title = x.Title,
              Description = x.Description,
              KeyWords = x.KeyWords,
              MetaDescription = x.MetaDescription,
              Slug = x.Slug,
              SubGroupId = x.SubGroupId,
              SubGroup = x.SubGroup.Title,
              Id=x.Id
          }).AsNoTracking().ToList();
      }

      public List<CourseGroupQueryModel> SearchQuery(CourseGroupSearchQuery categories)
      {
          var query = _context.CourseGroups.Select(x => new CourseGroupQueryModel
          {
              Title = x.Title,
              Description = x.Description,
              KeyWords = x.KeyWords,
              MetaDescription = x.MetaDescription,
              Slug = x.Slug,
              SubGroupId = x.SubGroupId,
              Id = x.Id,
              CreationDate=x.CreationDate,
          }).AsNoTracking().ToList();
          if (!string.IsNullOrWhiteSpace(categories.Title))
              query = query.Where(x => x.Title.Contains(categories.Title)).ToList();

          var orderly = query.OrderByDescending(x => x.CreationDate).ToList();
          return orderly;
      }
  }
}
