using AccountManagement.Application.Contract.Account;
using System;
using System.Collections.Generic;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;

namespace AccountManagement.Application
{
    public class AccountApplication:IAccountApplication
    {
        private readonly IAccountRepository _repository;
        private readonly IFileUploader _fileUploader;

        public AccountApplication(IAccountRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(RegisterUserViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == command.Email)) return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");
            var create = new Account(command.FullName, command.Email, command.Phone, command.Password, fileName,
                command.RoleId);

            _repository.Create(create);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditAccountViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == command.Email&&x.Id!=command.Id)) return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");

            var getUser = _repository.GetById(command.Id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);
            getUser.Edit(command.FullName, command.Email, command.Phone, fileName,
                command.RoleId);

            _repository.Update(getUser);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAccountViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public OperationResult Active(long id)
        {

            var operation = new OperationResult();

            var getUser = _repository.GetById(id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);
           
            getUser.Active(id);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult DeActive(long id)
        {
            var operation = new OperationResult();

            var getUser = _repository.GetById(id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getUser.DeActive(id);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ChangePassword(ChangePasswordViewModel command)
        {
            var operation = new OperationResult();

            var getUser = _repository.GetById(command.Id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            if (getUser.Password != command.OldPassword) return operation.Failed(ApplicationMessage.PasswordNotFount);
         
            getUser.ChangePassword(command.NewPassword);
            _repository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
