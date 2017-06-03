using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NgoaiTe
{
    public class NgoaiTeController : Controller
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
            ViewBag.Title = "NgoaiTes";

            return PartialView("~/Views/Categories/NgoaiTe/List.cshtml");
        }

    }
}