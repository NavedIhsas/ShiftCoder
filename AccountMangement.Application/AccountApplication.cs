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
                        new Teacher("","","",command.Id,ThisType.Teacher)
                    });
                _repository.Create(create);
            }
            else if (command.RoleId == 2)
            {
                var create = new Account(command.FullName, command.Email, command.Phone, command.Password, fileName,
                    command.RoleId, new List<Teacher>()
                    {
                        new Teacher("","","",command.Id,ThisType.Blogger)
                    });
                _repository.Create(create);
            }
            else
            {
                var create = new Account(command.FullName, command.Email, command.Phone, command.Password, fileName,
                    command.RoleId);
                _repository.Create(create);
            }


            var notification = new Notification($"کاربری جدید با نام ({command.FullName}) در سایت ثبت نام کرد", ThisType.Account, command.Id);
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

            switch (getUser.RoleId)
            {
                case 2:
                    getUser.Edit(command.FullName, command.Email, command.Phone, fileName,
                        command.RoleId, new List<Teacher>()
                        {
                            new Teacher("","","",command.Id,ThisType.Blogger),
                        });
                    _repository.Update(getUser);
                    break;

                case 3:
                    getUser.Edit(command.FullName, command.Email, command.Phone, fileName,
                        command.RoleId, new List<Teacher>()
                        {
                            new Teacher("","","",command.Id,ThisType.Teacher),
                        });
                    _repository.Update(getUser);
                    break;

                default:
                    getUser.Edit(command.FullName, command.Email, command.Phone, fileName,
                        command.RoleId);
                    _repository.Update(getUser);
                    break;
            }
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditAccountViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public BlockUserViewModel GetUserForBlock(long id)
        {
            return _repository.GetUserForBlock(id);
        }

        public BlockUserViewModel GetUserForUnblock(long id)
        {
            return _repository.GetUserForUnblock(id);
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

        public void ConfirmUnblockUser(long id)
        {
            _repository.ConfirmUnblockUser(id);
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

        public List<AccountViewModel> ShowBlockedUser()
        {
            return _repository.ShowBlockedUser();
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

            getUser.Edit(edit.Skills, edit.Bio, edit.Resumes, edit.AccountId, edit.Type);
            _teacher.Update(getUser);
            _teacher.SaveChanges();
            return operation.Succeeded();
        }


        public EditTeacherViewModel GetTeacherDetails(long id)
        {
            return _teacher.GetTeacherDetails(id);
        }

       
    }
}
