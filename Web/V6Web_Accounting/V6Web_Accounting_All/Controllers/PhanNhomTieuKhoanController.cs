using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.PhanNhomTieuKhoan
{
    public class PhanNhomTieuKhoanController : Controller
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
            ViewBag.Title = "PhanNhomTieuKhoans";

            return PartialView("~/Views/Categories/PhanNhomTieuKhoan/List.cshtml");
        }

    }
}