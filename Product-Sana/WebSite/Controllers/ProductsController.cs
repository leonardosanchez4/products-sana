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
            var model = new List<Product>();
            model = Biz.Product().GetProductList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection model)
        {


            

            ProductModel prd = new ProductModel
            {
                SKU = model["SKU"].ToString(),
                ProductName = model["ProductName"].ToString(),
                ProductDescription = model["ProductDescription"].ToString(),
                CurrentUnitPrice = model["CurrentUnitPrice"].ToString()
            };
            var categories = model["Categories[]"];
            prd.Categories = categories.Split(',');

            Product product = new Product
            {
                SKU = prd.SKU,
                ProductName = prd.ProductName,
                ProductDescription = prd.ProductDescription,
                CurrentUnitPrice = prd.CurrentUnitPrice,
                Categories = prd.Categories
            };

            prd.ErrorMessage = Biz.Product().AddProductCache(product);

            return View(prd);
        }

    }
}