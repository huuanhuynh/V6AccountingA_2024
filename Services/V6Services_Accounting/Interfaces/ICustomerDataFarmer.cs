using System;
using System.Collections.Generic;
using V6Soft.Common.ModelFactory;

namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Fetches raw data from database.
    /// </summary>
    public interface ICustomerDataFarmer
    {
        bool AddCustomer(DynamicModel addedGroup);
        /// <summary>
        ///     Inserts a new customer group to database.
        ///     <para/>UID field of <param name="addedGroup" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool AddCustomerGroup(DynamicModel addedGroup);
        bool AddModelItem(ushort modelIndex, DynamicModel addedGroup);

        bool ModifyCustomer(DynamicModel modifiedGroup);
        /// <summary>
        ///     Modifies a customer group.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        bool ModifyCustomerGroup(DynamicModel modifiedGroup);
        bool ModifyModelItem(ushort modelIndex, DynamicModel modifiedGroup);

        bool RemoveCustomer(Guid uid);
        /// <summary>
        ///     Removes a customer group permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool RemoveCustomerGroup(Guid uid);
        bool RemoveModelItem(ushort modelIndex, Guid uid);

        IList<DynamicModel> SearchCustomers(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);
        /// <summary>
        ///     Gets list of customer groups satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> SearchCustomerGroups(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);
        IList<DynamicModel> SearchModelItems(ushort modelIndex, IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);

        bool TrashCustomer(Guid uid);
        /// <summary>
        ///     Moves a customer group to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool TrashCustomerGroup(Guid uid);
        bool TrashModelItem(ushort modelIndex, Guid uid);
    }
}
