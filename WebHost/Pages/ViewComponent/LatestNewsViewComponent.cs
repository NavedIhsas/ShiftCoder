using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.Comment;

namespace WebHost.Pages.ViewComponent
{
    public class LatestNewsViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICommentQuery _repository;

        public LatestNewsViewComponent(ICommentQuery repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var news = _repository.GetAllNews();
            return View("Default", news);
        }
    }
}
