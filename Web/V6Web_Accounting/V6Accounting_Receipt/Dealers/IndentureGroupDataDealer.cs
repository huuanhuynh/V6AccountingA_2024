using System;
using System.Linq;
using V6Soft.Accounting.IndentureGroup.Farmers;
using V6Soft.Accounting.Receipt.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.IndentureGroup;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.IndentureGroup.Dealers
{
    /// <summary>
    ///     Provides IndentureGroupItem-related operations (indentureGroup CRUD, indentureGroup group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class IndentureGroupDataDealer : IIndentureGroupDataDealer
    {
        private ILogger m_Logger;
        private IIndentureGroupDataFarmer m_IndentureGroupFarmer;

        public IndentureGroupDataDealer(ILogger logger, IIndentureGroupDataFarmer indentureGroupFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(indentureGroupFarmer, "indentureGroupFarmer");

            m_Logger = logger;
            m_IndentureGroupFarmer = indentureGroupFarmer;
        }
        /// <summary>
        ///     See <see cref="IIndentureGroupDataDealer.GetIndentureGroups()"/>
        /// </summary>
        public PagedSearchResult<IndentureGroupListItem> GetIndentureGroups(SearchCriteria criteria)
        {
            PagedSearchResult<IndentureGroupListItem> allIndentureGroups = m_IndentureGroupFarmer.GetIndentureGroups(criteria).ToIndentureGroupViewModel();

            allIndentureGroups.Data = allIndentureGroups.Data
                .Select(item =>
                {
                    item.TenNhom = VnCodec.TCVNtoUNICODE(item.TenNhom);
                    item.TenNhom2 = VnCodec.TCVNtoUNICODE(item.TenNhom2);
                    return item;
                })
                .ToList();
            return allIndentureGroups;
        }
        /// <summary>
        ///     See <see cref="IIndentureGroupDataDealer.AddIndentureGroup()"/>
        /// </summary>
        public bool AddIndentureGroup(AccModels.IndentureGroup indentureGroup)
        {
            indentureGroup.CreatedDate = DateTime.Now;
            indentureGroup.ModifiedDate = DateTime.Now;
            indentureGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            indentureGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_IndentureGroupFarmer.Add(indentureGroup);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteIndentureGroup(string key)
        {
            return m_IndentureGroupFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateIndentureGroup(AccModels.IndentureGroup indentureGroup)
        {
            indentureGroup.CreatedDate = DateTime.Now;
            indentureGroup.ModifiedDate = DateTime.Now;
            indentureGroup.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            indentureGroup.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_IndentureGroupFarmer.Edit(indentureGroup);
        }
    }
}
