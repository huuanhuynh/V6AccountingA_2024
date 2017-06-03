using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Controllers
{
    public class ComponentController : Controller
    {
        public ActionResult Grid(string viewName)
        {
            return PartialView();
        }
        
        public ActionResult Lookup()
        {
            return PartialView();
        }
    }
}