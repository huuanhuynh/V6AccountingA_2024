using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.LoaiVatTu
{
    public class LoaiVatTuController : Controller
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
            ViewBag.Title = "LoaiVatTus";

            return PartialView("~/Views/Categories/LoaiVatTu/List.cshtml");
        }

    }
}