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
    public class AvilableCategoriesController : ApiController
    {

        public HttpResponseMessage Get()
        {
            try
            {
                string query = @"select categoryId , categoryName from category";
                DataTable datatable = new DataTable();

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["cafeappdb"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(datatable);
                }
                var dataset = new DataSet();
                dataset.Tables.Add(datatable);
                return Request.CreateResponse(HttpStatusCode.OK, dataset);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
