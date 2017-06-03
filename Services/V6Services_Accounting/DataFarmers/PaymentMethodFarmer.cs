using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Common.ModelFactory;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Wcf.Common;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;

namespace V6Soft.Services.Accounting.DataFarmers
{
    public class PaymentMethodFarmer : RuntimeDataFarmerBase, IPaymentMethodDataFarmer
    {
        public PaymentMethodFarmer(string connectionString)
            : base(connectionString)
        {
        }

        public bool AddPaymentMethod(DynamicModel addedPaymentMethod)
        {
            bool result = Add((ushort)ModelIndex.PaymentMethod, addedPaymentMethod);
            return result;
        }

        public bool ModifyPaymentMethod(DynamicModel modifiedPaymentMethod)
        {
            bool result = Modify((ushort) ModelIndex.PaymentMethod, modifiedPaymentMethod);
            return result;
        }

        public bool RemovePaymentMethod(Guid uid)
        {
            bool result = Remove((ushort)ModelIndex.PaymentMethod ,uid);
            return result;
        }

        public IList<DynamicModel> SearchPaymentMethod(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            //IList<DynamicModel> results = base.Search((byte)ModelIndex.PaymentMethod,
            //    outputFields.Select(f => (byte)f).ToList(),
            //    criteria, pageIndex, pageSize, out total);
            //return results;
            throw new NotImplementedException();
        }

        public bool TrashPaymentMethod(Guid uid)
        {
            bool result = Trash((ushort)ModelIndex.PaymentMethod, uid);
            return result;
        }
    }
}
