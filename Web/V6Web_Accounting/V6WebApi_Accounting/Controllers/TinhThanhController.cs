using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Misc.Dealers;
using V6Soft.Accounting.Province.Dealers;
using V6Soft.Models.Accounting.ViewModels.Province;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/tinhthanhs")]
    public class ProvinceController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IProvinceDataDealer m_Dealer;

        #endregion

        #region Construtor
        public ProvinceController(IProvinceDataDealer dealer)
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
        public PagedSearchResult<ProvinceListItem> GetProvinces(SearchCriteria criteria)
        {
            PagedSearchResult<ProvinceListItem> result = m_Dealer.GetProvinces(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddProvince(AccModels.Province tinhthanh)
        {
            tinhthanh.UID = NextUID;
            var result = m_Dealer.AddProvince(tinhthanh);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteProvince(string key)
        {
            m_Dealer.DeleteProvince(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateProvince(AccModels.Province tinhthanh)
        {
            m_Dealer.UpdateProvince(tinhthanh);
            return Ok();
        }
        #endregion
    }
}