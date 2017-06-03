using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Controllers
{
    public class TemplateController : Controller
    {
        public ActionResult Index(string viewName)
        {
            return View(string.Format("~/Views/Template/{0}.cshtml", viewName));
        }
        
        public ActionResult Menu()
        {
            return View();
        }
    }
}