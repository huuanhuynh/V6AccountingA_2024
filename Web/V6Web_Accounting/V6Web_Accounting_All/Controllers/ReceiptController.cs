using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Controllers
{
    public class ReceiptController : Controller
    {
        public ActionResult Detail()
        {
            return PartialView("~/Views/Frontend/Receipt/Detail.cshtml");
        }
    }
}