using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Tax.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.Tax;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ITaxDataFarmer"/>
    /// </summary>
    public class MssqlTaxDataFarmer : EnFwDataFarmerBase<Althue, Models.Accounting.DTO.Tax>, ITaxDataFarmer
    {
        private readonly IMappingRelatedToTax mappingRelatedToUser;
        public MssqlTaxDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToTax mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.Tax> GetTaxs(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaThue")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.Tax GetTaxById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.Tax ToAppModel(Althue dbModel)
        {
            return mappingRelatedToUser.MapToTax<Models.Accounting.DTO.Tax>(dbModel);
        }

        protected override Althue ToEntityModel(Models.Accounting.DTO.Tax appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Althue>(appModel);
            return dbModel;
        }
    }
}
