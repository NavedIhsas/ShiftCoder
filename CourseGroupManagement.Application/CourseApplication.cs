using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        private readonly ICourseGroupRepository _courseGroup;
        private readonly IFileUploader _fileUploader;

        public CourseApplication(ICourseRepository course, IFileUploader fileUploader,
            ICourseGroupRepository courseGroup)
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

           
            //get slug for path
            var courseGroupSlug = _courseGroup.GetSlug(command.CourseGroupId);

            //path
            var pictureName = _fileUploader.Uploader(command.Picture, $"/Courses/{courseGroupSlug}");
            var fileName = _fileUploader.Uploader(command.File, courseGroupSlug + "/DemoFile");
            var poster = _fileUploader.Uploader(command.DemoPoster, courseGroupSlug + "/DemoFile");

            ////resize image
            if (command.Picture != null)
            {
                //resize to 600 X 400
                #region 600 X 400

                var image = Image.FromStream(command.Picture.OpenReadStream());
                var resized = new Bitmap(image, new Size(600, 400));

                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();

                var imgName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/", imgName);

                using var streamImg = new FileStream(
                    path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                streamImg.Write(imageBytes, 0, imageBytes.Length);

                #endregion

                //resize to 80 X 80
                #region 80 x 80

                var img = Image.FromStream(command.Picture.OpenReadStream());
                var resizedImg = new Bitmap(img, new Size(80, 80));

                using var imgStream = new MemoryStream();
                resizedImg.Save(imgStream, ImageFormat.Jpeg);
                var imgBytes = imgStream.ToArray();

                var imageName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/80X80", imageName);

                using var stream = new FileStream(
                    imgPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                stream.Write(imgBytes, 0, imgBytes.Length);

                #endregion
            }

            //create
            var course = new Course(command.Name, command.Description, command.ShortDescription, fileName,
                command.Price, pictureName, command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(), command.Code, command.CourseGroupId,
                command.CourseLevelId, command.CourseStatusId, poster, command.TeacherId,command.CanonicalAddress);

            _course.Create(course);
            _course.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseViewModel command)
        {
            var operation = new OperationResult();

            var course = _course.GetById(command.Id);
            if (course == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            //delete current file
            if (command.Picture != null)
            {
                var coursePictureDelete = $"wwwroot/FileUploader/{course.Picture}";
                if (File.Exists(coursePictureDelete))
                    File.Delete(coursePictureDelete);
            }

            if (command.File != null)
            {
                var demoDelete = $"wwwroot/FileUploader/{course.File}";
                if (File.Exists(demoDelete))
                    File.Delete(demoDelete);
            }

            if (command.DemoPoster != null)
            {
                var posterDelete = $"wwwroot/FileUploader/{course.DemoVideoPoster}";
                if (File.Exists(posterDelete))
                    File.Delete(posterDelete);
            }

            //get group slug
            var courseGroupSlug = _courseGroup.GetSlug(command.CourseGroupId);

            //get path for save file
            var pictureName = _fileUploader.Uploader(command.Picture,$"/Courses/{courseGroupSlug}");
            var fileName = _fileUploader.Uploader(command.File, courseGroupSlug + "/DemoFile");
            var poster = _fileUploader.Uploader(command.DemoPoster, courseGroupSlug + "/DemoFile");


            ////resize image
            if (command.Picture != null)
            {
                //resize to 600 x 400
                #region 600 X400

                var image = Image.FromStream(command.Picture.OpenReadStream());
                var resized = new Bitmap(image, new Size(600, 400));

                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();

                var imgName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/", imgName);

                using var stream = new FileStream(
                    path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                stream.Write(imageBytes, 0, imageBytes.Length);

                #endregion

                //resize to 80 X 80
                #region 80 x 80

                var img = Image.FromStream(command.Picture.OpenReadStream());
                var resizedImg = new Bitmap(img, new Size(80, 80));

                using var imgStream = new MemoryStream();
                resizedImg.Save(imgStream, ImageFormat.Jpeg);
                var imgBytes = imgStream.ToArray();

                var imageName = $"{DateTime.Now.ToFileName()}-{command.Picture.FileName}";
                var imgPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/Thumb/80X80", imageName);

                using var streamImg = new FileStream(
                    imgPath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                streamImg.Write(imgBytes, 0, imgBytes.Length);

                #endregion
            }

            //edit
            course.Edit(command.Name, command.Description, command.ShortDescription, fileName,
                command.Price, pictureName, command.PictureAlt, command.PictureTitle, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(), command.Code, command.CourseGroupId,
                    command.CourseLevelId, command.CourseStatusId,poster, command.TeacherId,command.CanonicalAddress);

            //check duplicate course
            if (_course.IsExist(x =>
                x.Name == command.Name.Trim() && x.CourseGroupId == command.CourseGroupId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            //update and save change 
            _course.Update(course);
            _course.SaveChanges();
            return operation.Succeeded(); }

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