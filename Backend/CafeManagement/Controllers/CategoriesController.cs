using CafeManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CafeManagement.Controllers
{
    public class CategoriesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            //Boolean ans=
            string query = @"
                 select * from dbo.category where isDeleted=0;
                    ";
            //DataTable table = new DataTable();
            DataTable dataTable = new DataTable();
            dataTable.TableName = "MyDataTable";
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["CafeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))

            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dataTable);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }
        public string Post(Category cat)
        {
            try
            {
                string query = @"
                    insert into dbo.category values
                     ('" + cat.categoryName + @"', 
                     '" + cat.CatCreationDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                     '" + cat.CatModificationDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                     " + cat.isDeleted + ")";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Added Successfully!!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string Put(Category cat)
        {
            try
            {
                string query = @"
            UPDATE dbo.category SET 
            categoryName = @categoryName,
            CatcreationDate = @catCreationDate,
            CatmodificationDate = @CatModificationDate
            WHERE categoryId = @categoryId";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cafeappdb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {

                    
                    {
                        con.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@categoryName", cat.categoryName);
                        cmd.Parameters.AddWithValue("@catCreationDate", cat.CatCreationDate);
                        cmd.Parameters.AddWithValue("@catModificationDate", cat.CatModificationDate);
                        cmd.Parameters.AddWithValue("@categoryId", cat.categoryId);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return "Updated successfully!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


       
     

        public string delete(int id)
        {
          
            try
            {
                string query = @"UPDATE dbo.category SET
                isDeleted=@isDeleted
                WHERE categoryId = @categoryId";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryId", id);
                    cmd.Parameters.AddWithValue("@isDeleted", 1);


                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        return "Updated successfully!";
                    }
                    else
                    {
                        return "No rows updated.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        

    }
    
}
