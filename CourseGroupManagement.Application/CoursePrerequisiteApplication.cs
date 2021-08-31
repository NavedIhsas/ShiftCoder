using System.Collections.Generic;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CoursePrerequisite;
using ShopManagement.Domain.CoursePrerequisiteAgg;

namespace ShopManagement.Application
{
    public class CoursePrerequisiteApplication : ICoursePrerequisiteApplication
    {
        private readonly IPrerequisiteRepository _repository;

        public CoursePrerequisiteApplication(IPrerequisiteRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Create(CreateCoursePrerequisiteViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Title == command.Title.Trim()))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var create = new CoursePrerequisite(command.Title,command.CourseId);
            _repository.Create(create);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCoursePrerequisiteViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Title == command.Title.Trim() && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var update = _repository.GetById(command.Id);
            if (update == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            update.Edit(command.Title,command.CourseId);
            _repository.Update(update);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public List<CoursePrerequisiteViewModel> GetAll()
        {
            return _repository.GetAllList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var remove = _repository.GetById(id);
            if (remove == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            remove.Remove(id);
            _repository.Update(remove);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var remove = _repository.GetById(id);
            if (remove == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            remove.Restore(id);
            _repository.Update(remove);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCoursePrerequisiteViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

    }
}