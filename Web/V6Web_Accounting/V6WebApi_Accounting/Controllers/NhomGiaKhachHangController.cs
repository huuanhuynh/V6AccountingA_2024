using System.Net;
using System.Web.Http;
using V6Soft.Accounting.CustomerPriceGroup.Dealers;
using V6Soft.Models.Accounting.ViewModels.CustomerPriceGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/nhomgiakhachhangs")]
    public class CustomerPriceGroupController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ICustomerPriceGroupDataDealer m_Dealer;

        #endregion

        #region Construtor
        public CustomerPriceGroupController(ICustomerPriceGroupDataDealer dealer)
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
        public PagedSearchResult<CustomerPriceGroupListItem> GetCustomerPriceGroups(SearchCriteria criteria)
        {
            PagedSearchResult<CustomerPriceGroupListItem> result = m_Dealer.GetCustomerPriceGroups(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCustomerPriceGroup(AccModels.CustomerPriceGroup nhomgiakhachhang)
        {
            nhomgiakhachhang.UID = NextUID;
            var result = m_Dealer.AddCustomerPriceGroup(nhomgiakhachhang);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteCustomerPriceGroup(string key)
        {
            m_Dealer.DeleteCustomerPriceGroup(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateCustomerPriceGroup(AccModels.CustomerPriceGroup nhomgiakhachhang)
        {
            m_Dealer.UpdateCustomerPriceGroup(nhomgiakhachhang);
            return Ok();
        }
        #endregion
    }
}