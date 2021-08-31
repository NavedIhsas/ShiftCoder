using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.AfterCourse;
using ShopManagement.Domain.AfterTheCourseAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
   public class AfterCourseRepository:RepositoryBase<long,AfterTheCourse>,IAfterTheCourseRepository
   {
       private readonly ShopContext _shopContext;

       public AfterCourseRepository(ShopContext dbContext, ShopContext shopContext) : base(dbContext)
       {
           _shopContext = shopContext;
       }

       public List<AfterCourseViewModel> GetAllList()
       {
           return _shopContext.AfterTheCourses.Include(x=>x.Courses).Select(x => new AfterCourseViewModel
           {
               Id = x.Id,
               Title = x.Title,
               IsRemove = x.IsRemove,
               CourseName = x.Courses.Name,
           }).AsNoTracking().ToList();
       }

       public EditAfterCourseViewModel GetDetails(long id)
       {
           return _shopContext.AfterTheCourses.Select(x => new EditAfterCourseViewModel()
           {
               Title = x.Title,
               Id = x.Id,
               CourseId = x.CourseId
           }).FirstOrDefault(x => x.Id == id);
       }
   }
}
