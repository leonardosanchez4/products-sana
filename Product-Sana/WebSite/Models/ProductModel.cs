using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSite.Models
{

    public class ProductIndexModel
    {

    }



    public class ProductModel
    {
        public int Id { get; set; }
        [DisplayName("SKU")]
        public string SKU { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Description")]
        public string ProductDescription { get; set; }
        [DisplayName("Current Unit Price")]
        public string CurrentUnitPrice { get; set; }
        [DisplayName("Categories")]
        public string[] Categories { get; set; }

        public string ErrorMessage { get; set; }

    }
}