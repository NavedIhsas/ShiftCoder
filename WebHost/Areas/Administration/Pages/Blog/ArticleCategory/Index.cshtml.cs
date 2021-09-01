using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.CourseGroup;

namespace WebHost.Areas.Administration.Pages.Blog.ArticleCategory
{
    // ReSharper disable  StringLiteralTypo
    public class IndexModel : PageModel
    {
        private readonly IArticleCategoryApplication _application;
        public IndexModel(IArticleCategoryApplication article)
        {
            _application = article;
        }

        [TempData] public string Message { get; set; }

        public ArticleCategorySearchModel SearchModel;
        public List<ArticleCategoryViewModel> List;
        public void OnGet(ArticleCategorySearchModel searchModel)
        {

            List = _application.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticleCategoryViewModel());
        }

        public JsonResult OnPostCreate(CreateArticleCategoryViewModel command)
        {
            var courseGroup = _application.Create(command);
            return new JsonResult(courseGroup);
        }


        public IActionResult OnGetEdit(long id)
        {
            var courseGroup = _application.GetDetails(id);
            return Partial("./Edit", courseGroup);
        }

        public JsonResult OnPostEdit(EditArticleCategoryViewModel command)
        {
            var courseGroup = _application.Edit(command);
            return new JsonResult(courseGroup);
        }

      
    }
}
