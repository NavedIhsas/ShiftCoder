using System.Collections.Generic;
using System.IO;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Query;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace WebHost.Pages
{
   
    public class SingleCourseModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICommentApplication _aaApplication;
        private readonly IAccountRepository _account;
        private readonly ICourseEpisodeRepository _episode;
        private readonly INotificationRepository _notification;
        private readonly ICommentRepository _commentRepository;
        private readonly IAuthHelper _authHelper;

        public SingleCourseModel(ICourseQuery course, ICommentApplication aaApplication, IAccountRepository account, ICourseEpisodeRepository episode, INotificationRepository notification, ICommentRepository commentRepository, IAuthHelper authHelper)
        {
            _course = course;
            _aaApplication = aaApplication;
            _account = account;
            _episode = episode;
            _notification = notification;
            _commentRepository = commentRepository;
            _authHelper = authHelper;
        }


        public CourseQueryModel Course;
        public void OnGet(string id)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Course = _course.GetCourseBySlug(id,ipAddress);
        }

        public IActionResult OnPost(CreateCommentViewModel command,string productSlug)
        {
            if (_authHelper.IsAuthenticated())
            {
                command.Type = 1;
                var createComment= new Comment(_authHelper.CurrentAccountFullName(), User.Identity.Name, command.Message
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
          var user= _account.GetUserBy(User.Identity.Name);
          
            var episode = _course.GetEpisodeFile(id);
        
            var getGroupSlug = _episode.GetCourseGroupSlugBy(episode.CourseId);
            if (getGroupSlug == null)
            {
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/EpisodeFiles",
                    episode.FileName);


                if (episode.IsFree)
                {
                    byte[] file = System.IO.File.ReadAllBytes(pathFile);
                    var downloadFile = File(file, "application/force-download", episode.FileName);

                    var notification = new Notification($"دانلود فایل رایگان از دورۀ( {episode.Course.Name}).",
                        ThisType.Course, episode.Id);
                    _notification.Create(notification);
                    _notification.SaveChanges();

                    return downloadFile;


                }
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/{getGroupSlug}/EpisodeFiles",
                episode.FileName);

            if (episode.IsFree)
            {
                byte[] file = System.IO.File.ReadAllBytes(filePath);
               var downloadFile= File(file, "application/force-download", episode.FileName);

                var notification = new Notification($"دانلود فایل رایگان از دورۀ( {episode.Course.Name}).",
                    ThisType.Course, episode.Id);
                _notification.Create(notification);
                _notification.SaveChanges();

                return downloadFile;

            }

            if (!User.Identity.IsAuthenticated) return Forbid();
            {
                var userInCourse = _course.UserInCourse(User.Identity.Name, episode.CourseId);

                if (!userInCourse) return Forbid();

                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", episode.FileName);
            }

        }

    }
}
