using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using V6Soft.Models.Accounting;
using V6Soft.Common.ModelFactory;
using V6Soft.Models.Core;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;

namespace V6Interface_Service
{
    public interface IPaymentMethodDataDealer
    {
        Task<AddResult> AddPaymentMethod(PaymentMethod addedPaymentMethod);

        /// <summary>
        ///     Modifies a payment method.
        /// </summary>
        /// <param name="modifiedPaymentMethod">Must specify UID field.</param>
        Task<OperationResult> ModifyPaymentMethod(PaymentMethod modifiedPaymentMethod);

        /// <summary>
        ///     Removes a payment method permanently.
        /// </summary>
        Task<OperationResult> RemovePaymentMethod(Guid uid);

        /// <summary>
        ///     Gets list of payment methods satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        Task<PagedList<PaymentMethod>> GetPaymentMethods(IList<FieldIndex> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize);

        /// <summary>
        ///     Moves a payment method to trash so that it can be restored later.
        /// </summary>
        Task<OperationResult> TrashPaymentMethod(Guid uid);
    }
}
