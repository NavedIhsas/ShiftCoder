using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebHost.Areas.Administration.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly CommentContext _context;

        public IndexModel(CommentContext context)
        {
            _context = context;
        }

        public List<CommentManagement.Domain.HomePageDetailsAgg.News> List;
        public void OnGet()
        {
            List = _context.News.Take(6).OrderByDescending(x => x.CreationDate).ToList();
        }

        [NeedPermission(Permission.CreateLatestNews)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CommentManagement.Domain.HomePageDetailsAgg.News());
        }
        public JsonResult OnPostCreate(CommentManagement.Domain.HomePageDetailsAgg.News command)
        {
           var create= _context.Add(command);
            _context.SaveChanges();
            return new JsonResult(create);
        }

        [NeedPermission(Permission.EditLatestNews)]
        public IActionResult OnGetEdit(long id)
        {
            var news= _context.News.FirstOrDefault(x => x.Id == id);
            return Partial("./Edit", news);
        }

        public JsonResult OnPostEdit(CommentManagement.Domain.HomePageDetailsAgg.News command)
        {
            var update=_context.News.Update(command);
            _context.SaveChanges();
            return new JsonResult(update);
        }
    }
}
