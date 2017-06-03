using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using V6Soft.Accounting.Misc.Dealers;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;

namespace V6Soft.Web.Accounting.Controllers
{
    public class AssistantController : Controller 
    {
        private IAssistantDataDealer m_AssistantDealer;
        public AssistantController(IAssistantDataDealer assistantDealer)
        {
            Guard.ArgumentNotNull(assistantDealer, "assistantDealer");
            m_AssistantDealer = assistantDealer;
        }
       
        // GET: /Home/ProvinceList
        public async Task<ActionResult> ProvinceList()
        {
            return null;
        }

        //
        // GET: /Home/DistrictList
        public ActionResult DistrictList()
        {
            return null;
        }

        //
        // GET: /Home/WardList
        public ActionResult WardLis()
        {
            return null;
        }
    }
}
