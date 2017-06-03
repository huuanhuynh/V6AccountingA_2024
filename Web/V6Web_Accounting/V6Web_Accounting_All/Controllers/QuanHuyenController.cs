using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.QuanHuyen
{
    public class QuanHuyenController : Controller
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
            ViewBag.Title = "QuanHuyens";

            return PartialView("~/Views/Categories/QuanHuyen/List.cshtml");
        }

    }
}