using System.Collections.Generic;
using _0_FrameWork.Application;
using ColleagueDiscountManagementApplication.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
   public class ColleagueApplication:IColleagueApplication
   {
       private readonly IColleagueRepository _colleague;

       public ColleagueApplication(IColleagueRepository colleague)
       {
           _colleague = colleague;
       }
       public OperationResult Create(CreateColleagueDiscountViewModel command)
       {
           var operation = new OperationResult();

           if (_colleague.IsExist(x => x.CourseId == command.CourseId && x.DiscountRate == command.DiscountRate))
               return operation.Failed(ApplicationMessage.DuplicatedRecord);

           var colleague = new ColleagueDiscount(command.CourseId, command.DiscountRate);
            _colleague.Create(colleague);
            _colleague.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditColleagueDiscountViewModel command)
        {
            var operation = new OperationResult();

            if (_colleague.IsExist(x => x.CourseId == command.CourseId && x.DiscountRate == command.DiscountRate
            && x.Id!=command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var getColleague = _colleague.GetById(command.Id);
            if (getColleague == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getColleague.Edit(command.CourseId, command.DiscountRate);

            _colleague.Update(getColleague);
            _colleague.SaveChanges();
            return operation.Succeeded();

        }

        public EditColleagueDiscountViewModel GetDetails(long id)
        {
           return _colleague.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> GetAll()
        {
            return _colleague.GetAllList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var getColleague = _colleague.GetById(id);
            if (getColleague == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getColleague.Remove(id);
            _colleague.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var getColleague = _colleague.GetById(id);
            if (getColleague == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getColleague.Restore(id);
            _colleague.SaveChanges();
            return operation.Succeeded();
        }
    }
}
