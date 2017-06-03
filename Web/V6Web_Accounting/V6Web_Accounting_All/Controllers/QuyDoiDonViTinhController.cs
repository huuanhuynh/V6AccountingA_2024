using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.QuyDoiDonViTinh
{
    public class QuyDoiDonViTinhController : Controller
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
            ViewBag.Title = "QuyDoiDonViTinhs";

            return PartialView("~/Views/Categories/QuyDoiDonViTinh/List.cshtml");
        }

    }
}