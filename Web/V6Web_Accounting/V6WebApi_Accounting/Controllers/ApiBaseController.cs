using System.Collections.Generic;
using System.Linq;
using System.Web.OData;
using System.Web.OData.Extensions;


namespace V6Soft.WebApi.Accounting.Controllers
{
    public abstract class ApiBaseController : ODataController
    {
        protected PageResult<T> ToPageResult<T>(IQueryable<T> itemQueryable)
        {
            var items = itemQueryable.ToList();
            long? total = Request.ODataProperties().TotalCount;
            return new PageResult<T>(items, null, total);
        }
    }
}