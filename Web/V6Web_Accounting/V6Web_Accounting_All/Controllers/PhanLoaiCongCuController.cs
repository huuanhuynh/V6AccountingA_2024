using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.PhanLoaiCongCu
{
    public class PhanLoaiCongCuController : Controller
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
            ViewBag.Title = "PhanLoaiCongCus";

            return PartialView("~/Views/Categories/PhanLoaiCongCu/List.cshtml");
        }

    }
}