using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using V6AccountingBusiness.Interfaces;
using V6AccoutingB_Services;
using V6Init;
using V6Soft.Models.Accounting.DTO;
using V6SqlConnect;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/mainform")]
    [Authorize]
    public class MainFormController : ApiController
    {
        private IMainformServices m_MainformServices;
        public MainFormController(IMainformServices mainformServices)
        {
            m_MainformServices = mainformServices;
        }

        

        [HttpGet]
        [Route("VPA_V6VIEW_MESSAGE")]
        public IHttpActionResult VPA_V6VIEW_MESSAGE(string type, string advance1,
            string advance2, string advance3)
        {
            var result = m_MainformServices.VPA_V6VIEW_MESSAGE(V6LoginInfo.UserId, type, advance1, advance2, advance3);
            return Ok(result);
        }

        [HttpPost]
        [Route("VPA_00_POST_USERLOG")]
        public IHttpActionResult VPA_00_POST_USERLOG(Dictionary<string, object> plist)
        {
            var body = new SqlParameter[]
            {
                new SqlParameter("@user_name",plist["user_name"]),
                new SqlParameter("@user_id", Convert.ToInt32(plist["user_id"])),
                new SqlParameter("@itemid", plist["itemid"]),
                new SqlParameter("@action", plist["action"]),
                new SqlParameter("@status", plist["status"]),
                new SqlParameter("@type", plist["type"])
            };
            var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_00_POST_USERLOG", body);
            return Ok(result);
        }
    }
}
