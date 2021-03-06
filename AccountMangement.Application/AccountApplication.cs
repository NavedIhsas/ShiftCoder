using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using _0_FrameWork.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Domain.Notification.Agg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly INotificationRepository _notification;
        private readonly IRazorPartialToStringRenderer _renderer;
        private readonly IAccountRepository _repository;
        private readonly ITeacherRepository _teacher;

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

            //check for IsExist
            if ( _repository.IsExist(x => x.Email == FixedText.FixEmail(command.Email)))
                return operation.Failed(ApplicationMessage.DuplicatedEmailAddress);


            // save avatar
            var fileName = command.Avatar?.FileName ?? "avatar.png";
            if (command.Avatar != null)
            {
                //check for image
                if (command.Avatar.IsImage())
                {
                    //resize to 315 X 315
                    #region 315 X 315
                    var image = Image.FromStream(command.Avatar.OpenReadStream());
                    var resized = new Bitmap(image, new Size(315, 315));

                    await using var imageStream = new MemoryStream();
                    resized.Save(imageStream, ImageFormat.Jpeg);
                    var imageBytes = imageStream.ToArray();

                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/UserAvatar/", fileName);
                    await using var streamImg = new FileStream(
                        path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                    streamImg.Write(imageBytes, 0, imageBytes.Length);
                    #endregion
                }

            }


            //register user
            if (command.RoleId == 3)
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone,
                    command.Password, fileName,
                    command.RoleId, NameGenerator.UniqCode(), new List<Teacher>
                    {
                        new("", "", "", command.Id, ThisType.Teacher)
                    });
                 _repository.Create(create);
            }
            else if (command.RoleId == 2)
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone,
                    command.Password, fileName,
                    command.RoleId, NameGenerator.UniqCode(), new List<Teacher>
                    {
                        new("", "", "", command.Id, ThisType.Blogger)
                    });
                _repository.Create(create);
            }
            else
            {
                var create = new Account(command.FullName, FixedText.FixEmail(command.Email), command.Phone,
                    command.Password, fileName,
                    command.RoleId, NameGenerator.UniqCode());
                _repository.Create(create);

                var body = await _renderer.RenderPartialToStringAsync("_SentActivityEmail", create);
                SendEmail.Send(create.Email, "تائید ایمیل", body);
            }

            //create notification
            var notification = new Notification($"کاربری جدید با نام ({command.FullName}) در سایت ثبت نام کرد",
                ThisType.Account, command.Id);
            _notification.Create(notification);
            _notification.SaveChanges();
             _repository.SaveChanges();
            return operation.Succeeded();
        }


        public OperationResult Edit(EditAccountViewModel command)
        {
            var operation = new OperationResult();
            if (_repository.IsExist(x => x.Email == FixedText.FixEmail(command.Email) && x.Id != command.Id))
                return operation.Failed(ApplicationMessage.DuplicatedRecord);

            var getUser = _repository.GetById(command.Id);

            var fileName = command.Avatar?.FileName ?? "avatar.png";
            if (command.Avatar != null)
            {
                //check for image
                if (command.Avatar.IsImage())
                {
                    //delete image
                    var deletePath = $"wwwroot/FileUploader/UserAvatar/{getUser.Avatar}";
                    if (File.Exists(deletePath))
                        File.Delete(deletePath);

                    //resize to 315 X 315
                    #region 315 X 315
                    var image = Image.FromStream(command.Avatar.OpenReadStream());
                    var resized = new Bitmap(image, new Size(315, 315));

                    using var imageStream = new MemoryStream();
                    resized.Save(imageStream, ImageFormat.Jpeg);
                    var imageBytes = imageStream.ToArray();

                    var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/FileUploader/UserAvatar/", fileName);
                    using var streamImg = new FileStream(
                        path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096);
                    streamImg.Write(imageBytes, 0, imageBytes.Length);
                    #endregion
                }

            }



            if (getUser == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            switch (getUser.RoleId)
            {
                case 2:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId, new List<Teacher>
                        {
                            new("", "", "", command.Id, ThisType.Blogger)
                        }, command.ActiveCode);
                    _repository.Update(getUser);
                    break;

                case 3:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId, new List<Teacher>
                        {
                            new("", "", "", command.Id, ThisType.Teacher)
                        }, command.ActiveCode);
                    _repository.Update(getUser);
                    break;

                default:
                    getUser.Edit(command.FullName, FixedText.FixEmail(command.Email), command.Phone, fileName,
                        command.RoleId, command.ProvinceId, command.Gander, command.BirthDate, command.AboutMe);
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
            var user = _repository.GetUserBy(FixedText.FixEmail(command.Email));
            if (user == null || user.IsActive == false) return false;
            command.ActiveCode = user.ActiveCode;

            var emailBody = _renderer.RenderPartialToStringAsync("_ResetPassword", command);
            SendEmail.Send(command.Email, "بازیابی کلمه عبور", await emailBody);

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

        public bool Login(LoginViewModel command)
        {
            return _repository.Login(command);
        }

        public List<AccountViewModel> ShowBlockedUser()
        {
            return _repository.ShowBlockedUser();
        }

        public EditTeacherViewModel GetTeacherDetails(long id)
        {
            return _teacher.GetTeacherDetails(id);
        }

        public BlockUserViewModel GetUserForUnblock(long id)
        {
            return _repository.GetUserForUnblock(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditAccountViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public BlockUserViewModel GetUserForBlock(long id)
        {
            return _repository.GetUserForBlock(id);
        }

        public void ConfirmUnblockUser(long id)
        {
            _repository.ConfirmUnblockUser(id);
        }

        public void Logout()
        {
            _repository.Logout();
        }
    }
}