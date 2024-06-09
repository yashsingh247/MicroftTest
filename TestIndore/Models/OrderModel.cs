using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIndore.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalQty { get; set; } /*(sum of QTY of Order details)*/
        public decimal TotalAmount { get; set; }/*(sum of Amount of Order details)*/
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail
    {
        public int DetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductCode { get; set; }
        public HttpPostedFile ProductImage { get; set; }
        public decimal Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }

    }
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}