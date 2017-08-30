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

        private string AddProductCache(Product prd)
        {
            
            List<Product> list = CacheContent<Product>("ProductList");
            if (list == null)
            {
                list = new List<Product>();               
            }

            int index = list.FindIndex(f => f.SKU == prd.SKU);

            if (index < 0)
            {
                list.Add(prd);
                CacheClear("ProductList");
                CacheAdd("ProductList", list);
                
                return "OK";
            }
            else
            {
                return "SKU Code Already Exists";
            }
        }

        private string AddProductXML(Product prd)
        {

            var list = FromXmlFile<List<Product>>("Product");
            if (list == null)
            {
                list = new List<Product>();
            }

            int index = list.FindIndex(f => f.SKU == prd.SKU);

            if (index < 0)
            {
                list.Add(prd);
                ToXmlFile(list,"Product");
                return "OK";
            }
            else
            {
                return "SKU Code Already Exists";
            }


        }



        public string addProduct(string Storage,Product prd)
        {
            if (Storage == "Cache")
            {
                return AddProductCache(prd);
            }
            else
            {
                return AddProductXML(prd);
            }

        }




        public List<Product> GetProductList(string Storage)
        {
            List<Product> list;

            if (Storage == "Cache")
            {
                list =  CacheContent<Product>("ProductList");
            }
            else
            {
               list = FromXmlFile<List<Product>>("Product");
               
            }

            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Product>();
            }


        }

        public string UpdateProduct(string Storage,Product prd)
        {
            try
            {
                List<Product> list;
                if (Storage == "Cache")
                {
                    list = CacheContent<Product>("ProductList");
                }
                else
                {
                    list = FromXmlFile<List<Product>>("Product");
                }
                
                int index = list.FindIndex(f => f.SKU == prd.SKU);
                list[index].ProductName = prd.ProductName;
                list[index].ProductDescription = prd.ProductDescription;
                list[index].CurrentUnitPrice = prd.CurrentUnitPrice;
                list[index].Categories = prd.Categories;

                if (Storage == "Cache")
                {
                    CacheClear("ProductList");
                    CacheAdd("ProductList", list);


                }
                else
                {
                    ToXmlFile(list, "Product");
                }


                return "OK";
            }
            catch (Exception ex)
            {
                return "Error Updating Record";
            }
            
        }

        public Product GetProduct(string storage,string SKU)
        {
            List<Product> list;
            if (storage == "Cache")
            {
                list = CacheContent<Product>("ProductList");
            }
            else
            {
                list = FromXmlFile<List<Product>>("Product");
            }
            var product = list.Find(p => p.SKU == SKU);
            return product;
        }


    }

}
