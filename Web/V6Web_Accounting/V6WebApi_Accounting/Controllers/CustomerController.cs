using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Customer.Dealers;
using V6Soft.Models.Accounting.ViewModels.Customer;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    
    [RoutePrefix("api/customers")]
    public class CustomerController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ICustomerDataDealer m_Dealer;

        #endregion

        #region Construtor
        public CustomerController(ICustomerDataDealer dealer)
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
        //[Authorize]
        public PagedSearchResult<CustomerListItem> GetCustomers(SearchCriteria criteria)
        {
            PagedSearchResult<CustomerListItem> result = m_Dealer.GetCustomers(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCustomer(AccModels.Customer customer)
        {
            customer.UID = NextUID;
            var result = m_Dealer.AddCustomer(customer);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteCustomer(string key)
        {
            m_Dealer.DeleteCustomer(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateCustomer(AccModels.Customer customer)
        {
            m_Dealer.UpdateCustomer(customer);
            return Ok();
        }
        #endregion
    }
} 