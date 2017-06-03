using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Accounting.Common.Farmers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Receipt.Farmers
{
    public interface IReceiptDataFarmer : IDataFarmerBase<DTO.Receipt>
    {
        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.Receipt> AsQueryable();
    }
}
