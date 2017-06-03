using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.MeasurementConversion.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementConversion;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IMeasurementConversionDataFarmer"/>
    /// </summary>
    public class MssqlMeasurementConversionDataFarmer : EnFwDataFarmerBase<ALqddvt, Models.Accounting.DTO.MeasurementConversion>, IMeasurementConversionDataFarmer
    {
        private readonly IMappingRelatedToMeasurementConversion mappingRelatedToUser;
        public MssqlMeasurementConversionDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToMeasurementConversion mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.MeasurementConversion> GetMeasurementConversions(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaVatTu")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.MeasurementConversion GetMeasurementConversionById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.MeasurementConversion ToAppModel(ALqddvt dbModel)
        {
            return mappingRelatedToUser.MapToMeasurementConversion<Models.Accounting.DTO.MeasurementConversion>(dbModel);
        }

        protected override ALqddvt ToEntityModel(Models.Accounting.DTO.MeasurementConversion appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALqddvt>(appModel);
            return dbModel;
        }
    }
}
