using AccountManagement.Application.Contract.Account;
using System.Collections.Generic;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.RoleAgg;
using CommentManagement.Domain.Notification.Agg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _repository;
        private readonly ITeacherRepository _teacher;
        private readonly IFileUploader _fileUploader;
        private readonly INotificationRepository _notification;

        public AccountApplication(IAccountRepository repository, IFileUploader fileUploader, ITeacherRepository teacher, INotificationRepository notification)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _teacher = teacher;
            _notification = notification;
        }
        public OperationResult Create(RegisterUserViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == command.Email)) return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");

            if (command.RoleId == 3)
            {
                var create = new Account(command.FullName, command.Email, command.Phone, command.Password, fileName,
                    command.RoleId, new List<Teacher>()
                    {
                        new Teacher("","","",command.Id)
                    });
                _repository.Create(create);
            }
            else
            {
                var create = new Account(command.FullName, command.Email, command.Phone, command.Password, fileName,
                    command.RoleId);
                _repository.Create(create);
            }

            var notification = new Notification($"کاربری جدید با نام ({command.FullName}) در سایت ثبت نام کرد",OwnerType.Account,command.Id);
            _notification.Create(notification);
            _notification.SaveChanges();

            _repository.SaveChanges();
            return operation.Succeeded();
        }


        public OperationResult Edit(EditAccountViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == command.Email && x.Id != command.Id)) return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");

            var getUser = _repository.GetById(command.Id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

                getUser.Edit(command.FullName, command.Email, command.Phone, fileName,
                    command.RoleId);
           

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

       
        public OperationResult Login(LoginViewModel command)
        {
            return _repository.Login(command);
        }
        public void Logout()
        {
            _repository.Logout();
        }

      
        public OperationResult EditTeacher(EditTeacherViewModel edit)
        {
            var operation = new OperationResult();

            var getUser = _teacher.GetById(edit.Id);
            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            getUser.Edit(edit.Skills, edit.Bio, edit.Resumes, edit.AccountId);
            _teacher.Update(getUser);
            _teacher.SaveChanges();
            return operation.Succeeded();
        }


        public EditTeacherViewModel GetTeacherDetails(long id)
        {
            return _teacher.GetTeacherDetails(id);
        }

        public List<TeacherViewModel> GetAllTeachers()
        {
            return _teacher.GetAllTeachers();
        }

    }
}
