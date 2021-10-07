using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CommentManagement.Domain.SliderAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using ShopManagement.Domain.CourseAgg;

namespace WebHost.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ICommentQuery _slider;
        private readonly ILogger<IndexModel> _logger;
        public List<Course> Courses;
        public long Episodes;
        public CourseQuerySearchModel SearchModel;
        public List<Slider> Sliders;

        public IndexModel(ICommentQuery slider, ILogger<IndexModel> logger)
        {
            _slider = slider;
            _logger = logger;
        }

        public void OnGet()
        {
            Sliders = _slider.GetThreeSlider();
        }

        public IActionResult OnGetNotFountPage()
        {
            return NotFound();
        }

        public async Task<JsonResult> OnPostUploadImage([FromForm] IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            //your custom code logic here

            //1)check if the file is image

            //2)check if the file is too large

            //etc

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            //save file under wwwroot/CKEditorImages folder

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/FileUploader/CKEditorImages",
                fileName);

            await using (var stream = System.IO.File.Create(filePath))
            {
                await upload.CopyToAsync(stream);
            }

            var url = $"{"/FileUploader/CKEditorImages/"}{fileName}";

            var success = new UploadSuccess
            {
                Uploaded = 1,
                FileName = fileName,
                Url = url
            };

            return new JsonResult(success);
        }

    }
}

public class UploadSuccess
{
    public int Uploaded { get; set; }
    public string FileName { get; set; }
    public string Url { get; set; }
}