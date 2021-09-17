using System;
using System.Collections.Generic;
using Shop.Management.Application.Contract.OrderDetail;

namespace Shop.Management.Application.Contract.Order
{
    public class OrderViewModel
    {
        public string Price { get; set; }
        public string UserName { get; set; }
        public long AccountId { get; set; }
        public int DiscountRate { get; set; }
        public int DiscountRateCode { get; set; }
        public bool IsFinally { get; set; }
        public int? CustomerDiscountRate { get; set; }
        public double OrderSum { get; set; }
        public string CreationDate { get; set; }
        public DateTime DateTimeDate { get; set; }
        public long Id { get; set; }
        public string CourseName { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}