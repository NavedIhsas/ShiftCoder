using System.Collections.Generic;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Query;

namespace WebHost.Pages
{
    public class SingleCourseModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICommentQuery _comment;
        private readonly ICommentApplication _aaApplication;
     

        public SingleCourseModel(ICourseQuery course, ICommentQuery comment, ICommentApplication aaApplication)
        {
            _course = course;
            _comment = comment;
            _aaApplication = aaApplication;
        }


        public CourseQueryModel Course;
        public void OnGet(string id)
        {
            Course = _course.GetCourseBySlug(id);
        }

        public IActionResult OnPost(CreateCommentViewModel command,string productSlug)
        {
            command.Type = 1;
            var post = _aaApplication.Create(command);
            return RedirectToPage("SingleCourse", new { id = productSlug });
        }

    }
}
