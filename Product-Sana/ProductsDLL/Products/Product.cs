using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDLL
{
    public sealed class Product
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string CurrentUnitPrice { get; set; }
        public string[] Categories { get; set; }

    }
}
