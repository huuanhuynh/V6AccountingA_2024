using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Common.ModelFactory;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;

namespace V6Soft.Services.Accounting.Interfaces
{
    public interface IPaymentMethodDataFarmer
    {
        bool AddPaymentMethod(DynamicModel addedPaymentMethod);
        bool ModifyPaymentMethod(DynamicModel modifiedPaymentMethod);
        bool RemovePaymentMethod(Guid uid);

        IList<DynamicModel> SearchPaymentMethod(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total);

        bool TrashPaymentMethod(Guid uid);
    }
}
