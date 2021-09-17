using System.Collections.Generic;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(RegisterUserViewModel command);
        OperationResult Edit(EditAccountViewModel command);
        EditAccountViewModel GetDetails(long id);
        BlockUserViewModel GetUserForBlock(long id);
        BlockUserViewModel GetUserForUnblock(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult Active(long id);
        OperationResult DeActive(long id);
        void ConfirmUnblockUser(long id);
        OperationResult ChangePassword(ChangePasswordViewModel command);
        OperationResult Login(LoginViewModel command);
        List<AccountViewModel> ShowBlockedUser();
        void Logout();

        OperationResult EditTeacher(EditTeacherViewModel edit);
        EditTeacherViewModel GetTeacherDetails(long id);
    }
}