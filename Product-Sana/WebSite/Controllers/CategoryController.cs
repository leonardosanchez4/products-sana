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
        public ActionResult Details(int id)
        {
            return View();
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
                cat.ErrorMessage = Biz.Category().addCategory(storage.ToString(),cat);

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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
