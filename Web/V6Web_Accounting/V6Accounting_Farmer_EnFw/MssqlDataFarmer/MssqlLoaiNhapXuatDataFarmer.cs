using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.LoaiNhapXuat.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.LoaiNhapXuat;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ILoaiNhapXuatDataFarmer"/>
    /// </summary>
    public class MssqlLoaiNhapXuatDataFarmer : EnFwDataFarmerBase<Allnx, Models.Accounting.DTO.LoaiNhapXuat>, ILoaiNhapXuatDataFarmer
    {
        private readonly IMappingRelatedToLoaiNhapXuat mappingRelatedToUser;
        public MssqlLoaiNhapXuatDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToLoaiNhapXuat mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.LoaiNhapXuat> GetLoaiNhapXuats(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaLoaiNhapXuat")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.LoaiNhapXuat GetLoaiNhapXuatById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.LoaiNhapXuat ToAppModel(Allnx dbModel)
        {
            return mappingRelatedToUser.MapToLoaiNhapXuat<Models.Accounting.DTO.LoaiNhapXuat>(dbModel);
        }

        protected override Allnx ToEntityModel(Models.Accounting.DTO.LoaiNhapXuat appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Allnx>(appModel);
            return dbModel;
        }
    }
}
