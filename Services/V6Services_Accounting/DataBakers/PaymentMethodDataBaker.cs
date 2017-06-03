using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.Utils;
using V6Soft.Services.Accounting.Interfaces;

namespace V6Soft.Services.Accounting.DataBakers
{
    public class PaymentMethodDataBaker : IPaymentMethodDataBaker
    {
        private readonly IPaymentMethodDataFarmer m_PaymentMethodFarmer;

        public PaymentMethodDataBaker(IPaymentMethodDataFarmer paymentMethodFarmer)
        {
            Guard.ArgumentNotNull(paymentMethodFarmer, "paymentMethodFarmer");
            m_PaymentMethodFarmer = paymentMethodFarmer;
        }

        public bool AddPaymentMethod(DynamicModel addedPaymentMethod)
        {
            Guard.ArgumentNotNull(addedPaymentMethod, "addedModel");
            ValidateModel(addedPaymentMethod);
            bool result = m_PaymentMethodFarmer.AddPaymentMethod(addedPaymentMethod);
            return result;
        }

        public bool ModifyPaymentMethod(DynamicModel modifiedPaymentMethod)
        {
            Guard.ArgumentNotNull(modifiedPaymentMethod, "modifiedModel");
            ValidateModel(modifiedPaymentMethod, true);
            return m_PaymentMethodFarmer.ModifyPaymentMethod(modifiedPaymentMethod);
        }

        public bool RemovePaymentMethod(Guid uid)
        {
            return m_PaymentMethodFarmer.RemovePaymentMethod(uid);
        }

        public DynamicModel GetPaymentMethod(Guid uid, IList<string> outputFields)
        {
            return GetPaymentMethodBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        public DynamicModel GetPaymentMethod(string code, IList<string> outputFields)
        {
            return GetPaymentMethodBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        public IList<DynamicModel> SearchPaymentMethod(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            return m_PaymentMethodFarmer.SearchPaymentMethod(outputFields, 
                criteria, pageIndex, pageSize, out total);
        }

        #region Private methods

        private DynamicModel GetPaymentMethodBySingleField(
            IList<string> outputFields, string field, object conditionValue)
        {
            var searchByOneField = new List<SearchCriterion>()
            {
                new SearchCriterion()
                {
                        FieldName = field,
                        CompareOperator = CompareOperator.Equal,
                        ConditionValue = conditionValue
                }
            };
            ulong total;

            IList<DynamicModel> results =
                m_PaymentMethodFarmer.SearchPaymentMethod(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }

        private static void ValidateModel(DynamicModel model, bool mustHaveUid = false)
        {
            // Must specify UID
            if (mustHaveUid)
            {
                //object uid = model.GetField((byte)FieldIndex.ID);
                object uid = model.Field(DefinitionName.Fields.UID);
                Guard.ArgumentNotNull(uid, "UID");
            }

            // Must satisfy all constraints
            bool isValidated = DynamicModelFactory.DynamicModelValidator.Validate(model);
            if (!isValidated)
            {
                throw new ConstraintViolationException();
            }
        }

        #endregion
    }
}
