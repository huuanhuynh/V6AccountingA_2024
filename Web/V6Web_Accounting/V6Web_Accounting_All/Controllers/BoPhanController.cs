using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.BoPhan
{
    public class BoPhanController : Controller
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
            ViewBag.Title = "BoPhans";

            return PartialView("~/Views/Common/List.cshtml");
        }

    }
}