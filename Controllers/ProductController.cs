using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using OnBoard.Models;
using System.Configuration;

namespace OnBoard.Controllers
{
    public class ProductController : ApiController
    {
        // Get() 
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select ProductID, Name, Price 
                            from dbo.Products
                            ";
            using (var con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["OnBordTaskDB"]
                .ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        // Post()

        public string Post(Product pro)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            insert into dbo.Products (Name,Price)
                            values ('" + pro.Name + @"', '" + pro.Price + @"')
                            ";
                using (var con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["OnBordTaskDB"]
                    .ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to Add";
            }
        }

        //Update 

        public string Put(Product pro)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            update dbo.Products set Name='" + pro.Name + @"',
                                Price='" + pro.Price + @"' where 
                                ProductID= " + pro.ProductID + @"
                            ";
                using (var con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["OnBordTaskDB"]
                    .ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update";
            }
        }

        // Delete 

        public string Delete(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @" delete dbo.Sales  where  ProductID= " + id
               + " delete dbo.Products  where ProductID= " + id;
                using (var con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["OnBordTaskDB"]
                    .ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(dt);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete";
            }
        }
    }
}
