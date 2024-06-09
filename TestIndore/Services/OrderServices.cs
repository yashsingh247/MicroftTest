using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using TestIndore.Models;

namespace TestIndore.Services
{
    public class OrderServices
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        public List<Order> GetOrderList(int id = 0)
        {
            cmd = new SqlCommand("Exec GetOrder " + id + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<Order> orders = new List<Order>();
            foreach (DataRow dr in dt.Rows)
            {
                orders.Add(new Order
                {
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    CustomerID = Convert.ToInt32(dr["CustomerID"]),
                    CustomerName = dr["Name"].ToString(),
                    OrderDate = Convert.ToDateTime(dr["OrderDate"].ToString()),
                    TotalQty = Convert.ToDecimal(dr["Unit"]),
                    TotalAmount = Convert.ToDecimal(dr["Rate"]),
                    CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString()),
                    CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                    UpdatedBy = Convert.ToInt32(dr["Updateby"]),
                    UpdatedDate = Convert.ToDateTime(dr["UpdateDate"].ToString()),
                });

            }
            Order currentOrder = null;
            foreach (DataRow dr in dt.Rows)
            {
                // Check if it's a new order
                int orderId = Convert.ToInt32(dr["OrderID"]);
                if (currentOrder == null || currentOrder.OrderID != orderId)
                {
                    // Create a new Order object
                    currentOrder = new Order
                    {
                        OrderID = orderId,
                        CustomerID = Convert.ToInt32(dr["CustomerID"]),
                        CustomerName = dr["Name"].ToString(),
                        OrderDate = Convert.ToDateTime(dr["OrderDate"]),
                        TotalQty = Convert.ToDecimal(dr["TotalQty"]),
                        TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        UpdatedBy = Convert.ToInt32(dr["Updateby"]),
                        UpdatedDate = Convert.ToDateTime(dr["UpdateDate"])
                    };
                    orders.Add(currentOrder);
                }

                // Create an OrderDetail object
                OrderDetail orderDetail = new OrderDetail
                {
                    DetailID = Convert.ToInt32(dr["OrderDetailID"]),
                    OrderID = orderId,
                    //ProductCode = dr["ProductCode"].ToString(),
                    //ProductImage = dr["ProductImage"].ToString(),
                    Unit = Convert.ToDecimal(dr["Unit"]),
                    Rate = Convert.ToDecimal(dr["Rate"]),
                    Qty = Convert.ToInt32(dr["Qty"]),
                    Amount = Convert.ToDecimal(dr["Amount"])
                };

                // Add the OrderDetail object to the current Order object
                currentOrder.OrderDetails.Add(orderDetail);
            }
            return orders;
        }
        public List<OrderDetail> GetOrderDetails(int id = 0)
        {
            cmd = new SqlCommand("Exec GetOrderDetails " + id + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<OrderDetail> orderDetail = new List<OrderDetail>();
            foreach (DataRow dr in dt.Rows)
            {

                // Create an OrderDetail object
                 orderDetail.Add ( new OrderDetail
                {
                    DetailID = Convert.ToInt32(dr["OrderDetailID"]),
                    OrderID = Convert.ToInt32(dr["OrderID"]),
                    //ProductCode = dr["ProductCode"].ToString(),
                    //ProductImage = dr["ProductImage"].ToString(),
                    Unit = Convert.ToDecimal(dr["Unit"]),
                    Rate = Convert.ToDecimal(dr["Rate"]),
                    Qty = Convert.ToInt32(dr["Qty"]),
                    Amount = Convert.ToDecimal(dr["Amount"])
                });
            }
            return orderDetail;
        }
        public List<Customer> GetCustomerDDL()
        {
            Image image = null;
            string query = "Select ID,CONCAT(ID, ': ', Name) as Name from Customer";
            cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<Customer> customers = new List<Customer>();
            foreach (DataRow dr in dt.Rows)
            {
                customers.Add(new Customer
                {
                    CustomerID = Convert.ToInt32(dr["ID"]),
                    CustomerName = dr["Name"].ToString(),

                });

            }
            return customers;
        }
        public List<Product> GetProductDDL()
        {
            Image image = null;
            string query = "Select ProductID,CONCAT(ProductID, ': ', ProductName) as ProductName from Product";
            cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            List<Product> products = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new Product
                {
                    ProductID = Convert.ToInt32(dr["ProductID"]),
                    ProductName = dr["ProductName"].ToString(),

                });

            }
            return products;
        }
        public DataTable SaveOrder(XDocument order, XDocument orderDetails, int user)
        {
            cmd = new SqlCommand("Exec Insert_Update_Customer '" + order + "','"+ orderDetails + "'," + user + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}