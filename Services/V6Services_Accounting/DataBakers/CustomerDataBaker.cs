using System;
using System.Linq;
using System.Collections.Generic;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.Utils;
using V6Soft.Models.Core.Constants;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Models.Core.RuntimeModelExtensions;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Accounting.DataBakers
{
    /// <summary>
    ///     Implements <see cref="ICustomerDataBaker"/>
    /// </summary>
    public class CustomerDataBaker : ICustomerDataBaker
    {
        private ICustomerDataFarmer m_CustomerFarmer;


        public CustomerDataBaker(ICustomerDataFarmer customerFarmer)
        {
            Guard.ArgumentNotNull(customerFarmer, "customerFarmer");

            m_CustomerFarmer = customerFarmer;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.AddCustomerGroup"/>
        /// </summary>
        public bool AddCustomerGroup(DynamicModel addedGroup)
        {
            Guard.ArgumentNotNull(addedGroup, "addedModel");
            ValidateModel(addedGroup);
            return m_CustomerFarmer.AddCustomerGroup(addedGroup);
        }
        
        /// <summary>
        //      See <see cref="ICustomerDataBaker.ModifyCustomerGroup"/>
        /// </summary>
        public bool ModifyCustomerGroup(DynamicModel modifiedGroup)
        {
            Guard.ArgumentNotNull(modifiedGroup, "modifiedGroup");
            ValidateModel(modifiedGroup, true);
            return m_CustomerFarmer.ModifyCustomerGroup(modifiedGroup);
        }

        /// <summary>
        //      See <see cref="ICustomerDataBaker.RemoveCustomerGroup"/>
        /// </summary>
        public bool RemoveCustomerGroup(Guid uid)
        {
            return m_CustomerFarmer.RemoveCustomerGroup(uid);
        }

        /// <summary>
        //      See <see cref="ICustomerDataBaker.GetCustomerGroup"/>
        /// </summary>
        public DynamicModel GetCustomerGroup(Guid uid, IList<string> outputFields)
        {
            return GetCustomerGroupBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        /// <summary>
        //      See <see cref="ICustomerDataBaker.GetCustomerGroup"/>
        /// </summary>
        public DynamicModel GetCustomerGroup(string code, IList<string> outputFields)
        {
            return GetCustomerGroupBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.GetCustomerGroups"/>
        /// </summary>
        public IList<DynamicModel> GetCustomerGroups(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total)
        {
            return m_CustomerFarmer.SearchCustomerGroups(outputFields, criteria, pageIndex, pageSize, out total);
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.TrashCustomerGroup"/>
        /// </summary>
        public bool TrashCustomerGroup(Guid uid)
        {
            return m_CustomerFarmer.TrashCustomerGroup(uid);
        }


        #region Private methods

        private DynamicModel GetCustomerGroupBySingleField(//Nó có nhiều kq rồi làm sao?
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
                m_CustomerFarmer.SearchCustomerGroups(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }

        
        private DynamicModel GetCustomerBySingleField(
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
                m_CustomerFarmer.SearchCustomers(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }

       

        private DynamicModel GetModelItemBySingleField(ushort modelIndex,
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
                m_CustomerFarmer.SearchModelItems(modelIndex, outputFields, searchByOneField, 1, 1, out total);

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
                object uid = model.GetField((byte)FieldIndex.ID);
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


        public bool AddCustomer(DynamicModel addedGroup)
        {
            Guard.ArgumentNotNull(addedGroup, "addedModel");
            ValidateModel(addedGroup);
            return m_CustomerFarmer.AddCustomer(addedGroup);
        }

        public bool AddModelItem(ushort modelIndex, DynamicModel addedGroup)
        {
            Guard.ArgumentNotNull(addedGroup, "addedModel");
            ValidateModel(addedGroup);
            return m_CustomerFarmer.AddModelItem(modelIndex, addedGroup);
        }

        public bool ModifyCustomer(DynamicModel modifiedGroup)
        {
            Guard.ArgumentNotNull(modifiedGroup, "modifiedGroup");
            ValidateModel(modifiedGroup, true);
            return m_CustomerFarmer.ModifyCustomer(modifiedGroup);
        }

        public bool ModifyModelItem(ushort modelIndex, DynamicModel modifiedGroup)
        {
            Guard.ArgumentNotNull(modifiedGroup, "modifiedGroup");
            ValidateModel(modifiedGroup, true);
            return m_CustomerFarmer.ModifyModelItem(modelIndex, modifiedGroup);
        }

        public bool RemoveCustomer(Guid uid)
        {
            return m_CustomerFarmer.RemoveCustomer(uid);
        }

        public bool RemoveModelItem(ushort modelIndex, Guid uid)
        {
            return m_CustomerFarmer.RemoveModelItem(modelIndex, uid);
        }

        public DynamicModel GetCustomer(Guid uid, IList<string> outputFields)
        {
            return GetCustomerBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        public DynamicModel GetModelItem(ushort modelIndex, Guid uid, IList<string> outputFields)
        {
            return GetModelItemBySingleField(modelIndex, outputFields, DefinitionName.Fields.UID, uid);
        }
        
        public DynamicModel GetCustomer(string code, IList<string> outputFields)
        {
            return GetCustomerBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        public DynamicModel GetModelItem(ushort modelIndex, string code, IList<string> outputFields)
        {
            return GetModelItemBySingleField(modelIndex, outputFields, DefinitionName.Fields.Code, code);
        }

        public IList<DynamicModel> GetCustomers(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            return m_CustomerFarmer.SearchCustomers(outputFields, criteria, pageIndex, pageSize, out total);
        }

        public IList<DynamicModel> GetModelItems(ushort modelIndex, IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            return m_CustomerFarmer.SearchModelItems(modelIndex, outputFields, criteria, pageIndex, pageSize, out total);
        }

        public bool TrashCustomer(Guid uid)
        {
            return m_CustomerFarmer.TrashCustomer(uid);
        }

        public bool TrashModelItem(ushort modelIndex, Guid uid)
        {
            return m_CustomerFarmer.TrashModelItem(modelIndex, uid);
        }
    }
}
