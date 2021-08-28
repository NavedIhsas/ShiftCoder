namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventoryOperationViewModel
    {
        public int Count { get; set; }
        public long CurrentCount { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public string Description { get; set; }
        public bool Operation { get; set; }
        public string OperationDate { get; set; }
        public long Operator { get; set; }
    }
}