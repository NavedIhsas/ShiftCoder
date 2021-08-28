using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementInfrastructure.EfCore.Repository
{
   public class InventoryRepository:RepositoryBase<long,Inventory>,IInventoryRepository
   {
       private readonly InventoryContext _context;
        public InventoryRepository(InventoryContext dbContext, InventoryContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditInventoryViewModel GetDetails(long id)
        {
            return _context.Inventories.Select(x => new EditInventoryViewModel
            {
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                Id = x.Id
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var query = _context.Inventories.Select(x => new InventoryViewModel
            {
                Product = null,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                CurrentCount = 0,
                CreationDate = x.CreationDate.ToFarsi(),
                Id=x.Id
            }).AsNoTracking().ToList();

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId).ToList();

            if (!searchModel.InStock)
                query = query.Where(x => x.InStock).ToList();

            var orderly = query.OrderByDescending(x => x.Id).ToList();
            return orderly;
        }
   }
}
