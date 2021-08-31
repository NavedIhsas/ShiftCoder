using System.Collections.Generic;
using _0_Framework.Application;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.Course;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Application
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseRepository _course;
        private readonly IFileUploader _fileUploader;
        private readonly ICourseGroupRepository _courseGroup;
        public CourseApplication(ICourseRepository course, IFileUploader fileUploader, ICourseGroupRepository courseGroup)
        {
            _course = course;
            _fileUploader = fileUploader;
            _courseGroup = courseGroup;
        }
        public OperationResult Create(CreateCourseViewModel command)
        {
            var operation = new OperationResult();

            if (_course.IsExist(x => x.Name == command.Name.Trim() && x.CourseGroupId == command.CourseGroupId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseGroupSlug = _courseGroup.GetSlug(command.CourseGroupId);

            var pictureName = _fileUploader.Uploader(command.Picture, courseGroupSlug);
            var fileName = _fileUploader.Uploader(command.File, courseGroupSlug + "/DemoFile");
            var poster = _fileUploader.Uploader(command.DemoPoster, courseGroupSlug + "/DemoFile");

            var course = new Course(command.Name, command.Description, command.ShortDescription, fileName,
                 command.Price, pictureName, command.PictureAlt, command.PictureTitle, command.KeyWords,
                 command.MetaDescription, command.Slug.Slugify(), command.Code, command.CourseGroupId, command.CourseLevelId, command.CourseStatusId,poster);

            _course.Create(course);
            _course.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseViewModel command)
        {
            var operation = new OperationResult();

            var course = _course.GetById(command.Id);
            if (course == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            var courseGroupSlug = _courseGroup.GetSlug(command.CourseGroupId);
            var pictureName = _fileUploader.Uploader(command.Picture, courseGroupSlug);
            var fileName = _fileUploader.Uploader(command.File, courseGroupSlug + "/DemoFile");
            var poster = _fileUploader.Uploader(command.DemoPoster, courseGroupSlug + "/DemoFile");

            course.Edit(command.Name, command.Description, command.ShortDescription, fileName,
                command.Price, pictureName, command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(), command.Code, command.CourseGroupId, command.CourseLevelId, command.CourseStatusId,poster);

            if (_course.IsExist(x => x.Name == command.Name.Trim() && x.CourseGroupId == command.CourseGroupId &&x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            _course.Update(course);
            _course.SaveChanges();
            return operation.Succeeded();
        }

        public EditCourseViewModel GetDetails(long id)
        {
            return _course.GetDetails(id);
        }

        public List<CourseViewModel> Search(CourseSearchModel searchModel)
        {
            return _course.Search(searchModel);
        }

        public List<CourseViewModel> SelectCourses()
        {
            return _course.SelectCourses();
        }
    }
}
