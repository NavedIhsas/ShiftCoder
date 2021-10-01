using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages.ViewComponent
{
    public class GroupViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICourseGroupQuery _group;

        public GroupViewComponent(ICourseGroupQuery @group)
        {
            _group = @group;
        }

        public IViewComponentResult Invoke()
        {
            var group = _group.GetSixGroup();
            return View("Defautl", group);
        }
    }
}
