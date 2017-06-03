using System.Web.Http;
using V6Soft.Accounting.Membership.Dealers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.Membership.Dto;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : V6ApiControllerBase
    {
        private readonly IUserDataDealer m_Dealer;

        public UserController(IUserDataDealer dealer)
        {
            m_Dealer = dealer;
        }

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

        public object Dump()
        {
            object result = new { Name = "dumper", Age = 18 };
            return result;
        }

        [HttpPost]
        [Route("list")]
        public PagedSearchResult<UserListItem> GetUsers(SearchCriteria criteria)
        {
            PagedSearchResult<UserListItem> result =
                m_Dealer.GetUsers(criteria);
            return result;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddUser(User user)
        {
            var result = m_Dealer.AddUser(user);
            return Ok(result);
        }
    }
}