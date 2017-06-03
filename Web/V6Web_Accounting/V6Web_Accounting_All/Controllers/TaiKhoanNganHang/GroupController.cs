using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace V6Soft.Web.Accounting.Controllers.TaiKhoanNganHang
{
    public class GroupController : Controller
    {

        public async Task<ActionResult> Index()
        {
            return PartialView();
        }

        public ActionResult Add()
        {
            return PartialView();
        }

        public ActionResult List()
        {
            return PartialView();
        }       

    }
}