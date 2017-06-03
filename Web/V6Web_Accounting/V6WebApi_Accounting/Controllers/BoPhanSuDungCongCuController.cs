using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Department.Dealers;
using V6Soft.Models.Accounting.ViewModels.BoPhanSuDungCongCu;
using V6Soft.Models.Accounting.ViewModels.Department;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/bophansudungcongcus")]
    public class BoPhanSuDungCongCuController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IBoPhanSuDungCongCuDataDealer m_Dealer;

        #endregion

        #region Construtor
        public BoPhanSuDungCongCuController(IBoPhanSuDungCongCuDataDealer dealer)
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
        public PagedSearchResult<BoPhanSuDungCongCuItem> GetBoPhanSuDungCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<BoPhanSuDungCongCuItem> result = m_Dealer.GetBoPhanSuDungCongCus(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddBoPhanSuDungCongCu(Models.Accounting.DTO.BoPhanSuDungCongCu bophansudungcongcu)
        {
            bophansudungcongcu.UID = NextUID;
            var result = m_Dealer.AddBoPhanSuDungCongCu(bophansudungcongcu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteBoPhanSuDungCongCu(string key)
        {
            m_Dealer.DeleteBoPhanSuDungCongCu(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateBoPhanSuDungCongCu(AccModels.BoPhanSuDungCongCu bophansudungcongcu)
        {
            m_Dealer.UpdateBoPhanSuDungCongCu(bophansudungcongcu);
            return Ok();
        }
        #endregion
    }
}