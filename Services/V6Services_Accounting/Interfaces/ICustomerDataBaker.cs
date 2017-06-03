using System;
using System.Collections.Generic;
using V6Soft.Common.ModelFactory;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Collects and processes customer data from farmer(s) 
    ///     before returning results to customer service.
    /// </summary>
    public interface ICustomerDataBaker
    {
        /// <summary>
        ///     Inserts a new customer group to database.
        ///     <para/>UID field of <param name="addedGroup" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool AddCustomerGroup(DynamicModel addedGroup);
        bool AddCustomer(DynamicModel addedGroup);
        bool AddModelItem(ushort modelIndex, DynamicModel addedGroup);

        /// <summary>
        ///     Modifies a customer group.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="modifiedGroup">Must specify UID field.</param>
        bool ModifyCustomerGroup(DynamicModel modifiedGroup);
        bool ModifyCustomer(DynamicModel modifiedGroup);
        bool ModifyModelItem(ushort modelIndex, DynamicModel modifiedGroup);

        /// <summary>
        ///     Removes a customer group permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool RemoveCustomerGroup(Guid uid);
        bool RemoveCustomer(Guid uid);
        bool RemoveModelItem(ushort modelIndex, Guid uid);

        /// <summary>
        ///     Gets a customer group by its UID.
        ///     <para />Returns null if there is no results.
        /// </summary>
        DynamicModel GetCustomerGroup(Guid uid, IList<string> outputFields);
        DynamicModel GetCustomer(Guid uid, IList<string> outputFields);
        DynamicModel GetModelItem(ushort modelIndex, Guid uid, IList<string> outputFields);

        /// <summary>
        ///     Gets a customer group by its Code.
        ///     <para />Returns null if there is no results.
        /// </summary>
        DynamicModel GetCustomerGroup(string code, IList<string> outputFields);
        DynamicModel GetCustomer(string code, IList<string> outputFields);
        DynamicModel GetModelItem(ushort modelIndex, string code, IList<string> outputFields);

        /// <summary>
        ///     Gets list of customer groups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> GetCustomerGroups(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);
        IList<DynamicModel> GetCustomers(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);
        IList<DynamicModel> GetModelItems(ushort modelIndex, IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);

        /// <summary>
        ///     Moves a customer group to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool TrashCustomerGroup(Guid uid);
        bool TrashCustomer(Guid uid);
        bool TrashModelItem(ushort modelIndex, Guid uid);
    }
}
