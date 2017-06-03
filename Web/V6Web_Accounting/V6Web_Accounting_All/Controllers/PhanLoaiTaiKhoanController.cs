using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.PhanLoaiTaiKhoan
{
    public class PhanLoaiTaiKhoanController : Controller
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
            ViewBag.Title = "PhanLoaiTaiKhoans";

            return PartialView("~/Views/Categories/PhanLoaiTaiKhoan/List.cshtml");
        }

    }
}