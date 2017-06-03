using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.DanhMucThueSuat
{
    public class DanhMucThueSuatController : Controller
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
            ViewBag.Title = "DanhMucMaGias";

            return PartialView("~/Views/Categories/DanhMucThueSuat/List.cshtml");
        }

    }
}