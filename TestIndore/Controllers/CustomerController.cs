using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIndore.Models;
using TestIndore.Services;
using System.Xml.Linq;
using System.Data;
using static TestIndore.Services.LoginServices;

namespace TestIndore.Controllers
{
    public class CustomerController : Controller
    {
        CustomerServices CS = new CustomerServices();
        LoginServices GS = new LoginServices();
        // GET: Customer
        public ActionResult CustomerList()
        {
            List<Customer> reseller = new List<Customer>();
            reseller = CS.GetCustomerList(0);
            return View(reseller);
           // return View();
        }
        [HttpGet]
        public ActionResult AddEditCustomer(int id = 0)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else
            {
                List<Customer> customers = new List<Customer>();
                customers = CS.GetCustomerList(id);
                return View(customers.Where(x => x.CustomerID == id).FirstOrDefault<Customer>());

            }
        }

        [HttpPost]
        public ActionResult AddEditCustomer(Customer customer)
        {
            List<Customer> customers = new List<Customer>();
            var userID = Convert.ToInt32(Session["UserId"]);
            customers.Add(customer);
            XDocument Customer = new XDocument(new XDeclaration("1.0", "UTF - 8", "yes"),
            new XElement("Customers",
            from cstm in customers
            select new XElement("Customers",
            new XElement("Customerid", cstm.CustomerID),
            new XElement("Name", cstm.CustomerName),
            new XElement("Address", cstm.CustomerAddress),
            new XElement("City", cstm.CustomerCity),
            new XElement("Pincode", cstm.CustomerPinCode))));
            DataTable dt = CS.SaveCustomer(Customer, userID);
            String sp_Status = dt.Rows[0]["Status"].ToString();
            String sp_MSg = dt.Rows[0]["Message"].ToString();
            if (sp_Status == "1")
            {
                ViewBag.Alert = GS.ShowAlert(Alerts.Success, sp_MSg);

            }
            else
            {
                ViewBag.Alert = GS.ShowAlert(Alerts.Danger, sp_MSg);

            }
            return View(new Customer());

        }
        public ActionResult DeleteCustomer(int Id)
        {
            
            DataTable dt = CS.DaleteCustomer(Id);
            String sp_Status = dt.Rows[0]["Status"].ToString();
            String sp_MSg = dt.Rows[0]["Message"].ToString();
            return RedirectToAction("CustomerList");

        }

    }
}