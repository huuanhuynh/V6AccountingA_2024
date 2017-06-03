using System.Collections.Generic;
using System.Web.Http;
using V6AccountingBusiness.Helpers;
using V6AccountingBusiness.Interfaces;
using V6Init;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/static")]
    //[Authorize]
    public class StaticController : ApiController
    {
        private readonly IV6Categories Services;
        public StaticController(IV6Categories services)
        {
            Services = services;
        }
        
        
        [HttpGet]
        [Route("GetDefaultLookupFields")]
        public IHttpActionResult GetDefaultLookupFields(string tableName)
        {
            var result = V6Lookup.GetDefaultLookupFields(tableName);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("GetFieldHeader")]
        public IHttpActionResult GetFieldHeader(string name)
        {
            var result = CorpLan2.GetFieldHeader(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("SelectPaging")]
        public IHttpActionResult SelectPaging(string tableName, string fields, int page, int size,
            string getWhere, string sortField, bool ascending)
        {
            var result = Services.SelectPaging(tableName, fields, page, size, getWhere, sortField, ascending);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IHttpActionResult Insert(string tableName, SortedDictionary<string, object> keys)
        {
            var result = Services.Delete(tableName, keys);
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(string tableName, List<SortedDictionary<string, object>> data_keys)
        {
            var data = data_keys[0];
            var keys = data_keys[1];
            var result = Services.Update(V6BusinessHelper.ToV6TableName(tableName), data, keys);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string tableName, SortedDictionary<string,object> keys)
        {
            var result = Services.Delete(tableName, keys);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetHideColumns")]
        public IHttpActionResult GetHideColumns(string tableName)
        {
            var result = Services.GetHideColumns(tableName);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTableStruct")]
        public IHttpActionResult GetTableStruct(string tableName)
        {
            var result = V6BusinessHelper.GetTableStruct(tableName);
            return Ok(result);
        }


    }
}
