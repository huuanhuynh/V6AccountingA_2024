using System.Threading.Tasks;
using System.Web.Mvc;

namespace V6Soft.Web.Accounting.Controllers
{
    public class DonDatHangBanController : Controller
    {
        public ActionResult Add()
        {
            return PartialView();
        }

        public async Task<ActionResult> Edit(string uid)
        {
            return PartialView();
        }

        public ActionResult DonDatHangBan()
        {
            ViewBag.Title = "DonDatHangBans";

            return PartialView("~/Views/DonDatHangBan/DonDatHangBan.cshtml");
        }

    }
}