using CafeManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace CafeManagement.Controllers
{
    public class forgetController : ApiController

    {
            
        public string Put(NewEmail obj) { 
            try{
                String encrypted = Encrypt(obj.Password);
        string query = @"select Email from LoginDetails where Email=@Email";
                using (var con = new SqlConnection(ConfigurationManager.
                                ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    bool isCredentialsValid = false;
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isCredentialsValid = true;
                            reader.Close();

                            string inquery = @"update dbo.LoginDetails set Password=@Password where Email=@Email";
                            using (var cmd1 = new SqlCommand(inquery, con))
                            {
                                cmd1.Parameters.AddWithValue("@Email", obj.Email);
                                cmd1.Parameters.AddWithValue("@Password", encrypted);
                                cmd1.CommandType = CommandType.Text;
                                int rowsAffected = cmd1.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    return "Password updated successfully.";
                                }
                            }
                        }
                        return "Email not found.";
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static String Encrypt(String password)
        {
            if (password == null)
            {
                 return null;
             }
            else
            {
                byte[] store = ASCIIEncoding.ASCII.GetBytes(password);
                String EncryptedPassword = Convert.ToBase64String(store);
                return EncryptedPassword;
            }
        }
    }
}