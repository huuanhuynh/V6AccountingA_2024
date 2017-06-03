using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;

using Microsoft.OData.Core;

using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Accounting.Receipt.Dealers;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [ODataRoutePrefix("Receipt")]
    public class ReceiptController : ApiBaseController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        private readonly IReceiptDataDealer m_ReceiptDealer;

        public ReceiptController(IReceiptDataDealer receiptDealer)
        {
            m_ReceiptDealer = receiptDealer;
        }

        // See: http://www.asp.net/web-api/overview/odata-support-in-aspnet-web-api/odata-v4/create-an-odata-v4-endpoint

        // GET: odata/Receipt
        [ODataRoute]
        public PageResult<Receipt> Get(ODataQueryOptions<Receipt> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                throw ex;
                //return BadRequest(ex.Message);
            }
            var receiptQueryable = m_ReceiptDealer.AsQueryable(queryOptions);
            return ToPageResult(receiptQueryable);            
        }

        // GET: odata/Receipt(5)
        public async Task<IHttpActionResult> Get([FromODataUri] string key, ODataQueryOptions<Alkh> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            var alkh = queryOptions.ApplyTo(null);
            Receipt receipt = m_ReceiptDealer.GetReceipt(new Guid(key));
            return Ok(receipt);
        }

        // PUT: odata/Receipt(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Receipt receipt)
        {
            Validate(receipt);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Receipt updatedReceipt = m_ReceiptDealer.UpdateReceipt(receipt);

            return Updated(updatedReceipt);
        }

        // POST: odata/Receipt
        public async Task<IHttpActionResult> Post(Receipt receipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Receipt newReceipt = m_ReceiptDealer.AddReceipt(receipt);

            return Created(newReceipt);
        }


        // DELETE: odata/Receipt(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }    
}
