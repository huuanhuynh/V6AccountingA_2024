using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Banking.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomTieuKhoan;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IPhanNhomTieuKhoanDataFarmer"/>
    /// </summary>
    public class MssqlPhanNhomTieuKhoanDataFarmer : EnFwDataFarmerBase<ALnhtk, Models.Accounting.DTO.PhanNhomTieuKhoan>, IPhanNhomTieuKhoanDataFarmer
    {
        private readonly IMappingRelatedToPhanNhomTieuKhoan mappingRelatedToUser;
        public MssqlPhanNhomTieuKhoanDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToPhanNhomTieuKhoan mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.PhanNhomTieuKhoan> GetPhanNhomTieuKhoans(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaNhom")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.PhanNhomTieuKhoan GetPhanNhomTieuKhoanById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.PhanNhomTieuKhoan ToAppModel(ALnhtk dbModel)
        {
            return mappingRelatedToUser.MapToPhanNhomTieuKhoan<Models.Accounting.DTO.PhanNhomTieuKhoan>(dbModel);
        }

        protected override ALnhtk ToEntityModel(Models.Accounting.DTO.PhanNhomTieuKhoan appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALnhtk>(appModel);
            return dbModel;
        }
    }
}
