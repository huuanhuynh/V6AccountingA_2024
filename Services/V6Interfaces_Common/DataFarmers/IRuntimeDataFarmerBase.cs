using System;
using System.Collections.Generic;


namespace V6Soft.Interfaces.Accounting.Common.DataFarmers
{
    public interface IRuntimeDataFarmerBase
    {
        /// <summary>
        ///     Inserts a model to database.
        ///     <para/>UID field of <param name="addedModel" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool Add(ushort modelIndex, DynamicModel addedModel);

        /// <summary>
        ///     Modifies a model.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="changedModel">Must specify UID field.</param>
        bool Modify(ushort modelIndex, DynamicModel changedModel);
        
        /// <summary>
        ///     Removes a model permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool Remove(ushort modelIndex, Guid uid);

        /// <summary>
        ///     Looks for data items that meets specified <paramref name="criteria"/>.
        ///     <para/>Returns null if nothing is found.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> Search(ushort modelIndex, IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);

        /// <summary>
        ///     Moves a model to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool Trash(ushort modelIndex, Guid uid);
    }
}
