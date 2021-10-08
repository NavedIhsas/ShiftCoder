#nullable enable
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
        long GetUserIdBy(string email);
        Account? GetUserBy(string email);
        Account GetUserBy(long id);
        Account GetUserBy(long? id);
        Account? GetUserByActiveCode(string activeCode);
        bool EmailConfirm(string activeCode);
        List<AccountViewModel> SelectList();
        List<AccountViewModel> ShowBlockedUser();
        bool Login(LoginViewModel login);
        BlockUserViewModel GetUserForBlock(long id);
        BlockUserViewModel GetUserForUnblock(long id);
        void ConfirmUnblockUser(long id);
        void Logout();

      
    }
}