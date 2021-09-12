using System.Collections.Generic;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace ShiftCoderQuery.Contract.Course
{
    public interface ICourseQuery
    {
        List<GetAllCourseQueryModel> GetAllCourse(CourseQuerySearchModel searchQuery, List<long> groupId);
        List<GetAllCourseQueryModel> LatestCourses();
        List<GetAllCourseQueryModel> PopularCourses();
        CourseQueryModel GetCourseBySlug(string slug);

        bool UserInCourse(string email, long courseId);

        CourseEpisode GetEpisodeFile(long episodeId);


    }
}
