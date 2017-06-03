using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.QuocGia
{
    public class QuocGiaController : Controller
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
            ViewBag.Title = "QuocGias";

            return PartialView("~/Views/Categories/QuocGia/List.cshtml");
        }

    }
}