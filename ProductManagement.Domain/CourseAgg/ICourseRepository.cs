using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.Course;

namespace ShopManagement.Domain.CourseAgg
{
    public interface ICourseRepository:IRepository<long,Course>
    {
        EditCourseViewModel GetDetails(long id);
        List<ArticleViewModel> Search(CourseSearchModel searchModel);
        List<ArticleViewModel> SelectCourses();
    }
}
