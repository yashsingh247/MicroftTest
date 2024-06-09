using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestIndore.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public int CustomerPinCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class User
    { 
    public string UserName { get; set; }
        public string Password { get; set; }
    }
}