using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;
using Shop.Management.Application.Contract.CourseEpisode;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace WebHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICourseQuery _course;

        public IndexModel(ICourseQuery course)
        {
            _course = course;
        }

        public long Episodes;
        public void OnGet()
        {
           // Episodes = _course.GetAllEpisodes();
        }

    }
}
