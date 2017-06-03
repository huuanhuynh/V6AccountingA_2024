using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.KhoHang
{
    public class KhoHangController : Controller
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
            ViewBag.Title = "KhoHangs";

            return PartialView("~/Views/Categories/KhoHang/List.cshtml");
        }

    }
}