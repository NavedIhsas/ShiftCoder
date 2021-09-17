using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using DiscountManagement.Domain.UserDiscountAgg;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagementInfrastructure.EfCore.Repository
{
   public class UserDiscountRepository:RepositoryBase<long,UserDiscount>,IUserDiscountRepository
   {
       private readonly DiscountContext _context;
        public UserDiscountRepository(DiscountContext dbContext, DiscountContext context) : base(dbContext)
        {
            _context = context;
        }

        public UserDiscount GetUserDiscountBy(long accountId)
        {
           return _context.UserDiscounts.FirstOrDefault(x => x.AccountId == accountId );
        }
    }
}
