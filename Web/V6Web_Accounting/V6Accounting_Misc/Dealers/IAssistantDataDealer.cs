using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using V6Soft.Models.Core;
using V6Soft.Models.Core.Filters;
using V6Soft.Models.Core.Metadata;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Misc.Dealers
{
    /// <summary>
    ///     Acts as a service client to get assistant data from AssistantService.
    /// </summary>
    public interface IAssistantDataDealer
    {
        #region Province
        
        /// <summary>
        ///     Inserts a new province.
        ///     <para/>UID field of <param name="addedProvince" />
        ///     is assigned value only if adding successfully.
        /// </summary>
        Task<AddResult> AddProvince(Province addedProvince);

        /// <summary>
        ///     Modifies a province.
        /// </summary>
        /// <param name="modifiedProvince">Must specify UID field.</param>
        Task<OperationResult> ModifyProvince(Province modifiedProvince);

        /// <summary>
        ///     Removes a province permanently.
        /// </summary>
        Task<OperationResult> RemoveProvince(Guid uid);

        /// <summary>
        ///     Moves a province to trash so that it can be restored later.
        /// </summary>
        Task<OperationResult> TrashProvince(Guid uid);
        
        /// <summary>
        ///     Gets list of province satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        Task<PagedList<Province>> GetProvinces(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize);

        #endregion


        #region District
      
        /// <summary>
        ///     Inserts a new district.
        ///     <para/>UID field of <param name="addedDistrict" />
        ///     is assigned value only if adding successfully.
        /// </summary>
        Task<AddResult> AddDistrict(District addedDistrict);

        /// <summary>
        ///     Modifies a district.
        /// </summary>
        /// <param name="modifiedDistrict">Must specify UID field.</param>
        Task<OperationResult> ModifyDistrict(District modifiedDistrict);

        /// <summary>
        ///     Removes a district permanently.
        /// </summary>
        Task<OperationResult> RemoveDistrict(Guid uid);

        /// <summary>
        ///     Moves a district to trash so that it can be restored later.
        /// </summary>
        Task<OperationResult> TrashDistrict(Guid uid);
       
        /// <summary>
        ///     Gets list of district satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        Task<PagedList<District>> GetDistricts(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize);
        
        #endregion


        #region Ward
        /// <summary>
        ///     Inserts a new ward.
        ///     <para/>UID field of <param name="addedWard" />
        ///     is assigned value only if adding successfully.
        /// </summary>
        Task<AddResult> AddWard(Ward addedWard);

        /// <summary>
        ///     Modifies a ward.
        /// </summary>
        /// <param name="modifiedWard">Must specify UID field.</param>
        Task<OperationResult> ModifyWard(Ward modifiedWard);

        /// <summary>
        ///     Removes a ward permanently.
        /// </summary>
        Task<OperationResult> RemoveWard(Guid uid);

        /// <summary>
        ///     Moves a ward to trash so that it can be restored later.
        /// </summary>
        Task<OperationResult> TrashWard(Guid uid);
        
        /// <summary>
        ///     Gets list of ward satisfying given conditions.
        ///     <para />Returns null if there is no result.
        /// </summary>
        Task<PagedList<Ward>> GetWards(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize);

        #endregion
    }
}
