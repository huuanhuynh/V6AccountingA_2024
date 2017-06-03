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
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ICustomerDataFarmer"/>
    /// </summary>
    public class CustomerDataFarmer : RuntimeDataFarmerBase, ICustomerDataFarmer
    {
        public CustomerDataFarmer(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.AddCustomerGroup"/>
        /// </summary>
        public bool AddCustomerGroup(DynamicModel addedGroup)
        {
            bool result = Add((ushort)ModelIndex.CustomerGroup, addedGroup);
            return result;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.ModifyCustomerGroup"/>
        /// </summary>
        public bool ModifyCustomerGroup(DynamicModel modifiedGroup)
        {
            bool result = Modify((ushort)ModelIndex.CustomerGroup, modifiedGroup);
            return result;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.RemoveCustomerGroup"/>
        /// </summary>
        public bool RemoveCustomerGroup(Guid uid)
        {
            bool result = Remove((ushort)ModelIndex.CustomerGroup, uid);
            return result;
        }

        public IList<DynamicModel> SearchCustomerGroups(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search((byte)ModelIndex.CustomerGroup,
                outputFields.Select(f => (string)f).ToList(),
                criteria, pageIndex, pageSize, out total);
            return results;
        }
        
        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.TrashCustomerGroup"/>
        /// </summary>
        public bool TrashCustomerGroup(Guid uid)
        {
            bool result = Trash((ushort)ModelIndex.CustomerGroup, uid);
            return result;
        }

        public bool AddCustomer(DynamicModel addedGroup)
        {
            bool result = Add((ushort)ModelIndex.Customer, addedGroup);
            return result;
        }

        public bool AddModelItem(ushort modelIndex, DynamicModel addedGroup)
        {
            bool result = Add(modelIndex, addedGroup);
            return result;
        }

        public bool ModifyCustomer(DynamicModel modifiedGroup)
        {
            bool result = Modify((ushort)ModelIndex.Customer, modifiedGroup);
            return result;
        }

        public bool ModifyModelItem(ushort modelIndex, DynamicModel modifiedGroup)
        {
            bool result = Modify(modelIndex, modifiedGroup);
            return result;
        }

        public bool RemoveCustomer(Guid uid)
        {
            bool result = Remove((ushort)ModelIndex.Customer, uid);
            return result;
        }

        public bool RemoveModelItem(ushort modelIndex, Guid uid)
        {
            bool result = Remove(modelIndex, uid);
            return result;
        }

        public IList<DynamicModel> SearchCustomers(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search((byte)ModelIndex.Customer,
                outputFields.Select(f => (string)f).ToList(),
                criteria, pageIndex, pageSize, out total);
            return results;
        }

        public IList<DynamicModel> SearchModelItems(ushort modelIndex, IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search(modelIndex,
                outputFields.Select(f => (string)f).ToList(),
                criteria, pageIndex, pageSize, out total);
            return results;
        }

        public bool TrashCustomer(Guid uid)
        {
            bool result = Trash((ushort)ModelIndex.Customer, uid);
            return result;
        }

        public bool TrashModelItem(ushort modelIndex, Guid uid)
        {
            bool result = Trash(modelIndex, uid);
            return result;
        }
    }
}
