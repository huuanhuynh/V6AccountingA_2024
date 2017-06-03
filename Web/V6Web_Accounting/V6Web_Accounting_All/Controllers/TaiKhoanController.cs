using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TaiKhoan
{
    public class TaiKhoanController : Controller
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
            ViewBag.Title = "TaiKhoans";

            return PartialView("~/Views/Categories/TaiKhoan/List.cshtml");
        }

    }
}