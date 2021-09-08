using System.Collections.Generic;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
    public interface IAccountApplication
    {
        OperationResult Create(RegisterUserViewModel command);
        OperationResult Edit(EditAccountViewModel command);
        EditAccountViewModel GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        OperationResult Active(long id);
        OperationResult DeActive(long id);
        OperationResult ChangePassword(ChangePasswordViewModel command);
    }
}