using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using TestIndore.Models;
using TestIndore.Services;

namespace TestIndore.Controllers
{
    public class OrderController : Controller
    {
        OrderServices OS = new OrderServices();
        //Session["UserId"] = UserId;
        // GET: Order
        public ActionResult OrderList()
        {
            ViewBag.Alert = TempData["Alert"];
            List<Order> orders = new List<Order>();
            orders = OS.GetOrderList(0);
            return View(orders);
        }
        [HttpGet]
        public ActionResult AddEditOrder(int id = 0)
        {
            if (id == 0)
            {
                return View(new OrderViewModel());
            }
            else
            {
                List<Order> order = OS.GetOrderList(id);
                List<OrderDetail> orderDetail = OS.GetOrderDetails(id);

                OrderViewModel viewModel = new OrderViewModel
                {
                    Order = order.Where(x => x.OrderID == id).FirstOrDefault(),
                    OrderDetails = orderDetail
                };

                return View(viewModel);
                // return View(order.Where(x => x.OrderID == id).FirstOrDefault<Order>());

            }
        }
        [HttpPost]
        public ActionResult AddEditOrder(OrderViewModel orderViewModel)
        {
            List<Order> Orders = new List<Order>();
            List<OrderDetail> OrderDetails = new List<OrderDetail>();

            foreach (var detail in orderViewModel.OrderDetails)
            {

                detail.OrderID = orderViewModel.Order.OrderID;
                detail.Amount = detail.Rate * detail.Qty; // Calculate amount
                OrderDetails.Add(detail);
            }

            orderViewModel.Order.TotalQty = orderViewModel.OrderDetails.Sum(od => od.Qty);
            orderViewModel.Order.TotalAmount = orderViewModel.OrderDetails.Sum(od => od.Amount);

            Orders.Add(orderViewModel.Order);
            XDocument Order = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
             new XElement("Order",
             from ord in Orders
             select new XElement("Order",
             new XElement("OrderID", ord.OrderID),
             new XElement("OrderDate", ord.OrderDate),
             new XElement("CustomerID", ord.CustomerID),
             new XElement("TotalQty", ord.TotalQty),
             new XElement("TotalAmount", ord.TotalAmount))));

            XDocument OrderDetail = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
             new XElement("OrderDetails",
             from ordet in OrderDetails
             select new XElement("OrderDetails",
             new XElement("DetailID", ordet.DetailID),
             new XElement("OrderID", ordet.OrderID),
             new XElement("ProductCode", ordet.ProductCode),
             new XElement("Unit", ordet.Unit),
             new XElement("Qty", ordet.Qty),
             new XElement("Rate", ordet.Rate),
             new XElement("Amount", ordet.Amount))));
            var userID = Convert.ToInt32(Session["UserId"]);
            DataTable dt = OS.SaveOrder(Order, OrderDetail, 0);
            String sp_Status = dt.Rows[0]["Status"].ToString();
            String sp_MSg = dt.Rows[0]["Message"].ToString();
            return View();
        }
    }
}