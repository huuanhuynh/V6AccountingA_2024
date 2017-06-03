using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Newtonsoft.Json.Linq;
using SummerBreeze;
using V6Soft.Accounting.System.Dealers;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Accounting.Customer.Dealers;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils;
using V6Soft.Accounting.Material.Dealers;
using V6Soft.Accounting.Receipt.Dealers;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.Accounting.Common.Dealers;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [BreezeController]
    [RoutePrefix("api/breeze")]
    public class BreezeApiController : ApiController
    {
        private static IDictionary<string, Type> s_DealerMap = new Dictionary<string, Type>()
        {
            { "Customer", typeof(ICustomerDataDealer)},
             { "V6Option", typeof(IV6OptionDataDealer)},
            { "Material", typeof(IMaterialDataDealer)},
            { "Receipt", typeof(IReceiptDataDealer)},
            { "ReceiptDetail", typeof(IReceiptDataDealer)}
        };

        private readonly SummerBreezeContextProvider m_BzContextProvider;
        private readonly EFContextProvider<V6AccountingContext> m_ContextProvider;
        private readonly ICustomerDataDealer m_CustomerDealer;
        private readonly IV6OptionDataDealer m_V6OptionDealer;
        private readonly IMaterialDataDealer m_MaterialDealer;
        private readonly IReceiptDataDealer m_ReceiptDealer;

        public BreezeApiController(SummerBreezeContextProvider contextProvider, ICustomerDataDealer customerDealer,
            IMaterialDataDealer materialDealer, IReceiptDataDealer receiptDealer,IV6OptionDataDealer v6OptionDataDealer)
        {
            m_BzContextProvider = contextProvider;
            m_CustomerDealer = customerDealer;
            m_MaterialDealer = materialDealer;
            m_V6OptionDealer = v6OptionDataDealer;
            m_ReceiptDealer = receiptDealer;
            m_ContextProvider = new EFContextProvider<V6AccountingContext>();
        }

        //public BreezeApiController(SummerBreezeContextProvider contextProvider, IV6OptionDataDealer v6OptionDealer,
        //  IMaterialDataDealer materialDealer, IReceiptDataDealer receiptDealer)
        //{
        //    m_BzContextProvider = contextProvider;
        //    m_V6OptionDealer = v6OptionDealer;
        //    m_MaterialDealer = materialDealer;
        //    m_ReceiptDealer = receiptDealer;
        //    m_ContextProvider = new EFContextProvider<V6AccountingContext>();
        //}



        // ~/breeze/Metadata
        [HttpGet]
        [Route("metadata")]
        public string Metadata()
        {
            //var mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Models/v6meta.json");
            //return System.IO.File.ReadAllText(mappedPath);
            return m_BzContextProvider.Metadata();
        }

        [HttpGet]
        [Route("ping")]
        public IQueryable<object> Ping()
        {
            return (new object[1]).AsQueryable();
        }

        // ~/breeze/customers
        // ~/breeze/customers?$filter=Status eq false&$orderby=Created
        [HttpGet]
        [Route("customer")]
        //[Authorize]
        public IQueryable<Customer> Customer()
        //public IQueryable<Alkh> Customers()
        {
            return m_CustomerDealer.AsQueryable().OrderBy(x => x.UID);
            //return m_ContextProvider.Context.DMAlkh;
        }

        [HttpGet]
        [Route("v6option")]
        //[Authorize]
        public IQueryable<V6Option> V6Option()
        {
            return m_V6OptionDealer.AsQueryable();
        }

        // ~/breeze/materials
        // ~/breeze/materials?$filter=Status eq false&$orderby=Created
        [HttpGet]
        [Route("material")]
        //[Authorize]
        public IQueryable<Material> Material()
        {
            return m_MaterialDealer.AsQueryable();
        }

        // ~/breeze/receipt
        // ~/breeze/receipt?$filter=Status eq false&$orderby=Created
        [HttpGet]
        [Route("receipt")]
        //[Authorize]
        public IQueryable<Receipt> Receipt()
        {
            return m_ReceiptDealer.AsQueryable();
        }

        // ~/breeze/savechanges
        [HttpPost]
        [Route("savechanges")]
        //[Authorize]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            int count = saveBundle["entities"].Count();
            IList<DynamicObject> objList = null;
            DynamicObject dynObj = null;
            string lastType = null;

            for (int i = 0; i < count; i++)
            {
                dynObj = ParseObject((JObject)saveBundle["entities"][i]);
                if (lastType != dynObj.Type)
                {
                    Process(objList, lastType);
                    lastType = dynObj.Type;
                    objList = new List<DynamicObject>();
                }
                objList.Add(dynObj);
            }

            return new SaveResult();
        }

        private void Process(IList<DynamicObject> dynList, string type)
        {
            if (null == dynList) { return; }
            Type dealerType = s_DealerMap[type];
            IODataFriendly dataDealer = AppContext.DependencyInjector.Resolve(dealerType) as IODataFriendly;
            if (null == dataDealer) { throw new NotSupportedException("No data dealer for this DTO type"); }

            dataDealer.Save(dynList);
        }

        private DynamicObject ParseObject(JObject jsonObject)
        {
            List<string> keys = jsonObject.Properties().Select(p => p.Name).ToList();
            IEnumerable<JProperty> properties = jsonObject.Properties();

            // Expected entityTypeName: Receipt:#V6Soft.Accounting.Model.DTO
            string[] typeParts = jsonObject["entityAspect"]["entityTypeName"].Value<string>().Split(new string[] { ":#" }, StringSplitOptions.RemoveEmptyEntries);

            string type = typeParts[0];
            var dynObj = new DynamicObject(type);

            jsonObject.Remove("entityAspect");

            foreach (var prop in properties)
            {
                dynObj[prop.Name] = jsonObject[prop.Name].Value<object>();
            }

            return dynObj;
        }
    }
}