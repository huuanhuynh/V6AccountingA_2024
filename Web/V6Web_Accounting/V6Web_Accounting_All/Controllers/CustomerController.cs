using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.Customer
{
    public class CustomerController : Controller
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
            return PartialView("~/Views/Frontend/Customer/List.cshtml");
        }

        public ActionResult Detail()
        {
            return PartialView("~/Views/Frontend/Customer/Detail.cshtml");
        }

    }
}