using System;
using System.Linq;
using V6Soft.Accounting.InvoiceTemplate.Farmers;
using V6Soft.Accounting.Receipt.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.InvoiceTemplate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.InvoiceTemplate.Dealers
{
    /// <summary>
    ///     Provides InvoiceTemplateItem-related operations (invoiceTemplate CRUD, invoiceTemplate group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectInvoiceTemplateDataDealer : IInvoiceTemplateDataDealer
    {
        private ILogger m_Logger;
        private IInvoiceTemplateDataFarmer m_InvoiceTemplateFarmer;

        public DirectInvoiceTemplateDataDealer(ILogger logger, IInvoiceTemplateDataFarmer invoiceTemplateFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(invoiceTemplateFarmer, "invoiceTemplateFarmer");

            m_Logger = logger;
            m_InvoiceTemplateFarmer = invoiceTemplateFarmer;
        }
        /// <summary>
        ///     See <see cref="IInvoiceTemplateDataDealer.GetInvoiceTemplates()"/>
        /// </summary>
        public PagedSearchResult<InvoiceTemplateListItem> GetInvoiceTemplates(SearchCriteria criteria)
        {
            PagedSearchResult<InvoiceTemplateListItem> allInvoiceTemplates = m_InvoiceTemplateFarmer.GetInvoiceTemplates(criteria).ToInvoiceTemplateViewModel();

            allInvoiceTemplates.Data = allInvoiceTemplates.Data
                .Select(item =>
                {
                    item.TenMauHoaDon = VnCodec.TCVNtoUNICODE(item.TenMauHoaDon);
                    item.TenMauHoaDon2 = VnCodec.TCVNtoUNICODE(item.TenMauHoaDon2);
                    return item;
                })
                .ToList();
            return allInvoiceTemplates;
        }
        /// <summary>
        ///     See <see cref="IInvoiceTemplateDataDealer.AddInvoiceTemplate()"/>
        /// </summary>
        public bool AddInvoiceTemplate(AccModels.InvoiceTemplate invoiceTemplate)
        {
            invoiceTemplate.CreatedDate = DateTime.Now;
            invoiceTemplate.ModifiedDate = DateTime.Now;
            invoiceTemplate.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            invoiceTemplate.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_InvoiceTemplateFarmer.Add(invoiceTemplate);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteInvoiceTemplate(string key)
        {
            return m_InvoiceTemplateFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateInvoiceTemplate(AccModels.InvoiceTemplate invoiceTemplate)
        {
            invoiceTemplate.CreatedDate = DateTime.Now;
            invoiceTemplate.ModifiedDate = DateTime.Now;
            invoiceTemplate.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            invoiceTemplate.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_InvoiceTemplateFarmer.Edit(invoiceTemplate);
        }
    }
}
