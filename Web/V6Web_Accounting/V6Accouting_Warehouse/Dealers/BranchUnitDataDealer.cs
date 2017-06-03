using System;
using System.Linq;
using V6Soft.Accounting.BranchUnit.Farmers;
using V6Soft.Accounting.Warehouse.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.DonViCoSo;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.BranchUnit.Dealers
{
    /// <summary>
    ///     Provides BranchUnitItem-related operations (branchUnit CRUD, branchUnit group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectBranchUnitDataDealer : IBranchUnitDataDealer
    {
        private ILogger m_Logger;
        private IBranchUnitDataFarmer m_BranchUnitFarmer;

        public DirectBranchUnitDataDealer(ILogger logger, IBranchUnitDataFarmer branchUnitFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(branchUnitFarmer, "branchUnitFarmer");

            m_Logger = logger;
            m_BranchUnitFarmer = branchUnitFarmer;
        }
        /// <summary>
        ///     See <see cref="IBranchUnitDataDealer.GetBranchUnits()"/>
        /// </summary>
        public PagedSearchResult<BranchUnitListItem> GetBranchUnits(SearchCriteria criteria)
        {
            PagedSearchResult<BranchUnitListItem> allBranchUnits = m_BranchUnitFarmer.GetBranchUnits(criteria).ToBranchUnitViewModel();

            allBranchUnits.Data = allBranchUnits.Data
                .Select(item =>
                {
                    item.TenDonVi = VnCodec.TCVNtoUNICODE(item.TenDonVi);
                    item.TenDonVi2 = VnCodec.TCVNtoUNICODE(item.TenDonVi2);
                    return item;
                })
                .ToList();
            return allBranchUnits;
        }
        /// <summary>
        ///     See <see cref="IBranchUnitDataDealer.AddBranchUnit()"/>
        /// </summary>
        public bool AddBranchUnit(AccModels.BranchUnit branchUnit)
        {
            branchUnit.CreatedDate = DateTime.Now;
            branchUnit.ModifiedDate = DateTime.Now;
            branchUnit.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            branchUnit.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_BranchUnitFarmer.Add(branchUnit);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteBranchUnit(string key)
        {
            return m_BranchUnitFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateBranchUnit(AccModels.BranchUnit branchUnit)
        {
            branchUnit.CreatedDate = DateTime.Now;
            branchUnit.ModifiedDate = DateTime.Now;
            branchUnit.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            branchUnit.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_BranchUnitFarmer.Edit(branchUnit);
        }
    }
}
