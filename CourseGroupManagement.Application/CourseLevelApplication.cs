using System.Collections.Generic;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseLevel;
using ShopManagement.Domain.CourseLevelAgg;

namespace ShopManagement.Application
{
    public class CourseLevelApplication : ICourseLevelApplication
    {
        private readonly ICourseLevelRepository _repository;

        public CourseLevelApplication(ICourseLevelRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateCourseLevelViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseLevel = new CourseLevel(command.Title);

            _repository.Create(courseLevel);
            _repository.SaveChanges();
            return operation.Succeeded();
        }


        public OperationResult Edit(EditCourseLevelViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var courseLevel = _repository.GetById(command.Id);
            if (courseLevel == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            courseLevel.Edit(command.Title);
            _repository.Update(courseLevel);
            _repository.SaveChanges();
            return operation.Succeeded();
        }


        public List<CourseLevelViewModel> GetAll()
        {
            return _repository.GetAllCourseLevel();
        }

        public List<CourseLevelViewModel> SelectList()
        {
            return _repository.SelectList();
        }

        public EditCourseLevelViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}