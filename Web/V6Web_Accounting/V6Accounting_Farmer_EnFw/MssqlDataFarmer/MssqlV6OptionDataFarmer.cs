using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using V6Soft.Accounting.System.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.V6Option;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using DTO = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IV6OptionDataFarmer"/>
    /// </summary>
    public class MssqlV6OptionDataFarmer : EnFwDataFarmerBase<V6option, DTO.V6Option>, IV6OptionDataFarmer
    {
        private readonly IMappingRelatedToV6Option mappingRelatedToUser;
        public MssqlV6OptionDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToV6Option mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<DTO.V6Option> GetV6Options(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("STT")
                };
            }
            return FindByCriteria(criteria);
        }

        public DTO.V6Option GetV6OptionById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        public IQueryable<DTO.V6Option> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.V6Option>();
        }

        protected override DTO.V6Option ToAppModel(V6option dbModel)
        {
            return mappingRelatedToUser.MapToV6Option<DTO.V6Option>(dbModel);
        }

        protected override V6option ToEntityModel(DTO.V6Option model)
        {
            var dbModel = mappingRelatedToUser.MapTov6option<V6option>(model);
            return dbModel;
        }
    }
}
