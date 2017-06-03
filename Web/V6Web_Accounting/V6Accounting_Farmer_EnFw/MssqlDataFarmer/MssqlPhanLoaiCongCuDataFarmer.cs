using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentType;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IPhanLoaiCongCuDataFarmer"/>
    /// </summary>
    public class MssqlPhanLoaiCongCuDataFarmer : EnFwDataFarmerBase<ALplcc, PhanLoaiCongCu>, IPhanLoaiCongCuDataFarmer
    {
        private IMappingRelatedToEquipmentType _mappingRelatedToEquipmentType;
        public MssqlPhanLoaiCongCuDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToEquipmentType _mappingRelatedToEquipmentType)
            : base(dbContext)
        {
            this._mappingRelatedToEquipmentType = _mappingRelatedToEquipmentType;
        }

        public PagedSearchResult<Models.Accounting.DTO.PhanLoaiCongCu> GetPhanLoaiCongCus(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaLoai")
                };
            }
            return FindByCriteria(criteria);
        }

        public PhanLoaiCongCu GetPhanLoaiCongCuById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }



        protected override PhanLoaiCongCu ToAppModel(ALplcc dbModel)
        {
            return _mappingRelatedToEquipmentType.MapToEquipmentType<PhanLoaiCongCu>(dbModel);
        }

        protected override ALplcc ToEntityModel(PhanLoaiCongCu appModel)
        {
            var dbModel = _mappingRelatedToEquipmentType.MapToAlkh<ALplcc>(appModel);
            return dbModel;
        }
    }
}
