using System.IO;
using System.Linq;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharpCompress.Archives;
using ShiftCoderQuery.Contract.Course;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace WebHost.Pages
{

    public class SingleCourseModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICommentApplication _aaApplication;
        private readonly ICourseRepository _courseRepository;
        private readonly IAccountRepository _account;
        private readonly ICourseEpisodeRepository _episode;
        private readonly INotificationRepository _notification;
        private readonly ICommentRepository _commentRepository;
        private readonly IAuthHelper _authHelper;
        // ReSharper disable  CommentTypo
        public SingleCourseModel(ICourseQuery course, ICommentApplication aaApplication, IAccountRepository account, ICourseEpisodeRepository episode, INotificationRepository notification, ICommentRepository commentRepository, IAuthHelper authHelper, ICourseRepository courseRepository)
        {
            _course = course;
            _aaApplication = aaApplication;
            _account = account;
            _episode = episode;
            _notification = notification;
            _commentRepository = commentRepository;
            _authHelper = authHelper;
            _courseRepository = courseRepository;
        }

      
        public CourseQueryModel Course;
        public IActionResult OnGet(string id, long episode = 0)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Course = _course.GetCourseBySlug(id, ipAddress);

            
            if (episode != 0 && _authHelper.IsAuthenticated())
            {
                //چک کن این فایل مربوط همین کورس است یا خیر
                if (Course.EpisodeCourse.All(x => x.Id != episode))
                    return NotFound();

                //چک کن آیا این فایل رایگان است؟ اگر خیر، چک کن آیا شخص این کورس را خریده؟
                if (!Course.EpisodeCourse.First(x => x.Id == episode).IsFree)
                    if (!_course.UserInCourse(User.Identity.Name, Course.Id)) return NotFound();

                //برو فایل رو بیار
                var episodeFile = Course.EpisodeCourse.First(x => x.Id == episode);
                ViewData["Episode"] = episodeFile;

                //اسلاگ کورس برای یافتن محل فایل
                var courseSlug = _courseRepository.GetCourseSlugBy(Course.Id);

                // open video if exist
                string filePath = "";
                 filePath = $"/FileUploader/EpisodeFiles/{courseSlug}/" + episodeFile.FileName.Replace(".rar", ".mp4");
                 ;

                 //check file for exist
                var episodePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/EpisodeFiles/{courseSlug}",
                    episodeFile .FileName.Replace(".rar",".mp4"));

                //if not exist top file, ran this
                if (!System.IO.File.Exists(episodePath))
                {

                    // get rar file path and name
                    var rarFile = Path.Combine(Directory.GetCurrentDirectory(),
                        $"wwwroot/FileUploader/EpisodeFiles/{courseSlug}",
                        episodeFile.FileName);

                    //open rar file
                    var archive = ArchiveFactory.Open(rarFile);

                    //order by length name
                    var entries = archive.Entries.OrderBy(x => x.Key.Length);
                    foreach (var entry in entries)
                    {
                        //get extension file(.mp4)
                        if (Path.GetExtension(entry.Key) == ".mp4")
                        {
                            //extract file or replace extension from .rar to .mp4
                            entry.WriteTo(System.IO.File.Create(rarFile.Replace(".rar",".mp4")));
                        }

                    }

                }
                ViewData["FilePath"] = filePath;
            }

            return Page();
        }

        public IActionResult OnPost(CreateCommentViewModel command, string productSlug)
        {
            if (_authHelper.IsAuthenticated())
            {
                command.Type = 1;
                var createComment = new Comment(_authHelper.CurrentAccountFullName(), User.Identity.Name, command.Message
                    , command.OwnerRecordId, command.Type, command.ParentId);
                _commentRepository.Create(createComment);
                _commentRepository.SaveChanges();
                return RedirectToPage("SingleCourse", new { id = productSlug });

            }
            else
            {
                command.Type = 1;

                _aaApplication.Create(command);
                return RedirectToPage("SingleCourse", new { id = productSlug });
            }
        }

        public IActionResult OnGetDownloadFile(long id)
        {
           
            var episodeFile = _course.GetEpisodeFile(id);

            var courseSlug = _courseRepository.GetCourseSlugBy(episodeFile.CourseId);
            if (courseSlug == null)
            {
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/EpisodeFiles/{courseSlug}",
                    episodeFile.FileName);


                if (episodeFile.IsFree)
                {
                    byte[] file = System.IO.File.ReadAllBytes(pathFile);
                    var downloadFile = File(file, "application/force-download", episodeFile.FileName);

                    var notification = new Notification($"دانلود فایل رایگان از دورۀ( {episodeFile.Course.Name}).",
                        ThisType.Course, episodeFile.Id);
                    _notification.Create(notification);
                    _notification.SaveChanges();

                    return downloadFile;
                }
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/EpisodeFiles/{courseSlug}",
                episodeFile.FileName);

            if (episodeFile.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                var downloadFile = File(file, "application/force-download", episodeFile.FileName);

                var notification = new Notification($"دانلود فایل رایگان از دورۀ( {episodeFile.Course.Name}).",
                    ThisType.Course, episodeFile.Id);
                _notification.Create(notification);
                _notification.SaveChanges();

                return downloadFile;

            }

            if (!_authHelper.IsAuthenticated()) return Forbid();
            {
                var userInCourse = _course.UserInCourse(User.Identity.Name, episodeFile.CourseId);

                if (!userInCourse) return Forbid();

                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", episodeFile.FileName);
            }

        }

    }
}
