using System.Collections.Generic;
using _0_Framework.Application;
using _0_FrameWork.Application;
using ColleagueDiscountManagementApplication.Contract.DiscountCode;
using DiscountManagement.Domain.DiscountCode;

namespace DiscountManagement.Application
{
    public class DiscountCodeApplication : IDiscountCodeApplication
    {
        private readonly IDiscountCodeRepository _discount;

        public DiscountCodeApplication(IDiscountCodeRepository discount)
        {
            _discount = discount;
        }

        public OperationResult Create(CreateDiscountCodeViewModel command)
        {
            var operation = new OperationResult();
            if (_discount.IsExist(x => x.Code == command.DiscountCode))
                return operation.Failed(ApplicationMessage.ExistUserCourse);

            var discountCode = new DiscountCode(command.StartDate.ToGeorgianDateTime(),
                command.EndDate.ToGeorgianDateTime(), command.Reason,
                command.UseableCount, command.DiscountCode, command.DiscountRate);

            _discount.Create(discountCode);
            _discount.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditDiscountCodeViewModel command)
        {
            var operation = new OperationResult();
            if (_discount.IsExist(x => x.Code == command.DiscountCode && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.ExistUserCourse);

            var discountCode = _discount.GetById(command.Id);
            if (discountCode == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            discountCode.Edit(command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(),
                command.Reason,
                command.UseableCount, command.DiscountCode, command.DiscountRate);

            _discount.Update(discountCode);
            _discount.SaveChanges();
            return operation.Succeeded();
        }

        public EditDiscountCodeViewModel GetDetails(long id)
        {
            return _discount.GetDetails(id);
        }

        public List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel)
        {
            return _discount.SearchModel(searchModel);
        }
    }
}