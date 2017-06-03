using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NhomGiaVatTu
{
    public class NhomGiaVatTuController : Controller
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
            ViewBag.Title = "NhomGiaVatTus";

            return PartialView("~/Views/Categories/NhomGiaVatTu/List.cshtml");
        }

    }
}