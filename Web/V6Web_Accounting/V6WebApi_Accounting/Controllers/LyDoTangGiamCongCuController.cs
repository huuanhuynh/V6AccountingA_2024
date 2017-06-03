using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Asset.Dealers;
using V6Soft.Models.Accounting.ViewModels.EquipmentChangedReason;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/lydotanggiamcongcus")]
    public class LyDoTangGiamCongCuController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly ILyDoTangGiamCongCuDataDealer m_Dealer;

        #endregion

        #region Construtor
        public LyDoTangGiamCongCuController(ILyDoTangGiamCongCuDataDealer dealer)
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
        public PagedSearchResult<EquipmentChangedReasonListItem> GetLyDoTangGiamCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<EquipmentChangedReasonListItem> result = m_Dealer.GetLyDoTangGiamCongCus(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddLyDoTangGiamCongCu(DTO.LyDoTangGiamCongCu lydotanggiamcongcu)
        {
            lydotanggiamcongcu.UID = NextUID;
            var result = m_Dealer.AddLyDoTangGiamCongCu(lydotanggiamcongcu);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteLyDoTangGiamCongCu(string key)
        {
            m_Dealer.DeleteLyDoTangGiamCongCu(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateLyDoTangGiamCongCu(DTO.LyDoTangGiamCongCu lydotanggiamcongcu)
        {
            m_Dealer.UpdateLyDoTangGiamCongCu(lydotanggiamcongcu);
            return Ok();
        }
        #endregion
    }
}