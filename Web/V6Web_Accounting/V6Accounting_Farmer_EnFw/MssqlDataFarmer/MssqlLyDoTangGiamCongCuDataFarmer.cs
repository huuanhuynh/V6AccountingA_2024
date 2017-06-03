using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Asset.Dealers;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentChangedReason;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ILyDoTangGiamCongCuDataFarmer"/>
    /// </summary>
    public class MssqlLyDoTangGiamCongCuDataFarmer : EnFwDataFarmerBase<ALtgcc, Models.Accounting.DTO.LyDoTangGiamCongCu>, ILyDoTangGiamCongCuDataFarmer
    {
        private IMappingRelatedToLyDoTangGiamCongCu _mappingRelatedToLyDoTangGiamCongCu;
        public MssqlLyDoTangGiamCongCuDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToLyDoTangGiamCongCu _mappingRelatedToLyDoTangGiamCongCu)
            : base(dbContext)
        {
            this._mappingRelatedToLyDoTangGiamCongCu = _mappingRelatedToLyDoTangGiamCongCu;
        }

        public PagedSearchResult<Models.Accounting.DTO.LyDoTangGiamCongCu> GetLyDoTangGiamCongCus(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("Ma_TGCungCap")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.LyDoTangGiamCongCu GetLyDoTangGiamCongCuById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }



        protected override Models.Accounting.DTO.LyDoTangGiamCongCu ToAppModel(ALtgcc dbModel)
        {
            return _mappingRelatedToLyDoTangGiamCongCu.MapToLyDoTangGiamCongCu<Models.Accounting.DTO.LyDoTangGiamCongCu>(dbModel);
        }

        protected override ALtgcc ToEntityModel(Models.Accounting.DTO.LyDoTangGiamCongCu appModel)
        {
            var dbModel = _mappingRelatedToLyDoTangGiamCongCu.MapToAlkh<ALtgcc>(appModel);
            return dbModel;
        }
    }
}
