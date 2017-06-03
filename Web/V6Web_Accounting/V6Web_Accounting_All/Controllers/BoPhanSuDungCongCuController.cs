using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.BoPhanSuDungCongCu
{
    public class BoPhanSuDungCongCuController : Controller
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
            ViewBag.Title = "BoPhanSuDungCongCus";

            return PartialView("~/Views/Categories/BoPhanSuDungCongCu/List.cshtml");

        }

    }
}