using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestIndore.Models;

namespace TestIndore.Services
{
    public class ProductServices
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        public List<Product> GetProductList(int id = 0)
        {
            cmd = new SqlCommand("Exec GetProduct " + id + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<Product> product = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                product.Add(new Product
                {
                    ProductID = Convert.ToInt32(dr["ProductID"]),
                    ProductCode = dr["ProductCode"].ToString(),
                    ProductName = dr["ProductName"].ToString(),
                    ProductImage = dr["ProductImage"].ToString(),
                    Unit = Convert.ToDecimal(dr["Unit"]),
                    Rate = Convert.ToDecimal(dr["Rate"]),
                    Description= dr["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString()),
                    CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                    UpdatedBy = Convert.ToInt32(dr["Updateby"]),
                    UpdatedDate = Convert.ToDateTime(dr["UpdateDate"].ToString()),

                });

            }
            return product;
        }
        public Image GetImage(int id)
        {
            Image image = null;
            string query = "SELECT  FileName, Data, ContentType FROM Product WHERE ProductID = " + id + "";
            cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            //List<Image> product = new List<Image>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //  image=  new Image
            //    {

            //        FileName = dr["FileName"].ToString(),
            //        ContentType = dr["ContentType"].ToString(),
            //        Data = dr["Data"] as byte[] ?? new byte[0]
            //    };

            //}
            return image;


        }
        public DataTable SaveProduct(int id, string Name,string ProductCode, decimal unit, decimal Rate, string description, string image,int UserName)
        {
            cmd = new SqlCommand("Exec SaveProduct " + id + ",'"+ ProductCode + "','" + Name + "',"+unit+","+Rate+ ",'" + description + "'," + 0 + ",'" + image + "'", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataTable DaleteProduct(int Id)
        {
            cmd = new SqlCommand("Exec DeActivateProduct " + Id + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}