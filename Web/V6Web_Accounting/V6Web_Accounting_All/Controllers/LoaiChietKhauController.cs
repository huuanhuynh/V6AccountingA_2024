using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.LoaiChietKhau
{
    public class LoaiChietKhauController : Controller
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
            ViewBag.Title = "LoaiChietKhaus";

            return PartialView("~/Views/Categories/LoaiChietKhau/List.cshtml");
        }

    }
}