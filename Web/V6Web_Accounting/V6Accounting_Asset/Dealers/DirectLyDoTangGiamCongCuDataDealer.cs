using System;
using System.Linq;
using V6Soft.Accounting.Asset.Dealers;
using V6Soft.Accounting.Asset.Extensions;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.EquipmentChangedReason;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.LyDoTangGiamCongCu.Dealers
{
    /// <summary>
    ///     Provides LyDoTangGiamCongCuItem-related operations (equipmentChangedReason CRUD, equipmentChangedReason group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectLyDoTangGiamCongCuDataDealer : ILyDoTangGiamCongCuDataDealer
    {
        private ILogger m_Logger;
        private ILyDoTangGiamCongCuDataFarmer m_LyDoTangGiamCongCuFarmer;

        public DirectLyDoTangGiamCongCuDataDealer(ILogger logger, ILyDoTangGiamCongCuDataFarmer equipmentChangedReasonFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(equipmentChangedReasonFarmer, "equipmentChangedReasonFarmer");

            m_Logger = logger;
            m_LyDoTangGiamCongCuFarmer = equipmentChangedReasonFarmer;
        }
        /// <summary>
        ///     See <see cref="ILyDoTangGiamCongCuDataDealer.GetLyDoTangGiamCongCus()"/>
        /// </summary>
        public PagedSearchResult<EquipmentChangedReasonListItem> GetLyDoTangGiamCongCus(SearchCriteria criteria)
        {
            PagedSearchResult<EquipmentChangedReasonListItem> allLyDoTangGiamCongCus = m_LyDoTangGiamCongCuFarmer.GetLyDoTangGiamCongCus(criteria).ToLyDoTangGiamCongCuViewModel();

            allLyDoTangGiamCongCus.Data = allLyDoTangGiamCongCus.Data
                .Select(item =>
                {
                    item.Ma_TGCungCap = VnCodec.TCVNtoUNICODE(item.Ma_TGCungCap);
                    return item;
                })
                .ToList();
            return allLyDoTangGiamCongCus;
        }
        /// <summary>
        ///     See <see cref="ILyDoTangGiamCongCuDataDealer.AddLyDoTangGiamCongCu()"/>
        /// </summary>
        public bool AddLyDoTangGiamCongCu(AccModels.LyDoTangGiamCongCu equipmentChangedReason)
        {
            equipmentChangedReason.CreatedDate = DateTime.Now;
            equipmentChangedReason.ModifiedDate = DateTime.Now;
            equipmentChangedReason.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            equipmentChangedReason.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_LyDoTangGiamCongCuFarmer.Add(equipmentChangedReason);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteLyDoTangGiamCongCu(string key)
        {
            return m_LyDoTangGiamCongCuFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateLyDoTangGiamCongCu(AccModels.LyDoTangGiamCongCu equipmentChangedReason)
        {
            equipmentChangedReason.CreatedDate = DateTime.Now;
            equipmentChangedReason.ModifiedDate = DateTime.Now;
            equipmentChangedReason.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            equipmentChangedReason.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_LyDoTangGiamCongCuFarmer.Edit(equipmentChangedReason);
        }
    }
}
