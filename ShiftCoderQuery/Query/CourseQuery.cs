using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure.EfCore;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Contract.Course;
using Shop.Management.Application.Contract.AfterCourse;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.CoursePrerequisite;
using Shop.Management.Application.Contract.CourseSuitable;
using Shop.Management.Application.Contract.OrderDetail;
using Shop.Management.Application.Contract.UserCourse;
using ShopManagement.Domain.AfterTheCourseAgg;
using ShopManagement.Domain.CourseEpisodeAgg;
using ShopManagement.Domain.CoursePrerequisiteAgg;
using ShopManagement.Domain.CourseSuitableAgg;
using ShopManagement.Domain.OrderDetailAgg;
using ShopManagement.Domain.UserCoursesAgg;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    // ReSharper disable  CommentTypo
    public class CourseQuery : ICourseQuery
    {
        private readonly ShopContext _context;
        private readonly CommentContext _comment;
        private readonly AccountContext _account;
        private readonly IAccountRepository _accountRepository;
        private readonly IVisitRepository _visit;


        public CourseQuery(ShopContext context, CommentContext comment, AccountContext account, IAccountRepository accountRepository, IVisitRepository visit)
        {
            _context = context;
            _comment = comment;
            _account = account;
            _accountRepository = accountRepository;
            _visit = visit;
        }

        public List<GetAllCourseQueryModel> GetAllCourse(CourseQuerySearchModel searchQuery, List<long> group)
        {
            var query = _context.Courses
                .Include(x => x.UserCourses)
                .Include(x => x.CourseEpisodes).
                AsNoTracking().AsEnumerable().Select(x => new GetAllCourseQueryModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    ShortDescription = x.ShortDescription,
                    File = x.File,
                    Price = x.Price,
                    Code = x.Code,
                    UpdateDate = x.UpdateDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Id = x.Id,
                    TeacherId = x.TeacherId,
                    UserCourse = MapUserCourse(x.UserCourses),
                    CreationDate = x.CreationDate,
                    CourseGroupId = x.CourseGroupId,
                    TotalTime = new TimeSpan(x.CourseEpisodes.Sum(t => t.Time.Ticks))
                }).ToList();

            foreach (var item in query)
            {
                var comments = _comment.Comments.Where(x => x.OwnerRecordId == item.Id && x.Type == CommentType.Course).ToList();
                item.Comments = comments;

                var teacher = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == item.TeacherId);
                if (teacher == null) return null;
                item.TeacherName = teacher.Account.FullName;

            }

            if (!string.IsNullOrWhiteSpace(searchQuery.Name))
                query = query.Where(x => x.Name.ToLower().Trim().Contains(searchQuery.Name.ToLower().Trim())).ToList();


            if (!string.IsNullOrWhiteSpace(searchQuery.Filter))
            {
                query = searchQuery.Filter switch
                {
                    "maxPrice" => query.OrderByDescending(x => x.Price).ToList(),
                    "newest" => query.OrderByDescending(x => x.CreationDate).ToList(),
                    "minPrice" => query.OrderBy(x => x.Price).ToList(),
                    //"price"=>query.Where(x=>x.Price>0).ToList(), 
                    "free" => query.Where(x => x.Price == 0).ToList(),
                    //"all"=>query,
                    _ => query
                };
            }


            //or 

            if (group.Count != 0)
            {
                query = @group.Aggregate(query, (current, courseGroup) => current.Where(x => x.CourseGroupId == courseGroup).ToList());
            }
            return query;
        }

        public List<GetAllCourseQueryModel> LatestCourses()
        {
            var course = _context.Courses.Include(x => x.CourseEpisodes).
                Include(x => x.UserCourses).
                AsNoTracking().AsEnumerable().
                Select(x => new GetAllCourseQueryModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    ShortDescription = x.ShortDescription,
                    File = x.File,
                    Price = x.Price,
                    Code = x.Code,
                    UpdateDate = x.UpdateDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    TeacherId = x.TeacherId,
                    TotalTime = new TimeSpan(x.CourseEpisodes.Sum(t => t.Time.Ticks)),
                    UserCourse = MapUserCourse(x.UserCourses),

                }).OrderByDescending(x => x.CreationDate).Take(8).ToList();

            foreach (var item in course)
            {
                var comments = _comment.Comments.Where(x => x.OwnerRecordId == item.Id && x.Type == CommentType.Course).ToList();
                item.Comments = comments;
                var teacher = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == item.TeacherId);
                if (teacher == null) return null;
                item.TeacherName = teacher.Account.FullName;

            }

            return course;
        }

        public List<GetAllCourseQueryModel> PopularCourses()
        {
            var popular = _context.Courses.Include(x => x.CourseEpisodes)
                .Include(x => x.UserCourses)
                .Include(x => x.OrderDetails).
                AsNoTracking().AsEnumerable().
                Select(x => new GetAllCourseQueryModel
                {
                    TeacherId = x.TeacherId,
                    Name = x.Name,
                    Description = x.Description,
                    ShortDescription = x.ShortDescription,
                    File = x.File,
                    Price = x.Price,
                    Code = x.Code,
                    UpdateDate = x.UpdateDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Id = x.Id,
                    OrderDetails = x.OrderDetails,
                    CreationDate = x.CreationDate,
                    UserCourse = MapUserCourse(x.UserCourses),
                    TotalTime = new TimeSpan(x.CourseEpisodes.Sum(t => t.Time.Ticks))

                }).OrderByDescending(x => x.OrderDetails.Count).Take(4).ToList();

            foreach (var item in popular)
            {
                var comments = _comment.Comments.Where(x => x.OwnerRecordId == item.Id && x.Type == CommentType.Course).ToList();
                item.Comments = comments;

                var teacher = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == item.TeacherId);
                if (teacher == null) return null;
                item.TeacherName = teacher.Account.FullName;

            }

            return popular;
        }


        public CourseQueryModel GetCourseBySlug(string slug, string ipAddress)
        {
            var course = _context.Courses.Include(x => x.CourseLevel).Include(x => x.CourseStatus)
                .Include(x => x.CourseGroup).Include(x => x.UserCourses)
                .Include(x => x.CourseEpisodes)
                .Select(x => new CourseQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ShortDescription = x.ShortDescription,
                    File = x.File,
                    Price = x.Price,
                    Code = x.Code,
                    UpdateDate = x.UpdateDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    CourseGroupId = x.CourseGroupId,
                    TeacherId = x.TeacherId,
                    PosterImg = x.DemoVideoPoster,
                    CreationDate = x.CreationDate,
                    CourseGroup = x.CourseGroup.Title,
                    CourseStatus = x.CourseStatus.Title,
                    CourseLevel = x.CourseLevel.Title,
                    UserCourse = MapUserCourse(x.UserCourses),
                    SuitableCourse = MapSuitable(x.CourseSuitableList),
                    AfterCourse = MapAfterCourse(x.AfterTheCourses),
                    PrerequisiteCourse = MapPrerequisiteCourse(x.CoursePrerequisites),
                    EpisodeCourse = MapEpisodeCourse(x.CourseEpisodes),
                    EpisodeCount = x.CourseEpisodes.Count
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (course == null) return null;

            #region Visit

            var visit = _visit.GetUsedBy(ipAddress,VisitType.Course,course.Id);

            if (visit != null && visit.LastVisitDateTime.Date !=DateTime.Now)
            {
                visit.ReduceVisit(visit.NumberOfVisit);
                visit.SetDateTime();
                _visit.SaveChanges();
            }
            else if(visit==null)
            {
                visit = new Visit(VisitType.Course, ipAddress, DateTime.Now, 1,course.Id);
                _visit.Create(visit);
                _visit.SaveChanges();
            }
            course.VisitCount = _visit.GetNumberOfVisit(VisitType.Course, course.Id);

            #endregion

            #region Teacher

            var teacher = _account.Teachers.Include(x => x.Account).FirstOrDefault(x => x.Id == course.TeacherId);
            course.TeacherBio = teacher?.Bio;
            course.TeacherName = teacher?.Account.FullName;
            course.TeacherResume = teacher?.Resumes;
            course.TeacherSkills = teacher?.Skills;

            //barrase kon k en rahe dorst ast ya na?
            course.CourseTeacher = _context.Courses.Where(x => x.TeacherId == teacher.Id).Select(selector: x => new { x.Name, x.Slug }).
                Select(x => new CourseQueryModel() { Name = x.Name, Slug = x.Slug }).ToList();

            #endregion

            #region Comment

            var comment = _comment.Comments
                .Where(x => x.Type == 1).Where(x => x.IsConfirmed).Where(x => x.OwnerRecordId == course.Id && x.ParentId == null)
                .Select(x => new CommentQueryModel
                {
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    Id = x.Id,
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).AsNoTracking().ToList();

            foreach (var item in comment)
                MapChildren(item);

            course.Comments = comment;
            var comments = _comment.Comments.Where(x => x.OwnerRecordId == course.Id && x.Type == CommentType.Course).ToList();
            course.CommentList = comments;

            #endregion

            return course;
        }

        public bool UserInCourse(string email, long courseId)
        {
            var userId = _accountRepository.GetUserIdBy(email);
            return _context.UserCourses.Any(x => x.AccountId == userId && x.CourseId == courseId);
        }

        public CourseEpisode GetEpisodeFile(long episodeId)
            => _context.CourseEpisodes.FirstOrDefault(x => x.Id == episodeId);


        #region Mapping Single Course

        private static List<UserCourseViewModel> MapUserCourse(IEnumerable<UserCourse> userCourses)
        {
            return userCourses.Select(x => new UserCourseViewModel()
            {
                CourseId = x.CourseId,
                AccountId = x.AccountId,
            }).ToList();
        }

      

        private void MapChildren(CommentQueryModel parent)
        {
            var subComment = _comment.Comments
                .Where(x => x.Type == 1).Where(x => x.ParentId == parent.Id)
                .Where(x => x.IsConfirmed)
                .Select(x => new CommentQueryModel
                {
                    Name = x.Name,
                    Email = x.Email,
                    Message = x.Message,
                    OwnerRecordId = x.OwnerRecordId,
                    Id = x.Id,
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).AsNoTracking().ToList();

            foreach (var item in subComment)
            {
                MapChildren(item);
                parent.SubComment.Add(item);
            }
        }


        private static List<CourseEpisodeViewModel> MapEpisodeCourse(IEnumerable<CourseEpisode> courseEpisodes)
        {
            return courseEpisodes.Select(x => new CourseEpisodeViewModel
            {
                FileName = x.FileName,
                Time = x.Time,
                Title = x.Title,
                IsFree = x.IsFree,
                CourseId = x.CourseId,
                Id = x.Id
            }).ToList();
        }

        private static List<CoursePrerequisiteViewModel> MapPrerequisiteCourse(IEnumerable<CoursePrerequisite> coursePrerequisites)
        {
            return coursePrerequisites.Where(x => !x.IsRemove).Select(x => new CoursePrerequisiteViewModel()
            {
                Title = x.Title,
                Id = x.Id
            }).ToList();
        }

        private static List<AfterCourseViewModel> MapAfterCourse(IEnumerable<AfterTheCourse> afterTheCourses)
        {
            return afterTheCourses.Where(x => !x.IsRemove).Select(x => new AfterCourseViewModel()
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
        }

        private static List<CourseSuitableViewModel> MapSuitable(IEnumerable<CourseSuitable> courseSuitableList)
        {
            return courseSuitableList.Where(x => !x.IsRemove).Select(x => new CourseSuitableViewModel()
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
        }

        #endregion
    }
}
