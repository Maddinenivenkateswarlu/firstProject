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
    public class NewValidationsController : ApiController
    {
        public Boolean Post(NewEmail obj)
        {
            try
            {

                string empty ="";
                char ch;
                String EncryptedPassword = Encrypt(obj.Password);
                string Query = @"insert into  LoginDetails  (Username , Email, Password, Number , Type) VALUES ( @Username ,@Email, @Password , @Number ,@Type )";
                //DataTable table = new DataTable();
                if (true)
                {
                    if (obj.Username =="")
                    {
                        return false;
                    }
                    if (obj.Email == empty)
                    {
                        return false;
                    }
                    if (obj.Password == empty)
                    {
                        return false;
                    }
                }
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CafeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(Query, con))
                {

                    cmd.CommandType = CommandType.Text;
             
                    cmd.Parameters.AddWithValue("@Username", obj.Username);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Password", EncryptedPassword);
                    cmd.Parameters.AddWithValue("@Number", obj.Number);
                    cmd.Parameters.AddWithValue("@Type", obj.Type);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();
                    if (rowsAffected > 0)
                    {
                        return true ;
                    }
                    else
                    {
                        return false ;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
            public String put(NewEmail obj)
            {

                try
                {

                    String comparison = "Admin";

                    String ans = "";
                String Decrypted = "";
                    string Query = @"select Type ,Password from LoginDetails where (Email=@Email)";

                    using (var con = new SqlConnection(ConfigurationManager.
                            ConnectionStrings["CafeAppDB"].ConnectionString))
                    using (var cmd = new SqlCommand(Query, con))
                    {
                        con.Open();
                        bool isCredentialsValid = false;

                        cmd.Parameters.AddWithValue("@Email", obj.Email);
                        //cmd.Parameters.AddWithValue("@password", obj.Password);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                isCredentialsValid = true;
                                ans = reader["Type"].ToString();
                                Decrypted = Decrypt(reader["Password"].ToString());
                                
                                
                            }
                        }

                        if (isCredentialsValid && Decrypted==obj.Password)
                    { 
                        // Email and password are correct
                        //Console.WriteLine("Credentials are valid.");
                        return ans;
                        }
                    return "Wrong;";
                    }
                }
                catch (Exception )
                {
                    return "some thing wrong";
                }
            }

        public  static  String Encrypt(String password)
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

        public static String Decrypt(String password)
        {
            if (password == null)
            {
                return null;
            }
            else
            {
                byte[] encryptedpassword = Convert.FromBase64String(password);
                String decrypted = ASCIIEncoding.ASCII.GetString(encryptedpassword);
                return decrypted;
            }
        }



    }
}
