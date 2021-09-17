using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.UserCourse;
using ShopManagement.Domain.CourseEpisodeAgg;
using ShopManagement.Domain.UserCoursesAgg;

namespace ShiftCoderQuery.Contract.Course
{
    public interface ICourseQuery
    {
        CoursePaginationViewModel GetAllCourse(CourseQuerySearchModel searchQuery, List<long> groupId, int pageId = 1);
           
        List<GetAllCourseQueryModel> LatestCourses(string ipAddress);
        List<GetAllCourseQueryModel> PopularCourses();
        CourseQueryModel GetCourseBySlug(string slug,string ipAddress);
        bool UserInCourse(string email, long courseId);
        CourseEpisode GetEpisodeFile(long episodeId);
        List<Account> GetAllUsers();
        double GetAllEpisodes();
        List<BlogManagement.Domain.ArticleAgg.Article> GetAllArticle();
        List<Teacher> GetAllTeacher();
        List<UserCourseViewModel> GetUserCourseBy(string email);
     
    }
}
