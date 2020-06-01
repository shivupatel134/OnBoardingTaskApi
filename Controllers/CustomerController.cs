using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using OnBoard.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace OnBoard.Controllers
{
    public class CustomerController : ApiController
    {
        // Get() 
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select CustomerID, Name, Address 
                            from dbo.Customers
                            ";
            using (var con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["OnBordTaskDB"]
                .ConnectionString))
            using (var cmd = new SqlCommand(query,con)) 
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dt);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dt);
        }

        // Post()

        public string Post(Customer cust)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            insert into dbo.Customers (Name,Address)
                            values ('"+ cust.Name + @"', '"+ cust.Address + @"')
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
            catch(Exception )
            {
                return "Failed to Add";
            }
        }

        //Update 

        public string Put(Customer cust)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            update dbo.Customers set Name='"+cust.Name+ @"',
                                Address='" +cust.Address +@"' where 
                                CustomerID= "+cust.CustomerID +@"
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
                string query = @" delete dbo.Sales  where  CustomerID= " + id  
               + " delete dbo.Customers  where CustomerID= " +id ;
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
