using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.LyDoTangGiamCongCu
{
    public class LyDoTangGiamCongCuController : Controller
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
            ViewBag.Title = "LyDoTangGiamCongCus";

            return PartialView("~/Views/Categories/LyDoTangGiamCongCu/List.cshtml");
        }

    }
}