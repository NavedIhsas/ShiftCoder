using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;

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
        public void OnGet(string course)
        { 
            
           // Episodes = _course.GetAllEpisodes();
        }





       
      
        public JsonResult UploadImageOnPost(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/FileUploader/SaveFromCDN",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return new JsonResult(new { uploaded = true, url });
        }

    }
}
