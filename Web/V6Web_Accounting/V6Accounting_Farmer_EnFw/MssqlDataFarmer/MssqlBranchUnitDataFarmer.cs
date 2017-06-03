using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.BranchUnit.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.BranchUnit;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IBranchUnitDataFarmer"/>
    /// </summary>
    public class MssqlBranchUnitDataFarmer : EnFwDataFarmerBase<Aldvcs, Models.Accounting.DTO.BranchUnit>, IBranchUnitDataFarmer
    {
        private readonly IMappingRelatedToBranchUnit mappingRelatedToUser;
        public MssqlBranchUnitDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToBranchUnit mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.BranchUnit> GetBranchUnits(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaDonVi")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.BranchUnit GetBranchUnitById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.BranchUnit ToAppModel(Aldvcs dbModel)
        {
            return mappingRelatedToUser.MapToBranchUnit<Models.Accounting.DTO.BranchUnit>(dbModel);
        }

        protected override Aldvcs ToEntityModel(Models.Accounting.DTO.BranchUnit appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Aldvcs>(appModel);
            return dbModel;
        }
    }
}
