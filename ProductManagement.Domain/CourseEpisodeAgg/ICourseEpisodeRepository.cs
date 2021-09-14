using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.CourseEpisode;

namespace ShopManagement.Domain.CourseEpisodeAgg
{
    public interface ICourseEpisodeRepository:IRepository<long,CourseEpisode>
    {
        EditCourseEpisodeViewModel GetDetails(long id);
        List<CourseEpisodeViewModel> Search(CourseEpisodeSearchModel command);
        string GetCourseGroupSlugBy(long courseId);
        CourseEpisodeViewModel GetEpisodeIdBy(long courseId);
    }
}