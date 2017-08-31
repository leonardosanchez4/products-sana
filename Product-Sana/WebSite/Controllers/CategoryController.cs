using ProductsDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;
            var model = new List<Category>();
            model = Biz.Category().GetCategoryList(storage.ToString());
            return View(model);


        }

        // GET: Category/Details/5
        public ActionResult Details(string Name)
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;
            var cat = Biz.Category().GetCategory(storage.ToString(), Name);
            ProductCategoryModel category = new ProductCategoryModel
            {
                CategoryName = cat.CategoryName,
                Id = cat.Id
            };
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var storage = this.Session["_STORAGE"];
                var cat = new Category { CategoryName = collection["CategoryName"] };
                cat.ErrorMessage = Biz.Category().addCategory(storage.ToString(), cat);

                if (cat.ErrorMessage == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var catModel = new ProductCategoryModel { CategoryName = cat.CategoryName };
                    return View(catModel);
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(string Name)
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;
            var cat = Biz.Category().GetCategory(storage.ToString(), Name);
            ProductCategoryModel category = new ProductCategoryModel
            {
                CategoryName = cat.CategoryName,
                Id = cat.Id
            };

            cat.ErrorMessage = "";
            return View(category);

        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, FormCollection collection)
        {
                var storage = this.Session["_STORAGE"];
                ViewBag.Storage = storage;

                ProductCategoryModel category = new ProductCategoryModel
                {
                    CategoryName = collection["CategoryName"].ToString(),
                    Id = Id
                };
                Category cat = new Category
                {
                    CategoryName = category.CategoryName,
                    Id = category.Id
                };

                try
                {
                    category.ErrorMessage = Biz.Category().UpdateCategory(storage.ToString(), cat);
                    return RedirectToAction("Index");
                }
                catch (Exception Ex)
                {
                category.ErrorMessage = "Error Updating Record";
                }
                return View(category);
                // TODO: Add update logic here           
        }

        // GET: Category/Delete/5
        public ActionResult Delete(string Name)
        {

            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;
            var cat = Biz.Category().GetCategory(storage.ToString(), Name);

            ProductCategoryModel category = new ProductCategoryModel
            {
                CategoryName = cat.CategoryName,
                Id = cat.Id
            };

            cat.ErrorMessage = "";
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(string Name, FormCollection collection)
        {
            try
            {
                var storage = this.Session["_STORAGE"];
                ViewBag.Storage = storage;
                Biz.Category().DeleteCategory(storage.ToString(), Name);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
