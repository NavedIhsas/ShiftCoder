using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using _0_Framework.Application;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseGroup;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Application
{
    public class CourseGroupApplication : ICourseGroupApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ICourseGroupRepository _repository;

        public CourseGroupApplication(ICourseGroupRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            //resize to 400 X 250
            #region 600 X 400

            var image = Image.FromStream(command.Picture.OpenReadStream());
            var resized = new Bitmap(image, new Size(400, 250));

            using var imageStream = new MemoryStream();
            resized.Save(imageStream, ImageFormat.Jpeg);
            var imageBytes = imageStream.ToArray();

            var imgName = $"{DateTime.Now.Date.ToFileName()}-{command.Picture.FileName}";
            var path1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/CourseGroup/", imgName);

            using var streamImg = new FileStream(
                path1, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
            streamImg.Write(imageBytes, 0, imageBytes.Length);

            #endregion

            var courseGroup = new CourseGroup(command.Title, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(), command.SubGroupId, imgName, command.PictureAlt,
                command.PictureTitle);

            _repository.Create(courseGroup);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseGroup = _repository.GetById(command.Id);
            if (command.Picture != null)
            {
                var deletePath = $"wwwroot/FileUploader/CourseGroup/{courseGroup.Picture}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            if (courseGroup == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            var imgName = $"{DateTime.Now.Date.ToFileName()}-{command.Picture?.FileName}";
            if (command.Picture != null)
            {
                //resize to 500 X 600
                #region 400 X 250

                var image = Image.FromStream(command.Picture.OpenReadStream());
                var resized = new Bitmap(image, new Size(400, 250));

                using var imageStream = new MemoryStream();
                resized.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();

                var path1 = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/CourseGroup/", imgName);

                using var streamImg = new FileStream(
                    path1, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                streamImg.Write(imageBytes, 0, imageBytes.Length);

                #endregion
            }

            courseGroup.Edit(command.Title, command.KeyWords,
                command.MetaDescription, command.Slug, command.SubGroupId, imgName, command.PictureAlt,
                command.PictureTitle);

            _repository.Update(courseGroup);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var courseGroup = _repository.GetById(id);
            if (courseGroup == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            courseGroup.Remove();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var courseGroup = _repository.GetById(id);
            if (courseGroup == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            courseGroup.Restore();
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CourseGroupViewModel> SelectList()
        {
            return _repository.SelectList();
        }

        public EditCourseGroupViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<CourseGroupViewModel> Search(CourseGroupSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }
    }
}