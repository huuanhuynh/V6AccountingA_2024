using System.Net;
using System.Web.Http;
using V6Soft.Accounting.MeasurementConversion.Dealers;
using V6Soft.Models.Accounting.ViewModels.MeasurementConversion;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/quydoidonvitinhs")]
    public class MeasurementConversionController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly IMeasurementConversionDataDealer m_Dealer;

        #endregion

        #region Construtor
        public MeasurementConversionController(IMeasurementConversionDataDealer dealer)
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
        public PagedSearchResult<MeasurementConversionListItem> GetMeasurementConversions(SearchCriteria criteria)
        {
            PagedSearchResult<MeasurementConversionListItem> result = m_Dealer.GetMeasurementConversions(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddMeasurementConversion(AccModels.MeasurementConversion quydoidonvitinh)
        {
            quydoidonvitinh.UID = NextUID;
            var result = m_Dealer.AddMeasurementConversion(quydoidonvitinh);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteMeasurementConversion(string key)
        {
            m_Dealer.DeleteMeasurementConversion(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateMeasurementConversion(AccModels.MeasurementConversion quydoidonvitinh)
        {
            m_Dealer.UpdateMeasurementConversion(quydoidonvitinh);
            return Ok();
        }
        #endregion
    }
}