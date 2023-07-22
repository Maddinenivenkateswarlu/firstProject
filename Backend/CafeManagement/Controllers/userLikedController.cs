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
    public class userLikedController : ApiController
    {
        public HttpResponseMessage Get(string email)
        {
            try
            {
                string query = @"select * from yourList where loginId=(
                                select Id from LoginDetails where Email=@Email)";
                DataTable dataTable = new DataTable();
                dataTable.TableName = "MyDataTable";

                using (var con = new SqlConnection(ConfigurationManager.
                            ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(dataTable);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, dataTable);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public string Post(Addcart obj)
        {
            try
            {
                int id = 0;
                int rows = 0;
                String SproductName = "";
                string anotherQuery = @"SELECT Id FROM LoginDetails WHERE Email = @Email";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(anotherQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    con.Open();
                    id = (int)cmd.ExecuteScalar();
                }

                
                string already = @"select * from yourList where productName=@productName";
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(already, con))
                {
                   
                    cmd.Parameters.AddWithValue("@productName", obj.productName);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                       
                        SproductName = reader["productName"].ToString();
                        break;
                        
                    }

                    reader.Close();
                    rows =cmd.ExecuteNonQuery();
                }
                if (SproductName=="")
                {
                    string query = @"INSERT INTO yourList (productName, price, loginId, productId)
                         VALUES (@productName, @price, @loginId, @productId)";
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDB"].ConnectionString))
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productName", obj.productName);
                        cmd.Parameters.AddWithValue("@price", obj.price);
                        cmd.Parameters.AddWithValue("@loginId", id);
                        cmd.Parameters.AddWithValue("@productId", obj.productId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                }
               

               

                return "Success";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
            public string delete(int id)
            {
                try
                {
                    string query = @"delete from yourList where productId=@productId";
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDB"].ConnectionString))
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@productId", id);
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                return "success";
                }
            catch(Exception ex)
            {
                return ex.Message;
            }
            }
        
    }

   }

