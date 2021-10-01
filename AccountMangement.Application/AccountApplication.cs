using AccountManagement.Application.Contract.Account;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _repository;
        private readonly ITeacherRepository _teacher;
        private readonly IFileUploader _fileUploader;
        private readonly INotificationRepository _notification;
        private readonly IRazorPartialToStringRenderer _renderer;

        public AccountApplication(IAccountRepository repository, IFileUploader fileUploader, 
            ITeacherRepository teacher, INotificationRepository notification, IRazorPartialToStringRenderer renderer)
        {
            _repository = repository;
            _fileUploader = fileUploader;
            _teacher = teacher;
            _notification = notification;
            _renderer = renderer;
        }
        public async Task<OperationResult> Create(RegisterUserViewModel command)
        {
            var operation = new OperationResult();

            //---check for IsExist--//
            if (_repository.IsExist(x => x.Email == FixedText.FixEmail(command.Email))) return operation.Failed(ApplicationMessage.DuplicatedEmailAddress);

            //---get path for save avatar--//
            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");

            //---register user----//
            if (command.RoleId == 3)
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone, command.Password, fileName,
                    command.RoleId,NameGenerator.UniqCode(), new List<Teacher>()
                    {
                        new Teacher("","","",command.Id,ThisType.Teacher)
                    });
                _repository.Create(create);
            }
            else if (command.RoleId == 2)
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone, command.Password, fileName,
                    command.RoleId, NameGenerator.UniqCode(), new List<Teacher>()
                    {
                        new Teacher("","","",command.Id,ThisType.Blogger)
                    });
                _repository.Create(create);
            }
            else
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone, command.Password, fileName,
                    command.RoleId, NameGenerator.UniqCode());
                _repository.Create(create);

                var body = await _renderer.RenderPartialToStringAsync("_SentActivityEmail", create);
                 SendEmail.Send(create.Email,  "تائید ایمیل", body);

            }
            //---create notification--//
            var notification = new Notification($"کاربری جدید با نام ({command.FullName}) در سایت ثبت نام کرد", ThisType.Account, command.Id);
            _notification.Create(notification);
            _notification.SaveChanges();
            _repository.SaveChanges();
            return operation.Succeeded();
        }


        public OperationResult Edit(EditAccountViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == FixedText.FixEmail(command.Email) && x.Id != command.Id)) return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var fileName = _fileUploader.Uploader(command.Avatar, "UserAvatar");

            var getUser = _repository.GetById(command.Id);


            if (command.Avatar != null)
            {
                var deletePath = $"wwwroot/FileUploader/{getUser.Avatar}";
                if (File.Exists(deletePath))
                    File.Delete(deletePath);
            }

            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            switch (getUser.RoleId)
            {
                case 2:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId, new List<Teacher>()
                        {
                            new Teacher("","","",command.Id,ThisType.Blogger),
                        },command.ActiveCode);
                    _repository.Update(getUser);
                    break;

                case 3:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId, new List<Teacher>()
                        {
                            new Teacher("","","",command.Id,ThisType.Teacher),
                        },command.ActiveCode);
                    _repository.Update(getUser);
                    break;

                default:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId);
                    _repository.Update(getUser);
                    break;
            }
            _repository.SaveChanges();
            _teacher.SaveChanges();
            return operation.Succeeded();
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

        public async Task<bool> ForgotPassword(ForgotPasswordViewModel command)
        {
            var user =_repository.GetUserBy(FixedText.FixEmail(command.Email));
            if (user==null ||user.IsActive==false) return false;
            command.ActiveCode = user.ActiveCode;

            var emailBody = _renderer.RenderPartialToStringAsync("_ResetPassword", command);
            SendEmail.Send(command.Email,"بازیابی کلمه عبور",await emailBody);

            return true;
        }

        public bool ResetPassword(ResetPasswordViewModel command)
        {
            var user = _repository.GetUserByActiveCode(command.ActiveCode);
            if (user == null) return false;

            user.ChangePassword(command.Password);
            user.ChangeActiveCode(user.Id);
            _repository.Update(user);
            _repository.SaveChanges();
            return true;
        }

        public bool Login(LoginViewModel command) => _repository.Login(command);
        public List<AccountViewModel> ShowBlockedUser() => _repository.ShowBlockedUser();
        public EditTeacherViewModel GetTeacherDetails(long id) => _teacher.GetTeacherDetails(id);
        public BlockUserViewModel GetUserForUnblock(long id) => _repository.GetUserForUnblock(id);
        public List<AccountViewModel> Search(AccountSearchModel searchModel) => _repository.Search(searchModel);
        public EditAccountViewModel GetDetails(long id) => _repository.GetDetails(id);
        public BlockUserViewModel GetUserForBlock(long id) => _repository.GetUserForBlock(id);
        public void ConfirmUnblockUser(long id) => _repository.ConfirmUnblockUser(id);
        public void Logout() => _repository.Logout();
    }
}
