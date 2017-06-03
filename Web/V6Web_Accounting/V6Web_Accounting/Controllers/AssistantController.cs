using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.Utils;
using V6Soft.Interfaces.Accounting.Assistant.DataDealers;
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
            PagedList<Province> models = null;
            models = await m_AssistantDealer.GetProvinces(
                new List<string>() { 
                    DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Status },
                null, 1, 100
            );
            return View(models);
        }

        //
        // GET: /Home/DistrictList
        public ActionResult DistrictList()
        {
            PagedList<District> models = null;
            models = m_AssistantDealer.GetDistricts(
                new List<string>()
                {
                    DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Status
                },
                null, 1, 10
            ).Result;
            return View(models);
        }

        //
        // GET: /Home/WardList
        public ActionResult WardLis()
        {
            PagedList<Ward> models = null;
            models = m_AssistantDealer.GetWards(
                new List<string>()
                {
                    DefinitionName.Fields.Code, 
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Status
                },
                null, 1, 100
            ).Result;
            return View(models);
        }
    }
}
