using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseStatus;
using ShopManagement.Domain.CourseStatusAgg;

namespace ShopManagement.Application
{
   public class CourseStatusApplication:ICourseStatusApplication
   {
       private readonly ICourseStatusRepository _repository;

       public CourseStatusApplication(ICourseStatusRepository repository)
       {
           _repository = repository;
       }

       public OperationResult Create(CourseStatusViewModel command)
       {
           var operation = new OperationResult();
           if (_repository.IsExist(x => x.Title == command.Title))
               return operation.Failed(ApplicationMessage.DuplicatedRecord);

           var courseStatus = new CourseStatus(command.Title);

            _repository.Create(courseStatus);
            _repository.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditCourseStatusViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Title == command.Title && x.Id !=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseStatus = _repository.GetById(command.Id);
            if (courseStatus == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            courseStatus.Edit(command.Title);
            _repository.Update(courseStatus);
            _repository.SaveChanges();
            return operation.Succeeded();

        }


        public List<CourseStatusViewModel> GetAll()
        {
            return _repository.GetAllCourseStatus();
        }

        public List<CourseStatusViewModel> SelectList()
        {
            return _repository.SelectList();
        }

        public EditCourseStatusViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

    }
}
