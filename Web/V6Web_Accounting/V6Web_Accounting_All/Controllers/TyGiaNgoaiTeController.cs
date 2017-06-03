using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TyGiaNgoaiTe
{
    public class TyGiaNgoaiTeController : Controller
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
            ViewBag.Title = "TyGiaNgoaiTes";

            return PartialView("~/Views/Categories/TyGiaNgoaiTe/List.cshtml");
        }

    }
}