using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.VanChuyen
{
    public class VanChuyenController : Controller
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
            ViewBag.Title = "VanChuyens";

            return PartialView("~/Views/Categories/VanChuyen/List.cshtml");
        }

    }
}