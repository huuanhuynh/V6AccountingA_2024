using System;
using System.Linq;
using V6Soft.Accounting.Customer.Extensions;
using V6Soft.Accounting.PaymentMethod.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.ViewModels.PaymentMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.PaymentMethod.Dealers
{
    /// <summary>
    ///     Provides PaymentMethodItem-related operations (paymentMethod CRUD, paymentMethod group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectPaymentMethodDataDealer : IPaymentMethodDataDealer
    {
        private ILogger m_Logger;
        private IPaymentMethodDataFarmer m_PaymentMethodFarmer;

        public DirectPaymentMethodDataDealer(ILogger logger, IPaymentMethodDataFarmer paymentMethodFarmer)
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(paymentMethodFarmer, "paymentMethodFarmer");

            m_Logger = logger;
            m_PaymentMethodFarmer = paymentMethodFarmer;
        }
        /// <summary>
        ///     See <see cref="IPaymentMethodDataDealer.GetPaymentMethods()"/>
        /// </summary>
        public PagedSearchResult<PaymentMethodListItem> GetPaymentMethods(SearchCriteria criteria)
        {
            PagedSearchResult<PaymentMethodListItem> allPaymentMethods = m_PaymentMethodFarmer.GetPaymentMethods(criteria).ToPaymentMethodViewModel();

            allPaymentMethods.Data = allPaymentMethods.Data
                .Select(item =>
                {
                    item.TenHinhThucThanhToan = VnCodec.TCVNtoUNICODE(item.TenHinhThucThanhToan);
                    return item;
                })
                .ToList();
            return allPaymentMethods;
        }
        /// <summary>
        ///     See <see cref="IPaymentMethodDataDealer.AddPaymentMethod()"/>
        /// </summary>
        public bool AddPaymentMethod(AccModels.PaymentMethod paymentMethod)
        {
            paymentMethod.CreatedDate = DateTime.Now;
            paymentMethod.ModifiedDate = DateTime.Now;
            paymentMethod.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            paymentMethod.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_PaymentMethodFarmer.Add(paymentMethod);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeletePaymentMethod(string key)
        {
            return m_PaymentMethodFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdatePaymentMethod(AccModels.PaymentMethod paymentMethod)
        {
            paymentMethod.CreatedDate = DateTime.Now;
            paymentMethod.ModifiedDate = DateTime.Now;
            paymentMethod.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            paymentMethod.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_PaymentMethodFarmer.Edit(paymentMethod);
        }
    }
}
