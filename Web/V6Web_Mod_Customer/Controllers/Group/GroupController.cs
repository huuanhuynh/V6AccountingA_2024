using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;

namespace V6Soft.Web.Accounting.Modules.Customer.Controllers.Group
{
    public class GroupController : Controller
    {
        private ICustomerDataDealer m_CustomerDealer;

        public GroupController(ICustomerDataDealer customerDealer)
        {
            m_CustomerDealer = customerDealer;
        }

        public async Task<ActionResult> Index()
        {
            PagedList<CustomerGroup> models = null;
            models = await m_CustomerDealer.GetCustomerGroups(
                new List<string>()
                {
                    DefinitionName.Fields.Code,
                    DefinitionName.Fields.Name, 
                    DefinitionName.Fields.Status
                },
                null, 1, 100
            );
            return View(models);
        }

        public ActionResult List()
        {
            return View();
        }
    }
}