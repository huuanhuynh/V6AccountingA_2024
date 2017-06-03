using System.Net;
using System.Web.Http;
using V6Soft.Accounting.InvoiceTemplate.Dealers;
using V6Soft.Models.Accounting.ViewModels.InvoiceTemplate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/mauhoadons")]
    public class InvoiceTemplateController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IInvoiceTemplateDataDealer m_Dealer;

        #endregion

        #region Construtor
        public InvoiceTemplateController(IInvoiceTemplateDataDealer dealer)
        {
            m_Dealer = dealer;
        }
        #endregion

        #region Ping
        [HttpGet]
        public IHttpActionResult Ping()
        {
            object result;
            if (m_Dealer != null)
            {
                result = new { Resolved = true };
            }
            else
            {
                result = new { Resolved = false };
            }
            return Ok(result);
        }
        #endregion

        #region Getting
        [HttpPost]
        [Route("list")]
        public PagedSearchResult<InvoiceTemplateListItem> GetInvoiceTemplates(SearchCriteria criteria)
        {
            PagedSearchResult<InvoiceTemplateListItem> result = m_Dealer.GetInvoiceTemplates(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddInvoiceTemplate(AccModels.InvoiceTemplate mauhoadon)
        {
            mauhoadon.UID = NextUID;
            var result = m_Dealer.AddInvoiceTemplate(mauhoadon);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteInvoiceTemplate(string key)
        {
            m_Dealer.DeleteInvoiceTemplate(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateInvoiceTemplate(AccModels.InvoiceTemplate mauhoadon)
        {
            m_Dealer.UpdateInvoiceTemplate(mauhoadon);
            return Ok();
        }
        #endregion
    }
}