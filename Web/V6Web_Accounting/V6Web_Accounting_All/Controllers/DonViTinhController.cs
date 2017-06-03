using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.DonViTinh
{
    public class DonViTinhController : Controller
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
            ViewBag.Title = "DonViTinhs";

            return PartialView("~/Views/Categories/DonViTinh/List.cshtml");
        }

    }
}