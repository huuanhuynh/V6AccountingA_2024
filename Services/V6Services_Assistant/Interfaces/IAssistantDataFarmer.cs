using System;
using System.Collections.Generic;

using V6Soft.Common.ModelFactory;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Assistant.Interfaces
{
    /// <summary>
    ///     Fetches raw data from database.
    /// </summary>
    public interface IAssistantDataFarmer
    {
        #region Province

        /// <summary>
        ///     Inserts a new province to database.
        ///     <para/>UID field of <param name="AddProvince" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool AddProvince(DynamicModel addedProvince);
         
        /// <summary>
        ///     Modifies a province.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        bool ModifyProvince(DynamicModel modifiedProvince);

        /// <summary>
        ///     Removes a province permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool RemoveProvince(Guid uid);
        ///     Moves a province group to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool TrashProvince(Guid uid);
        /// <summary>
        ///     Gets list of satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> SearchProvinces(IList<string> outputFields,
           IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
           out ulong total);

        #endregion


        #region District

        /// <summary>
        ///     Inserts a new district to database.
        ///     <para/>UID field of <param name="AddDistrict" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool AddDistrict(DynamicModel addedDistrict); 

        /// <summary>
        ///     Modifies a district.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        bool ModifyDistrict(DynamicModel modifiedDistrict); 

        /// <summary>
        ///     Removes a district permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool RemoveDistrict(Guid uid);
        ///     Moves a district to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool TrashDistrict(Guid uid);
        /// <summary>
        ///     Gets list of districts satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> SearchDistricts(IList<string> outputFields,
           IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
           out ulong total);

        #endregion

        
        #region Ward

        /// <summary>
        ///     Inserts a new ward to database.
        ///     <para/>UID field of <param name="AddWard" />
        ///     is assigned value only if adding successfully.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool AddWard(DynamicModel addedWard);

        /// <summary>
        ///     Modifies a ward.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        bool ModifyWard(DynamicModel modifiedWard);

        /// <summary>
        ///     Removes a ward permanently from database.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool RemoveWard(Guid uid);
       
        ///     Moves a ward to trash so that it can be restored later.
        ///     <para/>Returns True if succeeded, False if failed.
        /// </summary>
        bool TrashWard(Guid uid);
        
        /// <summary>
        ///     Gets list of wards satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        /// <param name="outputFields">Optional</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageIndex">Starts at 1</param>
        /// <param name="pageSize">Can be any value</param>
        IList<DynamicModel> SearchWards(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total);
        
        #endregion
    }
}
