using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.HinhThucThanhToan
{
    public class HinhThucThanhToanController : Controller
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
            ViewBag.Title = "HinhThucThanhToans";

            return PartialView("~/Views/Categories/HinhThucThanhToan/List.cshtml");
        }

    }
}