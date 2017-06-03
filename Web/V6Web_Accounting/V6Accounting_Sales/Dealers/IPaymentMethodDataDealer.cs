using V6Soft.Models.Accounting.ViewModels.PaymentMethod;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.PaymentMethod.Dealers
{
    /// <summary>
    ///     Acts as a service client to get paymentMethod data from PaymentMethodService.
    /// </summary>
    public interface IPaymentMethodDataDealer
    {
        /// <summary>
        ///     Gets list of paymentMethods satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<PaymentMethodListItem> GetPaymentMethods(SearchCriteria criteria);
        /// <summary>
        ///     Persists data for a new paymentMethod.
        /// </summary>
        bool AddPaymentMethod(AccModels.PaymentMethod paymentMethod);
        /// <summary>
        ///     Delete a paymentMethod.
        /// </summary>
        bool DeletePaymentMethod(string key);
        /// <summary>
        ///     Update data for a paymentMethod.
        /// </summary>
        bool UpdatePaymentMethod(AccModels.PaymentMethod paymentMethod);
    }
}
