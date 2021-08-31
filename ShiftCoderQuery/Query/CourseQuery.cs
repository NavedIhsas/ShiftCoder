using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Course;
using Shop.Management.Application.Contract.AfterCourse;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.CoursePrerequisite;
using Shop.Management.Application.Contract.CourseSuitable;
using ShopManagement.Domain.AfterTheCourseAgg;
using ShopManagement.Domain.CourseEpisodeAgg;
using ShopManagement.Domain.CourseGroupAgg;
using ShopManagement.Domain.CoursePrerequisiteAgg;
using ShopManagement.Domain.CourseSuitableAgg;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    public class CourseQuery : ICourseQuery
    {
        private readonly ShopContext _context;

        public CourseQuery(ShopContext context)
        {
            _context = context;
        }

        public List<CourseQueryModel> GetAllCourse(CourseQuerySearchModel searchQuery, List<long> group)

        {
            var query = _context.Courses.Select(x => new CourseQueryModel
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
                CreationDate = x.CreationDate,
                CourseGroupId = x.CourseGroupId,
            }).AsNoTracking().ToList();


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
                foreach (var courseGroup in group)
                {
                    query = query.Where(x => x.CourseGroupId == courseGroup).ToList();
                }
            }

            var orderly = query.OrderByDescending(x => x.CreationDate).ToList();

            return orderly;
        }


        public List<CourseQueryModel> LatestCourses()
        {
            return _context.Courses.Include(x => x.CourseEpisodes).
                AsNoTracking(). AsEnumerable().
                Select(x => new CourseQueryModel
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
                    CreationDate = x.CreationDate,
                    TotalTime = new TimeSpan(x.CourseEpisodes.Sum(x => x.Time.Ticks))

                }).OrderByDescending(x => x.CreationDate).Take(8).ToList();
        }



        public CourseQueryModel GetCourseBySlug(string slug)
        {
            return _context.Courses.Include(x => x.CourseLevel).Include(x => x.CourseStatus).Include(x => x.CourseGroup)
                .Include(x => x.CourseEpisodes)
                .Select(x => new CourseQueryModel
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
                    PosterImg = x.DemoVideoPoster,
                    CreationDate = x.CreationDate,
                    CourseGroup = x.CourseGroup.Title,
                    CourseStatus = x.CourseStatus.Title,
                    CourseLevel = x.CourseLevel.Title,
                    SuitableCourse = MapSuitable(x.CourseSuitableList),
                    AfterCourse = MapAfterCourse(x.AfterTheCourses),
                    PrerequisiteCourse = MapPrerequisiteCourse(x.CoursePrerequisites),
                    EpisodeCourse = MapEpisodeCourse(x.CourseEpisodes),
                    EpisodeCount = x.CourseEpisodes.Count
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);
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
    }
}
