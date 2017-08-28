using ProductsDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDLL
{
    public sealed class ProductRepository: ConnectionRepository
    {
        public ProductRepository(string appSettingKey) : base(appSettingKey) {
        }

        public string AddProductCache(Product prd)
        {
            
            var list = CacheContent<Product>("ProductList");
            if (list == null)
            {
                list = new List<Product>();               
            }

            int index = list.FindIndex(f => f.SKU == prd.SKU);

            if (index < 0)
            {
                list.Add(prd);
                CacheAdd("ProductList", list);
                return "OK";
            }
            else
            {
                return "SKU Code Already Exists";
            }


        }

        public List<Product> GetProductList()
        {
            return CacheContent<Product>("ProductList");

        }


    }

}
