using ProductsDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;           
            List<Product> model = Biz.Product().GetProductList(storage.ToString());
            return View(model);
        }

        public ActionResult Create()
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;
            var Model = new ProductModel();                     
            Model.CategoriesList = Biz.Category().GetCategoryList(storage.ToString());
            Model.ErrorMessage = "";
            return View(Model);
        }
        [HttpPost]
        public ActionResult Create(FormCollection model)
        {
            var storage = this.Session["_STORAGE"];
            ViewBag.Storage = storage;

            ProductModel prd = new ProductModel
            {
                SKU = model["SKU"].ToString(),
                ProductName = model["ProductName"].ToString(),
                ProductDescription = model["ProductDescription"].ToString(),
                CurrentUnitPrice = model["CurrentUnitPrice"].ToString()
            };
            var categories = model["Categories[]"];
            prd.Categories = categories.Split(',');
            prd.CategoriesList = Biz.Category().GetCategoryList(storage.ToString());

            Product product = new Product
            {
                SKU = prd.SKU,
                ProductName = prd.ProductName,
                ProductDescription = prd.ProductDescription,
                CurrentUnitPrice = prd.CurrentUnitPrice,
                Categories = prd.Categories
            };

            prd.ErrorMessage = Biz.Product().addProduct(storage.ToString(),product);

            if (prd.ErrorMessage == "OK")
            {
                prd = new ProductModel { ErrorMessage = "OK" };
                return RedirectToAction("Index");
            }

            return View(prd);
        }



        // GET: Category/Edit/5
        public ActionResult Edit(string sku)
        {
            ViewBag.Storage = this.Session["_STORAGE"];
            var product = Biz.Product().GetProduct("XML",sku);

            ProductModel prd = new ProductModel
            {
                SKU = product.SKU,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                CurrentUnitPrice = product.CurrentUnitPrice,
                Categories = product.Categories
            };
            prd.CategoriesList = Biz.Category().GetCategoryList("XML");
            prd.ErrorMessage = "";
            return View(prd);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(string sku, FormCollection collection)
        {
            ViewBag.Storage = this.Session["_STORAGE"];
            try
            {
                ProductModel prd = new ProductModel
                {
                    SKU = collection["SKU"].ToString(),
                    ProductName = collection["ProductName"].ToString(),
                    ProductDescription = collection["ProductDescription"].ToString(),
                    CurrentUnitPrice = collection["CurrentUnitPrice"].ToString()
                };
                var categories = collection["Categories[]"];
                prd.Categories = categories.Split(',');
                prd.CategoriesList = Biz.Category().GetCategoryList("XML");

                Product product = new Product
                {
                    SKU = prd.SKU,
                    ProductName = prd.ProductName,
                    ProductDescription = prd.ProductDescription,
                    CurrentUnitPrice = prd.CurrentUnitPrice,
                    Categories = prd.Categories
                };

                prd.ErrorMessage = Biz.Product().UpdateProduct("XML",product);
                // TODO: Add update logic here
                if (prd.ErrorMessage == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(prd);
                }
                
            }
            catch
            {
                return View();
            }
        }


        // GET: Category/Details/5
        public ActionResult Details(string sku)
        {
            ViewBag.Storage = this.Session["_STORAGE"];
            var product = Biz.Product().GetProduct("XML",sku);

            ProductModel prd = new ProductModel
            {
                SKU = product.SKU,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                CurrentUnitPrice = product.CurrentUnitPrice,
                Categories = product.Categories
            };
            prd.CategoriesList = Biz.Category().GetCategoryList("XML");
            prd.ErrorMessage = "";
            return View(prd);
        }

    }
}