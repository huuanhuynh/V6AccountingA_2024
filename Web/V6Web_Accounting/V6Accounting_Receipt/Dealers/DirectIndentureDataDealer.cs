using System;
using System.Linq;
using V6Soft.Accounting.Indenture.Farmers;
using V6Soft.Accounting.Receipt.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Indenture;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Indenture.Dealers
{
    /// <summary>
    ///     Provides IndentureItem-related operations (indenture CRUD, indenture group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectIndentureDataDealer : IIndentureDataDealer
    {
        private ILogger m_Logger;
        private IIndentureDataFarmer m_IndentureFarmer;

        public DirectIndentureDataDealer(ILogger logger, IIndentureDataFarmer indentureFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(indentureFarmer, "indentureFarmer");

            m_Logger = logger;
            m_IndentureFarmer = indentureFarmer;
        }
        /// <summary>
        ///     See <see cref="IIndentureDataDealer.GetIndentures()"/>
        /// </summary>
        public PagedSearchResult<IndentureListItem> GetIndentures(SearchCriteria criteria)
        {
            PagedSearchResult<IndentureListItem> allIndentures = m_IndentureFarmer.GetIndentures(criteria).ToIndentureViewModel();

            allIndentures.Data = allIndentures.Data
                .Select(item =>
                {
                    item.TenKheUoc = VnCodec.TCVNtoUNICODE(item.TenKheUoc);
                    item.TenKheUoc2 = VnCodec.TCVNtoUNICODE(item.TenKheUoc2);
                    return item;
                })
                .ToList();
            return allIndentures;
        }
        /// <summary>
        ///     See <see cref="IIndentureDataDealer.AddIndenture()"/>
        /// </summary>
        public bool AddIndenture(AccModels.Indenture indenture)
        {
            indenture.CreatedDate = DateTime.Now;
            indenture.ModifiedDate = DateTime.Now;
            indenture.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            indenture.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_IndentureFarmer.Add(indenture);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteIndenture(string key)
        {
            return m_IndentureFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateIndenture(AccModels.Indenture indenture)
        {
            indenture.CreatedDate = DateTime.Now;
            indenture.ModifiedDate = DateTime.Now;
            indenture.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            indenture.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_IndentureFarmer.Edit(indenture);
        }
    }
}
