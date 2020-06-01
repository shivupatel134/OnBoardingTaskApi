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
    public class SalesController : ApiController
    {
        // Get() 
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select a.SalesID, a.DateSold, b.Name as ProductName, 
                            b.ProductID,c.CustomerID,a.StoreID,
                            c.Name as CustomerName, d.Name as StoreName
                            from dbo.Sales a, dbo.Products b, dbo.Customers c, 
                            dbo.Stores d where a.ProductID = b.ProductID and 
                            a.CustomerID = c.CustomerID and a.StoreID = d.StoreID";
            //string query = @" 
            //                   select * from dbo.Sales
            //  ";
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

        public string Post(Sales sal)
        {
           
                DataTable dt = new DataTable();
                string query = @"
                            insert into dbo.Sales (DateSold,ProductID,CustomerID,StoreID)
                            values ('" + sal.DateSold + @"'
                            , '" + sal.ProductID + @"', '" + sal.CustomerID + @"'
                            , '" + sal.StoreID + @"')
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

        //Update 

        public string Put(Sales sal)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            update dbo.Sales set 
                                    DateSold='" + sal.DateSold + @"', 
                                    ProductID='" + sal.ProductID + @"',
                                    CustomerID='" + sal.CustomerID + @"',
                                    StoreID='" + sal.StoreID + @"'
                            where SalesID= " + sal.SalesID + @"
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
                string query = @"
                            delete dbo.Sales  where 
                                SalesID= " + id;
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
