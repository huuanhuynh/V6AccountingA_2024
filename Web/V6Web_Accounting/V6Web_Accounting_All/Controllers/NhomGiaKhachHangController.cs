using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NhomGiaKhachHang
{
    public class NhomGiaKhachHangController : Controller
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
            ViewBag.Title = "NhomGiaKhachHangs";

            return PartialView("~/Views/Categories/NhomGiaKhachHang/List.cshtml");
        }

    }
}