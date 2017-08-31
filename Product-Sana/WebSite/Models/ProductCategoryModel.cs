using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public sealed class ProductCategoryModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string CategoryName { get; set; }
        public object ErrorMessage { get; internal set; }
    }
}