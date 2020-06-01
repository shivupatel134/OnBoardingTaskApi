using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnBoard.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnBoard.Controllers
{
    public class StoreController : ApiController
    {
        // Get() 
        public HttpResponseMessage Get()
        {
            DataTable dt = new DataTable();
            string query = @"
                            select StoreID, Name, Address 
                            from dbo.Stores
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

        public string Post(Store st)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            insert into dbo.Stores (Name,Address)
                            values ('" + st.Name + @"', '" + st.Address + @"')
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

        public string Put(Store st)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = @"
                            update dbo.Stores set Name='" + st.Name + @"',
                                Address='" + st.Address + @"' where 
                                StoreID= " + st.StoreID + @"
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
                string query = @" delete dbo.Sales  where  StoreID= " + id
               +"delete dbo.Stores  where StoreID= " + id;
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
