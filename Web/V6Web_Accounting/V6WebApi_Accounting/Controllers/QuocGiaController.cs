﻿using System.Net;
using System.Web.Http;
using V6Soft.Accounting.Nation.Dealers;
using V6Soft.Models.Accounting.ViewModels.Nation;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Web.Common.Controllers;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/quocgias")]
    public class NationController : V6ApiControllerBase
    {
        #region parameter declare part.

        private readonly INationDataDealer m_Dealer;

        #endregion

        #region Construtor
        public NationController(INationDataDealer dealer)
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
        public PagedSearchResult<NationListItem> GetNations(SearchCriteria criteria)
        {
            PagedSearchResult<NationListItem> result = m_Dealer.GetNations(criteria);
            return result;
        }
        #endregion

        #region Adding

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddNation(AccModels.Nation quocgia)
        {
            quocgia.UID = NextUID;
            var result = m_Dealer.AddNation(quocgia);
            return Ok(result);
        }
        #endregion

        #region Deleting
        [HttpDelete]
        [Route("{key}")]
        public IHttpActionResult DeleteNation(string key)
        {
            m_Dealer.DeleteNation(key);
            return Ok(HttpStatusCode.NoContent);
        }
        #endregion

        #region Updating
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateNation(AccModels.Nation quocgia)
        {
            m_Dealer.UpdateNation(quocgia);
            return Ok();
        }
        #endregion
    }
}