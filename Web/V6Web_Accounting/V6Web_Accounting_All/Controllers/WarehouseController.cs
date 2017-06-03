using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Controllers
{
    public class WarehouseController : Controller
    {
        public ActionResult Add()
        {
            return PartialView("~/Views/Warehouse/Add.cshtml");
        }

        public ActionResult List()
        {
            ViewBag.Title = "Customers";

            return PartialView("~/Views/Common/List.cshtml");
        }
    }
}