using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.PhanNhomCongCu
{
    public class PhanNhomCongCuController : Controller
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
            ViewBag.Title = "PhanNhomCongCus";

            return PartialView("~/Views/Categories/PhanNhomCongCu/List.cshtml");
        }

    }
}