using System.Net;
using System.Web.Http;
using V6Soft.Accounting.MeasurementUnit.Dealers;
using V6Soft.Models.Accounting.ViewModels.MeasurementUnit;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/donvitinhs")]
    public class MeasurementUnitController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMeasurementUnitDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MeasurementUnitController(IMeasurementUnitDataDealer dealer)
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
        public PagedSearchResult<MeasurementUnitListItem> GetMeasurementUnits(SearchCriteria criteria)
        {
            PagedSearchResult<MeasurementUnitListItem> result = m_Dealer.GetMeasurementUnits(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMeasurementUnit(AccModels.MeasurementUnit donvitinh)
        {
            donvitinh.UID = NextUID;
            var result = m_Dealer.AddMeasurementUnit(donvitinh);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMeasurementUnit(string key)
        {
            m_Dealer.DeleteMeasurementUnit(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMeasurementUnit(AccModels.MeasurementUnit donvitinh)
        {
            m_Dealer.UpdateMeasurementUnit(donvitinh);
            return Ok();
        }
        #endregion
    }
}