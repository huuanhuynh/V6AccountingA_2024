using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.InvoiceTemplate.Farmers;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper.InvoiceTemplate;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IInvoiceTemplateDataFarmer"/>
    /// </summary>
    public class MssqlInvoiceTemplateDataFarmer : EnFwDataFarmerBase<ALMAUHD, Models.Accounting.DTO.InvoiceTemplate>, IInvoiceTemplateDataFarmer
    {
        private readonly IMappingRelatedToInvoiceTemplate mappingRelatedToUser;
        public MssqlInvoiceTemplateDataFarmer(IV6AccountingContext dbContext, IMappingRelatedToInvoiceTemplate mappingRelatedToUser)
            : base(dbContext)
        {
            this.mappingRelatedToUser = mappingRelatedToUser;
        }

        public PagedSearchResult<Models.Accounting.DTO.InvoiceTemplate> GetInvoiceTemplates(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaMauHoaDon")
                };
            }
            return FindByCriteria(criteria);
        }

        public Models.Accounting.DTO.InvoiceTemplate GetInvoiceTemplateById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        protected override Models.Accounting.DTO.InvoiceTemplate ToAppModel(ALMAUHD dbModel)
        {
            return mappingRelatedToUser.MapToInvoiceTemplate<Models.Accounting.DTO.InvoiceTemplate>(dbModel);
        }

        protected override ALMAUHD ToEntityModel(Models.Accounting.DTO.InvoiceTemplate appModel)
        {
            var dbModel = mappingRelatedToUser.MapToAlkh<ALMAUHD>(appModel);
            return dbModel;
        }
    }
}
