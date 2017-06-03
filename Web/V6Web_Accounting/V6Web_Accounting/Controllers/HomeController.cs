using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.Utils;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;

namespace V6Soft.Web.Accounting.Controllers
{
    public class HomeController : Controller
    {
        private ICustomerDataDealer m_CustomerDealer;


        public HomeController(ICustomerDataDealer customerDealer)
        {
            Guard.ArgumentNotNull(customerDealer, "customerDealer");
            m_CustomerDealer = customerDealer;
        }

        //
        // GET: /Home/IndexCrap
        public async Task<ActionResult> IndexCrap()
        {
            await Task.Yield();
            
            try
            {
                dynamic newGroup = new CustomerGroup();
                newGroup.Name = "New group name";
                newGroup.Code = "NEWGRP6";
                newGroup.Status = true;
                AddResult addResult = await m_CustomerDealer.AddCustomerGroup(newGroup);
            }
            catch (Exception exception)
            {
                var ex = exception;
            }

            return View("Index", null);
        }

        //
        // GET: /Home/Index
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
    }
}
