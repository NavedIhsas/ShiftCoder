using _0_FrameWork.Application;
using Shop.Management.Application.Contract.Course;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract.Inventory
{
   public class CreateInventoryViewModel
    {
        [Range(0,int.MaxValue,ErrorMessage = Validate.Required)]
        public long ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = Validate.Required)]
        public double UnitPrice { get; set; }

        public List<ArticleViewModel> SelectCourses { get; set; }
    }
}
