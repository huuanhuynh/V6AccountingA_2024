using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using DataAccessLayer.Interfaces;
using V6SqlConnect;
using V6Structs;
using V6Tools.V6Convert;

namespace V6Soft.WebApi.Accounting.Controllers
{
    [RoutePrefix("api/categories")]
    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesServices Services;
        public CategoriesController(ICategoriesServices services)
        {
            Services = services;
        }


        [HttpPost]
        [Route("Select")]
        public IHttpActionResult Select(V6SelectModel selectModel)
        {
            //if (selectModel.Where.Contains("'"))
            //{
            //    throw new Exception("Xem lại tham số Where.");
            //}
            List<SqlParameter> plist = new List<SqlParameter>();
            if (selectModel.Parameters != null)
            {
                foreach (KeyValuePair<string, object> item in selectModel.Parameters)
                {
                    plist.Add(new SqlParameter(item.Key, item.Value));
                }
            }
            var result = Services.Select(selectModel.TableName, selectModel.Fields, selectModel.Where, selectModel.Group, selectModel.Sort, plist.ToArray());
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
            if (selectModel.Where.Contains("'"))
            {
                throw new Exception("Xem lại tham số Where.");
            }
            var result = Services.SelectPaging(selectModel.TableName, selectModel.Fields, selectModel.Page,
                selectModel.Size, selectModel.Where, selectModel.Sort, selectModel.Ascending);
            return Ok(result);
        }

        [HttpPost]
        [Route("Insert/tableName/{tableName}")]
        public IHttpActionResult Insert(string tableName, SortedDictionary<string, object> data)
        {
            try
            {
                var id = this.GetUserId();
                var result = Services.Insert(id, tableName, data);
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
        [Route("Update/tableName/{tableName}")]
        public IHttpActionResult Update(string tableName, List<SortedDictionary<string, object>> data_keys)
        {
            try
            {
                var id = this.GetUserId();
                var data = data_keys[0];
                var keys = data_keys[1];
                var result = Services.Update(id, tableName, data, keys);
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

        [HttpDelete]
        [Route("Delete/tableName/{tableName}")]
        public IHttpActionResult Delete(string tableName, SortedDictionary<string, object> keys)
        {
            try
            {
                var id = this.GetUserId();
                var result = Services.Delete(id, tableName, keys);
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

        

    }
}
