using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.DanhMucMaGia
{
    public class DanhMucMaGiaController : Controller
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
            ViewBag.Title = "DanhMucMaGias";

            return PartialView("~/Views/Categories/DanhMucMaGia/List.cshtml");
        }

    }
}