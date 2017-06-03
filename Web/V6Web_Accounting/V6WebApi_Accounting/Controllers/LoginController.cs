using System;
using System.Data;
using System.Web.Http;
using DataAccessLayer.Interfaces;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/login")]
    //[Authorize]
    public class LoginController : ApiController
    {
        private ILoginServices m_LoginServices;
        public LoginController(ILoginServices loginServices)
        {
            m_LoginServices = loginServices;
        }

        [HttpGet]
        [Route("GetServerDateTime")]
        public IHttpActionResult GetServerDateTime()
        {
            var result = m_LoginServices.GetServerDateTime();
            return Ok(result);
        }

        [HttpGet]
        [Route("moduleTable")]
        public IHttpActionResult GetModuleTable()
        {
            var result = m_LoginServices.GetModuleTable();
            return Ok(result);
        }

        [HttpGet]
        [Route("languageTable")]
        public IHttpActionResult GetLanguageTable()
        {
            var result = m_LoginServices.GetLanguageTable();
            return Ok(result);
        }

        [HttpGet]
        [Route("agentTable")]
        public IHttpActionResult GetAgentTable(string key, string lang)
        {
            var result = m_LoginServices.GetAgentTable(key, lang);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("defaultValues")]
        //public IHttpActionResult GetDefaultValues()
        //{
        //    var result = m_LoginServices.GetDefaultValues();
        //    return Ok(result);
        //}

        [HttpGet]
        [Route("userInfo")]
        public IHttpActionResult GetUserInfo(string userName)
        {
            var result = m_LoginServices.GetUserRow(userName);
            return Ok(result);
        }

        [HttpGet]
        [Route("countDvcs")]
        public IHttpActionResult GetDVCSCount()
        {
            var countDvcs = m_LoginServices.CountDvcs();
            return Ok(countDvcs);
        }


        //[HttpGet]
        //[Route("agentTable")]
        //public IHttpActionResult GetAgentTable(string key)
        //{
        //    var result = m_LoginServices.GetAgentTable(key);
        //    return Ok(result);
        //}

    }
}
