using System;
using System.Collections.Generic;
using System.IO;
using _0_Framework.Application;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseGroup;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Application
{
    public class CourseGroupApplication:ICourseGroupApplication
    {
        private readonly ICourseGroupRepository _repository;
        private readonly IFileUploader _fileUploader;

        public CourseGroupApplication(ICourseGroupRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title))
                return operation.Failed( ApplicationMessage.DuplicatedRecord);

            var filePath = $"CourseGroup/{command.Slug.Slugify()}";
            var fileName = _fileUploader.Uploader(command.Picture, filePath);

            var courseGroup = new CourseGroup(command.Title, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(),command.SubGroupId, fileName, command.PictureAlt,command.PictureTitle);

            _repository.Create(courseGroup);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title&&x.Id !=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseGroup = _repository.GetById(command.Id);

            if (command.Picture != null)
            {
                var deletePath = $"wwwroot/FileUploader/{courseGroup.Picture}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            if (courseGroup == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            var filePath = $"CourseGroup/{command.Slug.Slugify()}";
            var fileName = _fileUploader.Uploader(command.Picture, filePath);

            courseGroup.Edit(command.Title,command.KeyWords,
                command.MetaDescription, command.Slug,command.SubGroupId, fileName, command.PictureAlt, command.PictureTitle);

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

        public EditCourseGroupViewModel GetDetails(long id) => _repository.GetDetails(id);
       
        public List<CourseGroupViewModel> Search(CourseGroupSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

      
    }
}
