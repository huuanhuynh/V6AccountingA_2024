using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.Tax.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.Tax;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Tax.Dealers
{
    /// <summary>
    ///     Provides TaxItem-related operations (tax CRUD, tax group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectTaxDataDealer : ITaxDataDealer
    {
        private ILogger m_Logger;
        private ITaxDataFarmer m_TaxFarmer;

        public DirectTaxDataDealer(ILogger logger, ITaxDataFarmer taxFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(taxFarmer, "taxFarmer");

            m_Logger = logger;
            m_TaxFarmer = taxFarmer;
        }
        /// <summary>
        ///     See <see cref="ITaxDataDealer.GetTaxs()"/>
        /// </summary>
        public PagedSearchResult<TaxListItem> GetTaxs(SearchCriteria criteria)
        {
            PagedSearchResult<TaxListItem> allTaxs = m_TaxFarmer.GetTaxs(criteria).ToTaxViewModel();

            allTaxs.Data = allTaxs.Data
                .Select(item =>
                {
                    item.Ten_thue = VnCodec.TCVNtoUNICODE(item.Ten_thue);
                    item.Ten_thue2 = VnCodec.TCVNtoUNICODE(item.Ten_thue2);
                    return item;
                })
                .ToList();
            return allTaxs;
        }
        /// <summary>
        ///     See <see cref="ITaxDataDealer.AddTax()"/>
        /// </summary>
        public bool AddTax(AccModels.Tax tax)
        {
            tax.CreatedDate = DateTime.Now;
            tax.ModifiedDate = DateTime.Now;
            tax.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            tax.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_TaxFarmer.Add(tax);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteTax(string key)
        {
            return m_TaxFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateTax(AccModels.Tax tax)
        {
            tax.CreatedDate = DateTime.Now;
            tax.ModifiedDate = DateTime.Now;
            tax.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            tax.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_TaxFarmer.Edit(tax);
        }
    }
}
