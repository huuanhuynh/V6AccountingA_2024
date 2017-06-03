using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Asset.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomCongCu;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IPhanNhomCongCuDataFarmer"/>
    /// </summary>
    public class MssqlPhanNhomCongCuDataFarmer : EnFwDataFarmerBase<ALnhCC, Models.Accounting.DTO.PhanNhomCongCu>, IPhanNhomCongCuDataFarmer
    {
        private readonly IMappingRelatedToPhanNhomCongCu mappingRelatedToUser;
        public MssqlPhanNhomCongCuDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToPhanNhomCongCu mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.PhanNhomCongCu> GetPhanNhomCongCus(SearchCriteria criteria)
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

        public Models.Accounting.DTO.PhanNhomCongCu GetPhanNhomCongCuById(Guid guid)
        {
            var phanNhomCongCuDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(phanNhomCongCuDb);
        }

        protected override Models.Accounting.DTO.PhanNhomCongCu ToAppModel(ALnhCC dbModel)
        {
            return mappingRelatedToUser.MapToPhanNhomCongCu<Models.Accounting.DTO.PhanNhomCongCu>(dbModel);
        }

        protected override ALnhCC ToEntityModel(Models.Accounting.DTO.PhanNhomCongCu appModel)
        {
            var dbModel = mappingRelatedToUser.MapToALnhCC<ALnhCC>(appModel);
            return dbModel;
        }
    }
}
