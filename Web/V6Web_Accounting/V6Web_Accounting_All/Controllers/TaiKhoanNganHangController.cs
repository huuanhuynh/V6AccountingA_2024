using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TaiKhoanNganHang
{
    public class TaiKhoanNganHangController : Controller
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
            ViewBag.Title = "TaiKhoanNganHang";

            return PartialView("~/Views/Categories/TaiKhoanNganHang/List.cshtml");
        }

    }
}