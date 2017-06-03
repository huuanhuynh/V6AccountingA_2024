using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.NhomKheUoc
{
    public class NhomKheUocController : Controller
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
            ViewBag.Title = "NhomKheUocs";

            return PartialView("~/Views/Categories/NhomKheUoc/List.cshtml");
        }

    }
}