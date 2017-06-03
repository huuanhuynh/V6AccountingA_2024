using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V6WebApi_Accounting.Controllers
{
    /// <summary>
    /// Can be used for displaying welcome web page
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
