using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.HinhThucVanChuyen
{
    public class HinhThucVanChuyenController : Controller
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
            ViewBag.Title = "HinhThucVanChuyens";

            return PartialView("~/Views/Categories/HinhThucVanChuyen/List.cshtml");
        }

    }
}