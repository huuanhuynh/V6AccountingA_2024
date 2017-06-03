using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.SanPhamTrungGian
{
    public class SanPhamTrungGianController : Controller
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
            ViewBag.Title = "SanPhamTrungGians";

            return PartialView("~/Views/Categories/SanPhamTrungGian/List.cshtml");
        }

    }
}