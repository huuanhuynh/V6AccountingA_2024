using System;
using System.Linq;
using System.Web.Http.OData.Query;
using V6Soft.Accounting.Common.Dealers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.BoPhan.Dealers
{
    /// <summary>
    ///     Acts as a service client to get BoPhan data from BoPhanService.
    /// </summary>
    public interface IBoPhanDataDealer : IODataFriendly<DTO.Department>
    {
        IQueryable<DTO.Department> AsQueryable(ODataQueryOptions<DTO.Department> queryOptions);
        DTO.Department GetBoPhan(Guid guid);
    }
}
