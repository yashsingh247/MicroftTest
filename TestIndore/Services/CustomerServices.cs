using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestIndore.Models;
using System.Xml.Linq;

namespace TestIndore.Services
{
    public class CustomerServices
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        public List<Customer> GetCustomerList( int id=0)
        {
            cmd = new SqlCommand("Exec GetCustomer "+id+"", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            List<Customer> customer = new List<Customer>();
            foreach (DataRow dr in dt.Rows)
            {
                customer.Add(new Customer
                {
                    CustomerID = Convert.ToInt32(dr["ID"]),
                    CustomerName = dr["Name"].ToString(),
                    CustomerAddress = dr["Address"].ToString(),
                    CustomerCity = dr["City"].ToString(),
                    CustomerPinCode = Convert.ToInt32(dr["PinCode"]),
                    CreatedDate = Convert.ToDateTime(dr["CreatedDate"].ToString()),
                    CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                    UpdatedBy = Convert.ToInt32(dr["Updateby"]),
                    UpdatedDate = Convert.ToDateTime(dr["UpdateDate"].ToString()),
                });

            }
            return customer;
        }
        public DataTable SaveCustomer(XDocument cstm,int  user)
        {
            cmd = new SqlCommand("Exec Insert_Update_Customer '" + cstm + "'," + user + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataTable DaleteCustomer(int Id)
        {
            cmd = new SqlCommand("Exec DeActivateeCustomer " + Id + "", con);
            cmd.CommandType = CommandType.Text;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

    }
}