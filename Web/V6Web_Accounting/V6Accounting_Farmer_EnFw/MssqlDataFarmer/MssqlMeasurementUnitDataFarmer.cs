using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.MeasurementUnit.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementUnit;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMeasurementUnitDataFarmer"/>
    /// </summary>
    public class MssqlMeasurementUnitDataFarmer : EnFwDataFarmerBase<Aldvt, Models.Accounting.DTO.MeasurementUnit>, IMeasurementUnitDataFarmer
    {
        private readonly IMappingRelatedToMeasurementUnit mappingRelatedToUser;
        public MssqlMeasurementUnitDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMeasurementUnit mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.MeasurementUnit> GetMeasurementUnits(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("DonViTinh")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.MeasurementUnit GetMeasurementUnitById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.MeasurementUnit ToAppModel(Aldvt dbModel)
        {
            return mappingRelatedToUser.MapToMeasurementUnit<Models.Accounting.DTO.MeasurementUnit>(dbModel);
        }

        protected override Aldvt ToEntityModel(Models.Accounting.DTO.MeasurementUnit appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Aldvt>(appModel);
            return dbModel;
        }
    }
}
