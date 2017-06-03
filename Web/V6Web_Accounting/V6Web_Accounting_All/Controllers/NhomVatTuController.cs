using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NhomVatTu
{
    public class NhomVatTuController : Controller
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
            ViewBag.Title = "NhomVatTus";

            return PartialView("~/Views/Categories/NhomVatTu/List.cshtml");
        }

    }
}