using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TinhThanh
{
    public class TinhThanhController : Controller
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
            ViewBag.Title = "TinhThanhs";

            return PartialView("~/Views/Categories/TinhThanh/List.cshtml");
        }

    }
}