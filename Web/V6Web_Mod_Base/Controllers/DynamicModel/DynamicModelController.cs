using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Modules.Base.Controllers.DynamicModel
{
    public class DynamicModelController : Controller
    {
        public DynamicModelController()
        {
            string a = "a";
        }
        //
        // GET: {{Names.BaseRoute}}/DynamicModel/List

        public ActionResult List()
        {
            return View();
        }
    }
}
