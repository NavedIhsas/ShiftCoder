using System.Collections.Generic;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseEpisode
{
   public interface ICourseEpisodeApplication
   {
       OperationResult Create(CreateCourseEpisodeViewModel command);
       OperationResult Edit(EditCourseEpisodeViewModel command);
       EditCourseEpisodeViewModel GetDetails(long id);
       List<CourseEpisodeViewModel> Search(CourseEpisodeSearchModel command);
   }
}
