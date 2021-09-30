using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.CourseGroup
{
    public interface ICourseGroupQuery
    {
        List<CourseGroupQueryModel> GetAllCourseGroup();
        List<CourseGroupQueryModel> SearchQuery(CourseGroupSearchQuery categories);
        List<LatestCourseGroupViewModel> LatestCourseGroup();

    }
}
