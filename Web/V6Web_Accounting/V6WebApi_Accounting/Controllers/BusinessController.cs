using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccessLayer.Interfaces;
using V6Structs;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/business")]
    //[Authorize]
    public class BusinessController : ApiController
    {
        private readonly IBusinessServices Services;
        public BusinessController(IBusinessServices services)
        {
            Services = services;
        }

        
        [HttpGet]
        [Route("GetServerDateTime")]
        public IHttpActionResult GetServerDateTime()
        {
            var result = Services.GetServerDateTime();
            return Ok(result);
        }

        [HttpPost]
        [Route("Select")]
        public IHttpActionResult Select(V6SelectModel selectModel)
        {
            var result = Services.Select(selectModel.TableName, selectModel.Fields, selectModel.Where, selectModel.Group, selectModel.Sort);
            return Ok(result);
        }

        [HttpGet]
        [Route("SelectTable")]
        public IHttpActionResult SelectTable(string tableName)
        {
            var result = Services.SelectTable(tableName);
            return Ok(result);
        }

        [HttpPost]
        [Route("SelectPaging")]
        public IHttpActionResult SelectPaging(V6SelectModel selectModel)
        {
            var result = Services.SelectPaging(selectModel.TableName, selectModel.Fields, selectModel.Page,
                selectModel.Size, selectModel.Where, selectModel.Sort, selectModel.Ascending);
            return Ok(result);
        }

        [HttpPost]
        [Route("ExecuteProcedure/procName/{procName}")]
        public IHttpActionResult ExecuteProcedure(string procName, Dictionary<string, string> parameters)
        {
            var result = Services.ExecuteProcedure(procName, parameters);
            return Ok(result);
        }

        [HttpPost]
        [Route("ExecuteProcedureScalar/procName/{procName}")]
        public IHttpActionResult ExecuteProcedureScalar(string procName, Dictionary<string, string> parameters)
        {
            var result = Services.ExecuteProcedureScalar(procName, parameters);
            return Ok(result);
        }

        [HttpPost]
        [Route("ExecuteProcedureNoneQuery/procName/{procName}")]
        //[Route("ExecuteProcedure")]
        public IHttpActionResult ExecuteProcedureNoneQuery(string procName, Dictionary<string, string> parameters)
        {
            try
            {
                var result = Services.ExecuteProcedureNoneQuery(procName, parameters);
                return Ok(new SimpleResult
                {
                    Status = 1,
                    IntValue = result
                });
            }
            catch (Exception ex)
            {
                return Ok(new SimpleResult
                {
                    Status = 0,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(string tableName, List<SortedDictionary<string, object>> data_keys)
        {
            var data = data_keys[0];
            var keys = data_keys[1];
            var result = Services.Update(tableName, data, keys);
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
            var result = Services.GetTableStruct(tableName);
            return Ok(result);
        }

        [HttpGet]
        [Route("IsValidOneCode_Full")]
        public IHttpActionResult IsValidOneCode_Full(string cInputTable, byte nStatus,
            string cInputField, string cpInput, string cOldItems)
        {
            try
            {
                var result = Services.IsValidOneCode_Full(cInputTable, nStatus, cInputField, cpInput, cOldItems);
                var sResult = new SimpleResult
                {
                    Status = 1,
                    IntValue = result ? 1 : 0,
                    Message = "Success"
                };
                return Ok(sResult);
            }
            catch (Exception ex)
            {
                var sResult = new SimpleResult
                {
                    Status = 0,
                    IntValue = 0,
                    Message = ex.Message
                };
                return Ok(sResult);
            }
        }

        [HttpGet]
        [Route("IsExistOneCode_List")]
        public IHttpActionResult IsExistOneCode_List(string listTable, string field, string value)
        {
            try
            {
                bool result = Services.IsExistOneCode_List(listTable, field, value);
                return Ok(new SimpleResult { Status = 1, IntValue = result ? 1 : 0 });
            }
            catch (Exception ex)
            {
                return Ok(new SimpleResult { Status = 0, Message = ex.Message });
            }
        }
        
    }
}
