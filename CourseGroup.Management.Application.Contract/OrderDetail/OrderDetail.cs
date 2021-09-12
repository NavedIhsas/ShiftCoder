using System.Collections.Generic;
using Shop.Management.Application.Contract.Order;

namespace Shop.Management.Application.Contract.OrderDetail
{
   public class OrderDetailViewModel
    {
       
        public string Price { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public double DoublePrice { get; set; }
        public string Picture { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }
}
