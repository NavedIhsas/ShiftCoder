using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using Shop.Management.Application.Contract.AfterCourse;
using ShopManagement.Domain.AfterTheCourseAgg;

namespace ShopManagement.Application
{
   public class AfterTheCourseApplication:IAfterCourseApplication
   {
       private readonly IAfterTheCourseRepository _repository;

       public AfterTheCourseApplication(IAfterTheCourseRepository repository)
       {
           _repository = repository;
       }
       public OperationResult Create(CreateAfterCourseViewModel command)
       {
           var operation = new OperationResult();
          
           var create = new AfterTheCourse(command.Title,command.CourseId);
            _repository.Create(create);
            _repository.SaveChanges();
            return operation.Succeeded();
       }

        public OperationResult Edit(EditAfterCourseViewModel command)
        {
            var operation = new OperationResult();
           
            var update = _repository.GetById(command.Id);
            if (update == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            update.Edit(command.Title,command.CourseId);
            _repository.Update(update);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAfterCourseViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<AfterCourseViewModel> GetAll()
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
   }

    
}
