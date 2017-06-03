using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.MauHoaDon
{
    public class MauHoaDonController : Controller
    {
        public ActionResult Add()
        {
            return PartialView();
        }

        public async Task<ActionResult> Edit(string uid)
        {
            return PartialView();
        }

        public ActionResult List()
        {
            ViewBag.Title = "MauHoaDons";

            return PartialView("~/Views/Categories/MauHoaDon/List.cshtml");
        }

    }
}