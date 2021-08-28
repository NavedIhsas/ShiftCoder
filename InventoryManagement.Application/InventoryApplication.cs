using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using _0_FrameWork.Application;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _repository;

        public InventoryApplication(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateInventoryViewModel command)
        {
            var operation = new OperationResult();
            var inventory = new Inventory(command.UnitPrice, command.ProductId);

            _repository.Create(inventory);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventoryViewModel command)
        {
            var operation = new OperationResult();
            var inventory = _repository.GetById(command.Id);

            if (inventory == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            inventory.Edit(command.UnitPrice, command.ProductId);

            _repository.Update(inventory);
            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventoryViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public OperationResult IncreaseInventory(IncreaseViewModel increase)
        {
            var operation = new OperationResult();

            var inventory = _repository.GetById(increase.InventoryId);
            if (inventory == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            const long  operationId = 1;
                inventory.IncreaseInventory(increase.Count,operationId,increase.Description);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult ReduceInventory(ReduceViewModel reduce)
        {

            var operation = new OperationResult();

            var inventory = _repository.GetById(reduce.InventoryId);
            if (inventory == null) return operation.Failed(ApplicationMessage.RecordNotFount);

            const long operationId = 1;
            inventory.ReduceInventory(reduce.Count, operationId, reduce.OrderId,reduce.Description);

            _repository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult OperationInventory(InventoryOperationViewModel command)
        {
            throw new NotImplementedException();
        }
    }
}
