using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TinhTrangDichVu
{
    public class TinhTrangDichVuController : Controller
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
            ViewBag.Title = "TinhTrangDichVus";

            return PartialView("~/Views/Categories/TinhTrangDichVu/List.cshtml");
        }

    }
}