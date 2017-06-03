using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Models.Core;
using V6Soft.Services.Wcf.Common.Attributes;
using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Provides methods to operate menu data.
    /// </summary>
    [ServiceContract]
    public interface IMenuService
    {
        /// <summary>
        ///     Gets menu tree including menu items and
        ///     their chilren.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        [ReferencePreservingDataContractFormat]
        IList<MenuItem> GetMenuTree(byte level);

        /// <summary>
        ///     Gets children of the menu item with specified OID.
        ///     <para/>Returns null if there is no item.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        [ReferencePreservingDataContractFormat]
        IList<MenuItem> GetChildren(int oid);
    }
}
