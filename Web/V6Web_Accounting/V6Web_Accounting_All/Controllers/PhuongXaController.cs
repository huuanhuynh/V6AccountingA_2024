using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.PhuongXa
{
    public class PhuongXaController : Controller
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
            ViewBag.Title = "PhuongXas";

            return PartialView("~/Views/Categories/PhuongXa/List.cshtml");
        }

    }
}