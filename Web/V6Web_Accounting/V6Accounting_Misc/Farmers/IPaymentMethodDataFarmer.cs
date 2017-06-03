using System;
using System.Collections.Generic;

using V6Soft.Models.Core;
using V6Soft.Models.Core.Filters;


namespace V6Soft.Accounting.Misc.Farmers
{
    public interface IPaymentMethodDataFarmer
    {
        bool AddPaymentMethod(PaymentMethod addedPaymentMethod);
        bool ModifyPaymentMethod(PaymentMethod modifiedPaymentMethod);
        bool RemovePaymentMethod(Guid uid);

        IList<PaymentMethod> SearchPaymentMethod(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total);

        bool TrashPaymentMethod(Guid uid);
    }
}
