using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Receipt.Dealers;
using V6Soft.Models.Accounting.DTO;

namespace V6Soft.WebApi.Accounting.Controllers
{
    public class ReceiptDetailsController : ODataController
    {

        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IReceiptDataDealer m_ReceiptDealer;

        public ReceiptDetailsController(IReceiptDataDealer receiptDealer)
        {
            m_ReceiptDealer = receiptDealer;
        }

        private V6AccountingContext db = new V6AccountingContext();

        // GET: odata/ReceiptDetails
        [EnableQuery]
        public IQueryable<ReceiptDetail> Get(ODataQueryOptions<Receipt> queryOptions)
        {
            return null;
        }


        [HttpGet]
        public IHttpActionResult DetailCol()
        {
            var cols = new List<string>() { "SttRec", "MaKH" };
            return Ok(cols);
        }


        // GET: odata/ReceiptDetails(5)
        [EnableQuery]
        public SingleResult<ReceiptDetail> Get([FromODataUri] string key)
        {
            //return SingleResult.Create(db.ReceiptDetails.Where(receiptDetail => receiptDetail.SttRec == key));
            return null;
        }
        
    }
}
