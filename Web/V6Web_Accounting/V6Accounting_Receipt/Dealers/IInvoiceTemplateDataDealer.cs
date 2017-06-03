using V6Soft.Models.Accounting.ViewModels.InvoiceTemplate;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.InvoiceTemplate.Dealers
{
    /// <summary>
    ///     Acts as a service client to get invoiceTemplate data from InvoiceTemplateService.
    /// </summary>
    public interface IInvoiceTemplateDataDealer
    {
        /// <summary>
        ///     Gets list of invoiceTemplates satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<InvoiceTemplateListItem> GetInvoiceTemplates(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new invoiceTemplate.
        /// </summary>
        bool AddInvoiceTemplate(AccModels.InvoiceTemplate invoiceTemplate);
        /// <summary>
        ///     Delete a invoiceTemplate.
        /// </summary>
        bool DeleteInvoiceTemplate(string key);
        /// <summary>
        ///     Update data for a invoiceTemplate.
        /// </summary>
        bool UpdateInvoiceTemplate(AccModels.InvoiceTemplate invoiceTemplate);
    }
}
