using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.LoaiNhapXuat
{
    public class LoaiNhapXuatController : Controller
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
            ViewBag.Title = "LoaiNhapXuats";

            return PartialView("~/Views/Categories/LoaiNhapXuat/List.cshtml");
        }

    }
}