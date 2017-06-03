using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using V6Soft.Web.Accounting.Modules.Customer.Constants;


namespace V6Soft.Web.Accounting.Modules.Customer.Controllers
{
    public class TemplateController : Controller
    {
        public ActionResult CustomerList()
        {
            return View(GetViewPath("CustomerList"));
        }

        public ActionResult GroupList()
        {
            return View(GetViewPath("GroupList"));
        }


        private string GetViewPath(string fileName)
        {
            return string.Format("~/Views/{0}/Template/{1}.cshtml",
                Names.BaseRoute, fileName);
        }
    }
}