using System.Linq;
using System.Collections.Generic;
using System;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.Utils;
using V6Soft.Services.Assistant.Interfaces;

namespace V6Soft.Services.Assistant.DataBakers
{
    /// <summary>
    ///     Implements <see cref="IAssistantDataBaker"/>
    /// </summary>
    public class AssistantDataBaker : IAssistantDataBaker
    {
        private IAssistantDataFarmer m_AssistantFarmer;

        /// <summary>
        ///     Initializes a new instance of the V6Soft.Services.Assistant.DataBakers.AssistantDataBaker
        /// </summary>
        /// <param name="assistantFarmer">
        ///     The V6Soft.Services.Assistant.DataFamer.IAssistantDataFarmer that executes CRUD method
        /// </param>
        public AssistantDataBaker(IAssistantDataFarmer assistantFarmer)
        {
            Guard.ArgumentNotNull(assistantFarmer, "assistantFarmer");

            m_AssistantFarmer = assistantFarmer;
        }
        

        #region Province

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddProvince"/>
        /// </summary>
        public bool AddProvince(DynamicModel addedProvince)
        {
            Guard.ArgumentNotNull(addedProvince, "addedModel");
            ValidateModel(addedProvince);
            return m_AssistantFarmer.AddProvince(addedProvince);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyProvince"/>
        /// </summary>
        public bool ModifyProvince(DynamicModel modifiedProvince)
        {
            Guard.ArgumentNotNull(modifiedProvince, "modifiedProvince");
            ValidateModel(modifiedProvince, true);
            return m_AssistantFarmer.ModifyProvince(modifiedProvince);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveProvince"/>
        /// </summary>
        public bool RemoveProvince(Guid uid)
        {
            return m_AssistantFarmer.RemoveProvince(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashProvince"/>
        /// </summary>
        public bool TrashProvince(Guid uid)
        {
            return m_AssistantFarmer.TrashProvince(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetProvince"/>
        /// </summary>
        public DynamicModel GetProvince(Guid uid, IList<string> outputFields)
        {
            return GetProvinceBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetProvince"/>
        /// </summary>
        public DynamicModel GetProvince(string code, IList<string> outputFields)
        {
            return GetProvinceBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetProvinces"/>
        /// </summary>
        public IList<DynamicModel> GetProvinces(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            return m_AssistantFarmer.SearchProvinces(outputFields, criteria, pageIndex, pageSize, out total);
        }

        #endregion
        

        #region District

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddDistrict"/>
        /// </summary>
        public bool AddDistrict(DynamicModel addedDistrict)
        {
            Guard.ArgumentNotNull(addedDistrict, "addedDistrict");
            ValidateModel(addedDistrict, true);
            return m_AssistantFarmer.AddDistrict(addedDistrict);
        }


        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyDistrict"/>
        /// </summary>
        public bool ModifyDistrict(DynamicModel modifiedDistrict)
        {
            Guard.ArgumentNotNull(modifiedDistrict, "modifiedDistrict");
            ValidateModel(modifiedDistrict, true);
            return m_AssistantFarmer.ModifyDistrict(modifiedDistrict);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveDistrict"/>
        /// </summary>
        public bool RemoveDistrict(Guid uid)
        {
            return m_AssistantFarmer.RemoveDistrict(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashDistrict"/>
        /// </summary>
        public bool TrashDistrict(Guid uid)
        {
            return m_AssistantFarmer.TrashDistrict(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetDistrict"/>
        /// </summary>
        public DynamicModel GetDistrict(Guid uid, IList<string> outputFields)
        {
            return GetDistrictBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetDistrict"/>
        /// </summary>
        public DynamicModel GetDistrict(string code, IList<string> outputFields)
        {
            return GetDistrictBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetDistricts"/>
        /// </summary>
        public IList<DynamicModel> GetDistricts(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            return m_AssistantFarmer.SearchDistricts(outputFields, criteria, pageIndex, pageSize, out total);
        }

        #endregion


        #region Ward
        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.AddWard"/>
        /// </summary>
        public bool AddWard(DynamicModel addedWard)
        {
            Guard.ArgumentNotNull(addedWard, "addedWard");
            ValidateModel(addedWard, true);
            return m_AssistantFarmer.AddWard(addedWard);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.ModifyWard"/>
        /// </summary>
        public bool ModifyWard(DynamicModel modifiedWard)
        {
            Guard.ArgumentNotNull(modifiedWard, "modifiedWard");
            ValidateModel(modifiedWard, true);
            return m_AssistantFarmer.ModifyWard(modifiedWard);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.RemoveWard"/>
        /// </summary>
        public bool RemoveWard(Guid uid)
        {
            return m_AssistantFarmer.RemoveWard(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.TrashWard"/>
        /// </summary>
        public bool TrashWard(Guid uid)
        {
            return m_AssistantFarmer.TrashWard(uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetWard"/>
        /// </summary>
        public DynamicModel GetWard(Guid uid, IList<string> outputFields)
        {
            return GetWardBySingleField(outputFields, DefinitionName.Fields.UID, uid);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetWard"/>
        /// </summary>
        public DynamicModel GetWard(string code, IList<string> outputFields)
        {
            return GetWardBySingleField(outputFields, DefinitionName.Fields.Code, code);
        }

        /// <summary>
        ///     See <see cref="IAssistantDataFarmer.GetWards"/>
        /// </summary>
        public IList<DynamicModel> GetWards(IList<string> outputFields, 
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, 
            out ulong total)
        {
            return m_AssistantFarmer.SearchWards(outputFields, criteria, pageIndex, pageSize, out total);
        }
        #endregion


        #region Private methods
        /// <summary>
        ///     To get province by conditionValues are passed
        /// </summary>
        private DynamicModel GetProvinceBySingleField(
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
                m_AssistantFarmer.SearchProvinces(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }
       
        /// <summary>
        ///     To get district by conditionValues are passed
        /// </summary>
        private DynamicModel GetDistrictBySingleField(
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
                m_AssistantFarmer.SearchDistricts(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }
        
        /// <summary>
        ///     To get ward by conditionValues are passed
        /// </summary>
        private DynamicModel GetWardBySingleField(
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
                m_AssistantFarmer.SearchWards(outputFields, searchByOneField, 1, 1, out total);

            if (results != null && results.Count == 1)
            {
                return results.First();
            }
            return null;
        }
        
        /// <summary>
        ///     Using validate model
        /// </summary>
        private static void ValidateModel(DynamicModel model, bool mustHaveUid = false)
        {
            // Must specify UID
            if (mustHaveUid)
            {
                //object uid = model.GetField((byte)FieldIndex.ID);
                object uid = model.GetMember(DefinitionName.Fields.UID);
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
