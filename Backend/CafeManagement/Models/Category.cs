using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeManagement.Models
{
    public class Category
    {
        public int categoryId { set; get; }

        public string categoryName { set; get; }

        public DateTime CatCreationDate { set; get; }

        public DateTime CatModificationDate { set; get; }
        public int isDeleted { set; get; }
    }
}