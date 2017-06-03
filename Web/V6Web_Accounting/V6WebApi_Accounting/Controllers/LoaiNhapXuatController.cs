using System.Net;
using System.Web.Http;
using V6Soft.Accounting.LoaiNhapXuat.Dealers;
using V6Soft.Models.Accounting.ViewModels.LoaiNhapXuat;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/loainhapxuats")]
    public class LoaiNhapXuatController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ILoaiNhapXuatDataDealer m_Dealer;

        #endregion

        #region Construtor
        public LoaiNhapXuatController(ILoaiNhapXuatDataDealer dealer)
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
        public PagedSearchResult<LoaiNhapXuatItem> GetLoaiNhapXuats(SearchCriteria criteria)
        {
            PagedSearchResult<LoaiNhapXuatItem> result = m_Dealer.GetLoaiNhapXuats(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddLoaiNhapXuat(AccModels.LoaiNhapXuat loainhapxuat)
        {
            loainhapxuat.UID = NextUID;
            var result = m_Dealer.AddLoaiNhapXuat(loainhapxuat);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteLoaiNhapXuat(string key)
        {
            m_Dealer.DeleteLoaiNhapXuat(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateLoaiNhapXuat(AccModels.LoaiNhapXuat loainhapxuat)
        {
            m_Dealer.UpdateLoaiNhapXuat(loainhapxuat);
            return Ok();
        }
        #endregion
    }
}