using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class StorageController : Controller
    {
      
        [System.Web.Mvc.HttpGet]
        public string SetStorage(bool st)
        {

            try
            {
                var currentStorage = this.Session["_STORAGE"].ToString();
                if (st)
                {
                    this.Session["_STORAGE"] = "XML";
                }
                else
                {
                    this.Session["_STORAGE"] = "Cache";
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error";
            }



        }
    }
}
