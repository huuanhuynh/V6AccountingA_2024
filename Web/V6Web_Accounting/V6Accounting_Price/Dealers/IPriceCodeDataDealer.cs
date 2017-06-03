using System;
using System.Linq;
using System.Web.Http.OData.Query;
using V6Soft.Accounting.Common.Dealers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.PriceCode.Dealers
{
    /// <summary>
    ///     Acts as a service client to get PriceCode data from PriceCodeService.
    /// </summary>
    public interface IPriceCodeDataDealer : IODataFriendly<DTO.PriceCode>
    {
        IQueryable<DTO.PriceCode> AsQueryable(ODataQueryOptions<DTO.PriceCode> queryOptions);
        DTO.PriceCode GetPriceCode(Guid guid);
    }
}
