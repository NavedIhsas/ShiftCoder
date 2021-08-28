using System.Collections.Generic;
using Shop.Management.Application.Contract.Course;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool InStock { get; set; }
    }
}