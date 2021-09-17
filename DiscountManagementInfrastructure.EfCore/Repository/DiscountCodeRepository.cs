using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using ColleagueDiscountManagementApplication.Contract.DiscountCode;
using DiscountManagement.Domain.DiscountCode;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagementInfrastructure.EfCore.Repository
{
    public class DiscountCodeRepository : RepositoryBase<long, DiscountCode>, IDiscountCodeRepository
    {
        private readonly DiscountContext _context;
        public DiscountCodeRepository(DiscountContext dbContext, DiscountContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditDiscountCodeViewModel GetDetails(long id)
        {
            return _context.DiscountCodes.Select(x => new EditDiscountCodeViewModel
            {
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                Reason = x.Reason,
                UseableCount = x.UseableCount,
                DiscountCode = x.Code,
                DiscountRate = x.DiscountRate,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<DiscountCodeViewModel> SearchModel(DiscountCodeSearchModel searchModel)
        {
            var query = _context.DiscountCodes.Select(x => new DiscountCodeViewModel()
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Reason = x.Reason,
                DiscountCode = x.Code,
                DiscountRate = x.DiscountRate,
                Id = x.Id,
                UseableCount = x.UseableCount,
            }).ToList();
            if (!string.IsNullOrWhiteSpace(searchModel.DiscountCode))
                query = query.Where(x => x.DiscountCode.ToLower().Trim().Contains(searchModel.DiscountCode.ToLower().Trim())).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }

        public DiscountCode GetDiscountBy(long id)
        {
            return _context.DiscountCodes
                .FirstOrDefault(x=>x.Id==id);
        }
    }
}
