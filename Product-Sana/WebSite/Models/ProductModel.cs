using ProductsDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public List<Category> CategoriesList { get; set; }
        public string ErrorMessage { get; set; }

        public string MultiselectScript
        {
            get
            {

                if (Categories != null)
                {
                    var result = string.Join(",", Categories);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("var selectedCategories=\"" + result + "\";\n");
                    sb.Append("var dataarray = selectedCategories.split(\",\");\n");
                    sb.Append("$('.selectpicker').selectpicker('val', dataarray); \n");

                    return sb.ToString();
                }
                else
                {
                    return "";
                }                
            }
            set { }
        }

    }
}