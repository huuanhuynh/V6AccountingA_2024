using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.PaymentMethod;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.PaymentMethod.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IPaymentMethodDataFarmer"/>
    /// </summary>
    public class MssqlPaymentMethodDataFarmer : EnFwDataFarmerBase<Alhttt, Models.Accounting.DTO.PaymentMethod>, IPaymentMethodDataFarmer
    {
        private readonly IMappingRelatedToPaymentMethod mappingRelatedToUser;
        public MssqlPaymentMethodDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToPaymentMethod mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.PaymentMethod> GetPaymentMethods(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaHinhThucThanhToan")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.PaymentMethod GetPaymentMethodById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.PaymentMethod ToAppModel(Alhttt dbModel)
        {
            return mappingRelatedToUser.MapToPaymentMethod<Models.Accounting.DTO.PaymentMethod>(dbModel);
        }

        protected override Alhttt ToEntityModel(Models.Accounting.DTO.PaymentMethod appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<Alhttt>(appModel);
            return dbModel;
        }
    }
}
