using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIndore.Models;
using TestIndore.Services;

namespace TestIndore.Controllers
{
    public class ProductController : Controller
    {
        ProductServices PS = new ProductServices();

        // GET: Product
        public ActionResult ProductList()
        {
            var ResellerID = "";
            ViewBag.Alert = TempData["Alert"];
            List<Product> product = new List<Product>();
            product = PS.GetProductList(0);

            return View(product);
            // return View();
        }
        public ActionResult ViewImage(int id)
        {
            Image image = PS.GetImage(id);
            if (image.Data != null || image.Data.Length != 0)
            {
                return File(image.Data, image.ContentType);
            }
            return HttpNotFound();
        }
        [HttpGet]
        public ActionResult AddEditProduct(int id = 0)
        {
            if (id == 0)
            {
                return View(new Product());
            }
            else
            {
                List<Product> products = new List<Product>();
                products = PS.GetProductList(id);
                return View(products.Where(x => x.ProductID == id).FirstOrDefault<Product>());

            }
        }
        [HttpPost]
        public ActionResult AddEditProduct(Product product)
        {
            string fileName = null;
            string filePath = null;
            string imageUrl = null;
            if (product.File != null && product.File.ContentLength > 0)
            {
                byte[] data;
                
                using (var binaryReader = new System.IO.BinaryReader(product.File.InputStream))
                {
                    data = binaryReader.ReadBytes(product.File.ContentLength);
                }

                 fileName = Path.GetFileName(product.File.FileName);
                filePath = Path.Combine(Server.MapPath("~/ProductImage"), fileName);
                System.IO.File.WriteAllBytes(filePath, data);
                imageUrl = Url.Content("~/ProductImage/" + fileName);
            }
            var userID = Convert.ToInt32(Session["UserId"]);
            DataTable dt = PS.SaveProduct(product.ProductID, product.ProductName, product.ProductCode, product.Unit, product.Rate, product.Description, imageUrl, userID);
            String sp_Status = dt.Rows[0]["Status"].ToString();
            String sp_MSg = dt.Rows[0]["Message"].ToString();
            return View();
        }

        public ActionResult DeleteProduct(int Id)
        {

            DataTable dt = PS.DaleteProduct(Id);
            String sp_Status = dt.Rows[0]["Status"].ToString();
            String sp_MSg = dt.Rows[0]["Message"].ToString();
            return RedirectToAction("ProductList");

        }
    }

}