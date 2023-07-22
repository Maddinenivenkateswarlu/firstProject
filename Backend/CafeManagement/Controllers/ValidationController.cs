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
    public class ValidationController : ApiController

    {
        

        public string Post(emailPass obj)
        {

            try
            {
                string Query = @"insert into  emailPass  (Email, password) VALUES (@Email, @Password)";
                //DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CafeAppDB"].ConnectionString))
                    using ( var cmd=new SqlCommand(Query , con))
                {
                    
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@email", obj.Email);
                    cmd.Parameters.AddWithValue("@password", obj.password);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    if (rowsAffected > 0)
                    {
                        return "User registered";
                    }
                    else
                    {
                        return "Already exist or invalid type entered";
                    }

                }
            }
            catch(Exception es)
            {
                return es.Message;  
            }
        }
        public Boolean put(emailPass obj)
        {

            try
            {
                string Query = @"select * from emailPass where (Email=@Email and password=@password)";

                using (var con = new SqlConnection(ConfigurationManager.
                        ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(Query, con))
                {
                    con.Open();
                    bool isCredentialsValid = false;

                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@password", obj.password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            isCredentialsValid = true;
                        }
                    }

                    if (isCredentialsValid)
                    {
 
                        Console.WriteLine("Credentials are valid.");
                        return true;
                    }
                    else
                    {
                        // Email and password are incorrect
                        Console.WriteLine("Invalid credentials.");
                    }
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
    }
    
}
