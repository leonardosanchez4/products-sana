using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDLL.Categories
{
    public sealed class CategoryRepository : ConnectionRepository
    {
        public CategoryRepository(string appSettingKey) : base(appSettingKey)
        {
        }

        private string AddCategoryCache(Category Cat)
        {
            var list = CacheContent<Category>("CategoryList");
            int currId = 1;
            if (list == null)
            {
                list = new List<Category>();
            }
            else
            {
                currId = list.Max(obj => obj.Id);
                Cat.Id = currId + 1;
            }

            int index = list.FindIndex(f => f.CategoryName == Cat.CategoryName);

            if (index < 0)
            {
                list.Add(Cat);
                CacheClear("CategoryList");
                CacheAdd("CategoryList", list);
                return "OK";
            }
            else
            {
                return "Category Name Already Exists";
            }
        }


        private string AddCategoryXML(Category Cat)
        {
            List<Category> list;
            try
            {
                list = FromXmlFile<List<Category>>("Category");
            }
            catch(Exception ex)
            {
                 list = null;
            }
            
            int currId = 1;
            if (list == null)
            {
                list = new List<Category>();
            }
            else
            {
                currId = list.Max(obj => obj.Id);
                Cat.Id = currId + 1;
            }

            int index = list.FindIndex(f => f.CategoryName == Cat.CategoryName);

            if (index < 0)
            {
                list.Add(Cat);
                ToXmlFile(list, "Category");
                return "OK";
            }
            else
            {
                return "Category Name Already Exists";
            }
        }

        public string addCategory(string Storage,Category cat)
        {
            if (Storage == "Cache")
            {
                return AddCategoryCache(cat);
            }
            else
            {
                return AddCategoryXML(cat);
            }
        }

        public List<Category> GetCategoryList(string storage)
        {
            List<Category> list;
            if (storage == "Cache")
            {
                list = CacheContent<Category>("CategoryList");
            }
            else
            {
                list = FromXmlFile<List<Category>>("Category");
            }


            

            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Category>();
            }

        }




        public Category GetCategory(string storage, string Name)
        {
            List<Category> list;
            if (storage == "Cache")
            {
                list = CacheContent<Category>("CategoryList");
            }
            else
            {
                list = FromXmlFile<List<Category>>("Category");
            }
            var Category = list.Find(p => p.CategoryName == Name);
            return Category;
        }




        public void DeleteCategory(string storage, string Name)
        {
            List<Category> list;
            if (storage == "Cache")
            {
                list = CacheContent<Category>("CategoryList");
            }
            else
            {
                list = FromXmlFile<List<Category>>("Category");
            }


            int index = list.FindIndex(f => f.CategoryName == Name);

            list.Remove(list[index]);


            if (storage == "Cache")
            {
                CacheAdd("CategoryList", list);
            }
            else
            {
                ToXmlFile(list, "Category");
            }
         
        }




        public string UpdateCategory(string storage, Category cat)
        {
            List<Category> list;
            if (storage == "Cache")
            {
                list = CacheContent<Category>("CategoryList");
            }
            else
            {
                list = FromXmlFile<List<Category>>("Category");
            }


            int index = list.FindIndex(f => f.Id == cat.Id);
            list[index].CategoryName = cat.CategoryName;
            


            if (storage == "Cache")
            {
                CacheAdd("CategoryList", list);
            }
            else
            {
                ToXmlFile(list, "Category");
            }
            return "OK";

        }


    }
}
