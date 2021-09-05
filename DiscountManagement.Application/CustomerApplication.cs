using System;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;
using System.Collections.Generic;
using _0_Framework.Application;
using _0_FrameWork.Application;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerRepository _customer;
        public CustomerApplication(ICustomerRepository customer)
        {
            _customer = customer;
        }
        public OperationResult Create(CreateCustomerDiscountViewModel command)
        {
            var operation = new OperationResult();
            if (_customer.IsExist(x => x.DiscountRate == command.DiscountRate && x.CourseId == command.CourseId))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);


            if (command.EndTime.ToGeorgianDateTime() < command.StartTime.ToGeorgianDateTime()) return operation.Failed("تاریخ پایان نمیتواند کوچکتر از تاریخ شروع باشد");

            var customer = new CustomerDiscount(command.CourseId, command.DiscountRate, command.Description,
               command.StartTime.ToGeorgianDateTime(), command.EndTime.ToGeorgianDateTime(), command.Reason);

            _customer.Create(customer);
            _customer.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Edit(EditCustomerDiscountViewModel command)
        {
            var operation = new OperationResult();
            if (_customer.IsExist(x => x.DiscountRate == command.DiscountRate && x.CourseId == command.CourseId && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var getCustomer = _customer.GetById(command.Id);
            if (getCustomer == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            if (command.EndTime.ToGeorgianDateTime() < command.StartTime.ToGeorgianDateTime()) return operation.Failed("تاریخ پایان نمیتواند کوچکتر از تاریخ شروع باشد");

            getCustomer.Edit(command.CourseId, command.DiscountRate, command.Description,
                command.StartTime.ToGeorgianDateTime(), command.EndTime.ToGeorgianDateTime(), command.Reason);

            _customer.Update(getCustomer);
            _customer.SaveChanges();
            return operation.Succeeded();


        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var getCustomer = _customer.GetById(id);
            if (getCustomer == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getCustomer.Remove(id);
            _customer.SaveChanges();
            return operation.Succeeded();

        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var getCustomer = _customer.GetById(id);
            if (getCustomer == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getCustomer.Restore(id);
            _customer.SaveChanges();
            return operation.Succeeded();
        }

        public EditCustomerDiscountViewModel GetDetails(long id)
        {
            return _customer.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customer.Search(searchModel);
        }


    }
}
