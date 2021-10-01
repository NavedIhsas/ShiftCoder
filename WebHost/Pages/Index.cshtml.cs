using System;
using System.Collections.Generic;
using System.IO;
using CommentManagement.Domain.SliderAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using ShopManagement.Domain.CourseAgg;

namespace WebHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICommentQuery _slider;

        public IndexModel(ICourseQuery course, ICommentQuery slider)
        {
            _course = course;
            _slider = slider;
        }

        public List<Course> Courses;
        public List<Slider> Sliders;
        public CourseQuerySearchModel SearchModel;
        public long Episodes;
        public void OnGet()
        {
            Sliders = _slider.GetThreeSlider();
        }


        public JsonResult UploadImageOnPost(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/FileUploader/SaveFromCDN",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
                upload.CopyTo(stream);

            var url = $"{"/MyImages/"}{fileName}";
            return new JsonResult(new { uploaded = true, url });
        }

        public IActionResult OnGetNotFountPage()
        {
            return NotFound();
        }
    }
}
