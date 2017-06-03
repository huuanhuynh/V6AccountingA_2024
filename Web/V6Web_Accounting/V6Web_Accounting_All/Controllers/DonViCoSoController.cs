using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.DonViCoSo
{
    public class DonViCoSoController : Controller
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
            ViewBag.Title = "DonViCoSos";

            return PartialView("~/Views/Categories/DonViCoSo/List.cshtml");
        }

    }
}