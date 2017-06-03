using System.Collections;
using System.Collections.Generic;
using System.Linq;

using V6Soft.Common.Utils;


namespace V6Soft.Accounting.Common.Dealers
{
    public interface IODataFriendly
    {
        /// <summary>
        ///     Saves these models' changes
        /// </summary>
        /// <param name="models"></param>
        void Save(IList<DynamicObject> models);

        /// <summary>
        ///     Processes the objects' property values.
        ///     <para/> As Odata controller queries DbContext directly without going through business layers.
        ///     This function should be called before Odata results are returned to client.
        /// </summary>
        /// <param name="objSequence"></param>
        void StandardizeMany(IEnumerable objSequence);

        /// <summary>
        ///     Processes this object's property values.
        ///     <para/> As Odata controller queries DbContext directly without going through business layers.
        ///     This function should be called before Odata results are returned to client.
        /// </summary>
        /// <param name="objSequence"></param>
        void Standardize(object obj);
    }
}
