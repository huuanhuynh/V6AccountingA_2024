using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.LoaiDichVu
{
    public class LoaiDichVuController : Controller
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
            ViewBag.Title = "LoaiDichVus";

            return PartialView("~/Views/Categories/LoaiDichVu/List.cshtml");
        }

    }
}