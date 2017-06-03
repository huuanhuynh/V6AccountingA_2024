using System.Web.Http;
using DataAccessLayer.Interfaces;
using V6Soft.Models.Accounting.DTO;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/menu")]
    [Authorize]
    public class MenuController : ApiController
    {
        private IMenuServices Services;
        public MenuController(IMenuServices menuServices)
        {
            Services = menuServices;
        }

        [HttpGet]
        [Route("grandMenu")]
        public IHttpActionResult GetGrandMenu(string moduleId)
        {
            var userId = this.GetUserId();
            var result = Services.GetGrandMenu(userId, moduleId);
            return Ok(result);
        }

        [HttpGet]
        [Route("parentMenu")]
        public IHttpActionResult GetParentMenu(string moduleId, string v2Id)
        {
            var userId = this.GetUserId();
            var result = Services.GetParentMenu(userId, moduleId, v2Id);
            return Ok(result);
        }

        [HttpGet]
        [Route("childMenu")]
        public IHttpActionResult GetChildMenu(string moduleId, string v2Id, string jobId)
        {
            var userId = this.GetUserId();
            var result = Services.GetChildMenu(userId, moduleId, v2Id, jobId);
            return Ok(result);
        }
    }
}
