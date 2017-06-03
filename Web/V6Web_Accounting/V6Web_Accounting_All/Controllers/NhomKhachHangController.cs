using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NhomKhachHang
{
    public class NhomKhachHangController : Controller
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
            ViewBag.Title = "NhomKhachHang";

            return PartialView("~/Views/Categories/NhomKhachHang/List.cshtml");
        }

    }
}