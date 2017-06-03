using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.IndentureGroup.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.IndentureGroup;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IIndentureGroupDataFarmer"/>
    /// </summary>
    public class MssqlIndentureGroupDataFarmer : EnFwDataFarmerBase<Alnhku, Models.Accounting.DTO.IndentureGroup>, IIndentureGroupDataFarmer
    {
        private readonly IMappingRelatedToIndentureGroup mappingRelatedToUser;
        public MssqlIndentureGroupDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToIndentureGroup mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.IndentureGroup> GetIndentureGroups(SearchCriteria criteria)
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

        public Models.Accounting.DTO.IndentureGroup GetIndentureGroupById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.IndentureGroup ToAppModel(Alnhku dbModel)
        {
            return mappingRelatedToUser.MapToIndentureGroup<Models.Accounting.DTO.IndentureGroup>(dbModel);
        }

        protected override Alnhku ToEntityModel(Models.Accounting.DTO.IndentureGroup appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alnhku>(appModel);
            return dbModel;
        }
    }
}
