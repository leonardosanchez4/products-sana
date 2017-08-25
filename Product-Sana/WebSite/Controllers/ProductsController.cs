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
            return View();
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


            return View();
        }

    }
}