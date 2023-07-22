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
    public class ProductsController : ApiController
    {

        public HttpResponseMessage Get()
        {

            string query ="select * from dbo.products where isDeleted=0";
            DataTable obj = new DataTable();
            obj.TableName = "MyDataTable";
            using (var con = new SqlConnection(ConfigurationManager.
               ConnectionStrings["CafeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(obj);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, obj);

           

        }
        public bool GetById(int id)
        {
            string query = @"SELECT COUNT(*) FROM dbo.products WHERE categoryId = @categoryId";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDb"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@categoryId", id);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public string Post(Products pdt)
        {

            try
            {
                string query = @"
                                INSERT INTO dbo.products (productName, pdCreationDate, pdModifyDate, price, isDeleted, categoryId)
                                   VALUES (
                                            '" + pdt.productName + @"',
                                               '" + pdt.pdCreationDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                            '" + pdt.pdModifyDate.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                             " + pdt.price + @",
                                             " + pdt.isDeleted + @",
                                            " + pdt.categoryId + @"
                                            )";

                DataTable obj = new DataTable();
                obj.TableName = "MyDataTable";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(obj);
                    }
                }

                return "successful";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
       public string Put(Products pdt)
        {
            try
            {
                string query = @"UPDATE dbo.products SET
            productName = @ProductName,
            pdCreationDate = @CreationDate,
            pdModifyDate = @ModifyDate,
            categoryId = @CategoryId,
            price=@price
            WHERE productId = @ProductId";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductName", pdt.productName);
                    cmd.Parameters.AddWithValue("@CreationDate", pdt.pdCreationDate);
                    cmd.Parameters.AddWithValue("@ModifyDate", pdt.pdModifyDate);
                    cmd.Parameters.AddWithValue("@CategoryId", pdt.categoryId);
                    cmd.Parameters.AddWithValue("@price", pdt.price);
                    cmd.Parameters.AddWithValue("@ProductId", pdt.productId);
                  

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

        public string delete(int id)
        {
            try
            {
                string query = @"UPDATE dbo.products SET
                isDeleted=@isDeleted
                WHERE productId = @ProductId";

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["CafeAppDb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductId", id);
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
