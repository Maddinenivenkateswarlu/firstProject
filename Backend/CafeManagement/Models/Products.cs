using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeManagement.Models
{
    public class Products
    {

        public int productId { set; get; }

        public string productName { set; get; }

        public DateTime pdCreationDate { set; get; }

        public DateTime pdModifyDate{ set; get; }

        public int categoryId { get; set; }

        public int price { get; set; }

        public int isDeleted { get; set; }
    }
}