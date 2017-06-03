using System.Collections.Generic;
using System.Linq;
using System;

using V6Soft.Common.ModelFactory;
using V6Soft.Services.Wcf.Common;
using V6Soft.Services.Assistant.Interfaces;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Services.Assistant.DataFarmers
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Assistant.Interfaces.IAssistantDataFarmer"/>
    /// </summary>
    public class AssistantDataFarmer : RuntimeDataFarmerBase, IAssistantDataFarmer
    {
        /// <summary>
        ///     Initializes a new instance of the V6Soft.Services.Assistant.DataFamers.AssistantDataFarmer
        /// </summary>
        /// <param name="connectionString">
        ///     Values that contains many parameters to connect the database
        /// </param>
        public AssistantDataFarmer(string connectionString)
            : base(connectionString)
        {
        }

        #region Province

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddProvince"/>
        /// </summary>
        public bool AddProvince(DynamicModel addedProvince)
        {
            bool result = base.Add((ushort)ModelIndex.Province, addedProvince);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyProvince"/>
        /// </summary>
        public bool ModifyProvince(DynamicModel modifiedProvince)
        {
            bool result = base.Modify((ushort)ModelIndex.Province, modifiedProvince);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveProvince"/>
        /// </summary>
        public bool RemoveProvince(Guid uid)
        {
            bool result = base.Remove((ushort)ModelIndex.Province, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashProvince"/>
        /// </summary>
        public bool TrashProvince(Guid uid)
        {
            bool result = base.Trash((ushort)ModelIndex.Province, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.SearchProvinces"/>
        /// </summary>
        public IList<DynamicModel> SearchProvinces(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            IList<DynamicModel> results = base.Search((byte)ModelIndex.Province,
                outputFields.Select(f => f).ToList(),
                criteria, pageIndex, pageSize, out total);
            return results;
        }

        #endregion


        #region District

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddDistrict"/>
        /// </summary>
        public bool AddDistrict(DynamicModel addedDistrict)
        {
            bool result = base.Add((ushort)ModelIndex.District, addedDistrict);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyDistrict"/>
        /// </summary>
        public bool ModifyDistrict(DynamicModel modifiedDistrict)
        {
             bool result = base.Modify((ushort)ModelIndex.District, modifiedDistrict);
             return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveDistrict"/>
        /// </summary>
        public bool RemoveDistrict(Guid uid)
        {
            bool result = base.Remove((ushort)ModelIndex.District, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashDistrict"/>
        /// </summary>
        public bool TrashDistrict(Guid uid)
        {
            bool result = base.Trash((ushort)ModelIndex.District, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.SearchDistricts"/>
        /// </summary>
        public IList<DynamicModel> SearchDistricts(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            IList<DynamicModel> results = base.Search((byte)ModelIndex.District,
                outputFields.Select(f => f).ToList(),
                criteria, pageIndex, pageSize, out total);
            return results;
        }

        #endregion


        #region Ward

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddWard"/>
        /// </summary>
        public bool AddWard(DynamicModel addedWard)
        {
            bool result = base.Add((ushort)ModelIndex.Ward, addedWard);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyWard"/>
        /// </summary>
        public bool ModifyWard(DynamicModel modifiedWard)
        {
            bool result = base.Modify((ushort)ModelIndex.Ward, modifiedWard);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveWard"/>
        /// </summary>
        public bool RemoveWard(Guid uid)
        {
            bool result = base.Remove((ushort)ModelIndex.Ward, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashWard"/>
        /// </summary>
        public bool TrashWard(Guid uid)
        {
            bool result = base.Trash((ushort)ModelIndex.Ward, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.SearchWards"/>
        /// </summary>
        public IList<DynamicModel> SearchWards(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            IList<DynamicModel> results = base.Search((byte)ModelIndex.Ward,
               outputFields.Select(f => f).ToList(),
               criteria, pageIndex, pageSize, out total);
            return results;
        }

        #endregion
    }
}
