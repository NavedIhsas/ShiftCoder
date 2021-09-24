using System;
using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.UserCourse;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace ShiftCoderQuery.Contract.Course
{
    public interface ICourseQuery
    {
        CoursePaginationViewModel GetAllCourse(CourseQuerySearchModel searchQuery, List<string> categories, int pageId = 1);
           
        List<GetAllCourseQueryModel> LatestCourses(string ipAddress);
        List<GetAllCourseQueryModel> PopularCourses();
        CourseQueryModel GetCourseBySlug(string slug,string ipAddress);
        bool UserInCourse(string email, long courseId);
        CourseEpisode GetEpisodeFile(long episodeId);
        List<Account> GetAllUsers();
        double GetTotalMinutesEpisodeVideos();
        List<BlogManagement.Domain.ArticleAgg.Article> GetAllArticle();
        List<Teacher> GetAllTeacher();
        List<UserCourseViewModel> GetUserCourseBy(string email);

    }
}
