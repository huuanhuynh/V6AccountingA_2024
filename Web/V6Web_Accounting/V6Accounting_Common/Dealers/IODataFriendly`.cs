using System.Collections;
using System.Collections.Generic;
using System.Linq;

using V6Soft.Common.Utils;


namespace V6Soft.Accounting.Common.Dealers
{
    public interface IODataFriendly<TV6Model> : IODataFriendly
    {
        /// <summary>
        ///     Gets an object that allows OData clients to make queries.
        /// </summary>
        IQueryable<TV6Model> AsQueryable();
    }
}
