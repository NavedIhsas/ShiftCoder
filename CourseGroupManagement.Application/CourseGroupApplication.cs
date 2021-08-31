using System;
using System.Collections.Generic;
using _0_Framework.Application;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseGroup;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Application
{
    public class CourseGroupApplication:ICourseGroupApplication
    {
        private readonly ICourseGroupRepository _repository;

        public CourseGroupApplication(ICourseGroupRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Create(CreateCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title))
                return operation.Failed( ApplicationMessage.DuplicatedRecord);

            var courseGroup = new CourseGroup(command.Title, command.Description, command.KeyWords,
                command.MetaDescription, command.Slug.Slugify(),command.SubGroupId);

            _repository.Create(courseGroup);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCourseGroupViewModel command)
        {
            var operation = new OperationResult();

            if (_repository.IsExist(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseGroup = _repository.GetById(command.Id);
            if (courseGroup == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            courseGroup.Edit(command.Title, command.Description, command.KeyWords,
                command.MetaDescription, command.Slug,command.SubGroupId);

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
