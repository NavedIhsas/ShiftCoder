using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain;
using AccountManagement.Application.Contract.Account;

namespace AccountManagement.Domain.Account.Agg
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        EditAccountViewModel GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel searchModel);
        Account GetUserBy(string email);
        OperationResult Login(LoginViewModel login);
        void Logout();
    }
}