using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIndore.Models;
using TestIndore.Services;
using static TestIndore.Services.LoginServices;

namespace TestIndore.Controllers
{
    public class HomeController : Controller
    {
        LoginServices LS = new LoginServices();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User account)
        {
            var UserName = account.UserName;
            var Password = account.Password;
            DataTable data = LS.Account(UserName, Password);
            if (data.Rows.Count > 0)
            {
                this.Session["Account"] = data;
                
                String UserId = data.Rows[0]["UserId"].ToString();
                string User= data.Rows[0]["UserName"].ToString();
                Session["UserName"] = User;
                Session["UserId"] = UserId;
                ViewBag.Alert = LS.ShowAlert(Alerts.Success, "Login Success");
                return RedirectToAction("CustomerList", "Customer");
            }
            else
            {
                ViewBag.Alert = LS.ShowAlert(Alerts.Danger, "Your UserName Or Password is Incorrect");
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            this.Session["Account"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }



    }
}