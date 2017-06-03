using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Department.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.BoPhanSuDungCongCu;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IBoPhanSuDungCongCuDataFarmer"/>
    /// </summary>
    public class MssqlBoPhanSuDungCongCuDataFarmer : EnFwDataFarmerBase<Albpcc, Models.Accounting.DTO.BoPhanSuDungCongCu>, IBoPhanSuDungCongCuDataFarmer
    {
        private IMappingRelatedToBoPhanSuDungCongCu _mappingRelatedToBoPhanSuDungCongCu;
        public MssqlBoPhanSuDungCongCuDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToBoPhanSuDungCongCu _mappingRelatedToBoPhanSuDungCongCu)
            : base(dbContext)
        {
            this._mappingRelatedToBoPhanSuDungCongCu = _mappingRelatedToBoPhanSuDungCongCu;
        }

        public PagedSearchResult<Models.Accounting.DTO.BoPhanSuDungCongCu> GetBoPhanSuDungCongCus(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaBoPhan")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.BoPhanSuDungCongCu GeBoPhanSuDungCongCuById(Guid guid)
        {
            var boPhanSuDungCongCuDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(boPhanSuDungCongCuDb);
        }



        protected override Models.Accounting.DTO.BoPhanSuDungCongCu ToAppModel(Albpcc dbModel)
        {
            return _mappingRelatedToBoPhanSuDungCongCu.MapToBoPhanSuDungCongCu<Models.Accounting.DTO.BoPhanSuDungCongCu>(dbModel);
        }

        protected override Albpcc ToEntityModel(Models.Accounting.DTO.BoPhanSuDungCongCu appModel)
        {
            var dbModel = _mappingRelatedToBoPhanSuDungCongCu.MapToAlbp<Albpcc>(appModel);
            return dbModel;
        }
    }
}
