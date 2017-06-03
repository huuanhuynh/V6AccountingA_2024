using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Misc.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Province;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IProvinceDataFarmer"/>
    /// </summary>
    public class MssqlProvinceDataFarmer : EnFwDataFarmerBase<Altinh, Models.Accounting.DTO.Province>, IProvinceDataFarmer
    {
        private readonly IMappingRelatedToProvince mappingRelatedToUser;
        public MssqlProvinceDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToProvince mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Province> GetProvinces(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaTinh")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Province GetProvinceById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Province ToAppModel(Altinh dbModel)
        {
            return mappingRelatedToUser.MapToProvince<Models.Accounting.DTO.Province>(dbModel);
        }

        protected override Altinh ToEntityModel(Models.Accounting.DTO.Province appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Altinh>(appModel);
            return dbModel;
        }
    }
}
