using System.Net;
using System.Web.Http;
using V6Soft.Accounting.PaymentMethod.Dealers;
using V6Soft.Models.Accounting.ViewModels.PaymentMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/hinhthucthanhtoans")]
    public class PaymentMethodController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IPaymentMethodDataDealer m_Dealer;

        #endregion

        #region Construtor
        public PaymentMethodController(IPaymentMethodDataDealer dealer)
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
        public PagedSearchResult<PaymentMethodListItem> GetPaymentMethods(SearchCriteria criteria)
        {
            PagedSearchResult<PaymentMethodListItem> result = m_Dealer.GetPaymentMethods(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPaymentMethod(AccModels.PaymentMethod hinhthucthanhtoan)
        {
            hinhthucthanhtoan.UID = NextUID;
            var result = m_Dealer.AddPaymentMethod(hinhthucthanhtoan);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeletePaymentMethod(string key)
        {
            m_Dealer.DeletePaymentMethod(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdatePaymentMethod(AccModels.PaymentMethod hinhthucthanhtoan)
        {
            m_Dealer.UpdatePaymentMethod(hinhthucthanhtoan);
            return Ok();
        }
        #endregion
    }
}