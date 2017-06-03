using System;
using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.Utils;
using V6Soft.Services.Assistant.Interfaces;
using V6Soft.Services.Wcf.Common.Attributes;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;
using V6Soft.Services.Wcf.Common.ServiceContracts;
using V6Soft.Services.Assistant.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Services.Wcf.Common;
using V6Soft.Models.Core.RuntimeModelExtensions;
using V6Soft.Services.Common.Constants;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Services.Assistant.ServiceImpl 
{
    /// <summary>
    ///     Implements <see cref="IAssistantService"/>
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ServiceLogging] // TODO: Should exclude FaultException
    public class AssistantService : V6RuntimeModelServiceBase, IAssistantService
    {
        private IAssistantDataBaker m_AssistantBaker;

        /// <summary>
        ///     Initializes a new instance of the 
        ///         V6Soft.Services.Assistant.ServiceImpl.AssistantService
        /// </summary>
        /// <param name="assistantBaker">
        ///     The V6Soft.Services.Assistant.DataBakers.IAssistantDataBaker 
        ///         that executes CRUD method
        /// </param>
        /// <param name="logger">
        ///     The V6Soft.Common.Logging  that executes for logging
        /// </param>
        public AssistantService(IAssistantDataBaker assistantBaker, ILogger logger)
            : base(logger)
        {
            Guard.ArgumentNotNull(assistantBaker, "assistantBaker");

            m_AssistantBaker = assistantBaker;
        }


        #region Province

        /// <summary>
        ///     See <see cref="IAssistantService.AddProvince"/>
        /// </summary>
        public AddModelResponse AddProvince(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel province =
                ParseRuntimeModel(request.DynamicModel, ModelIndex.Province);
            try
            {
                // TODO: Should support adding multiple models.
                m_AssistantBaker.AddProvince(province);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }

            return new AddModelResponse()
            {
                NewUID = (Guid)province.GetField((byte) FieldIndex.ID)
            };
        }

        /// <summary>
        ///     See <see cref="IAssistantService.ModifyProvince"/>
        /// </summary>
        public ModifyModelResponse ModifyProvince(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel province =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.Province);

            try
            {
                // TODO: Should support updating multiple models.
                m_AssistantBaker.ModifyProvince(province);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new ModifyModelResponse();
        }

        /// <summary>
        ///     See <see cref="IAssistantService.RemoveProvince"/>
        /// </summary>
        public RemoveModelResponse RemoveProvince(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);

            try
            {
                // TODO: Should support removing multiple models.
                m_AssistantBaker.RemoveProvince(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse();
        }

        /// <summary>
        ///     See <see cref="IAssistantrService.GetProvinces"/>
        /// </summary>
        public GetModelsResponse GetProvinces(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> provinces;

            provinces = m_AssistantBaker.GetProvinces(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = provinces.ToDataContracts(),
                Total = total
            };
            return response;
        }

        #endregion


        #region District

        /// <summary>
        ///     See <see cref="IAssistantService.AddDistrict"/>
        /// </summary>
        public AddModelResponse AddDistrict(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel district =
                ParseRuntimeModel(request.DynamicModel, ModelIndex.District);

            try
            {
                // TODO: Should support adding multiple models.
                m_AssistantBaker.AddDistrict(district);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }

            return new AddModelResponse()
            {
                NewUID = (Guid)district.GetField((byte) FieldIndex.ID)
            };
        }

        /// <summary>
        ///     See <see cref="IAssistantService.ModifyDistrict"/>
        /// </summary>
        public ModifyModelResponse ModifyDistrict(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel district =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.District);

            try
            {
                // TODO: Should support updating multiple models.
                m_AssistantBaker.ModifyDistrict(district);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new ModifyModelResponse();
        }


        /// <summary>
        ///     See <see cref="IAssistantService.RemoveDistrict"/>
        /// </summary>
        public RemoveModelResponse RemoveDistrict(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);

            try
            {
                // TODO: Should support removing multiple models.
                m_AssistantBaker.RemoveDistrict(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse();
        }

        /// <summary>
        ///     See <see cref="IAssistantService.GetDistricts"/>
        /// </summary>
        public GetModelsResponse GetDistricts(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> districts;

            districts = m_AssistantBaker.GetDistricts(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = districts.ToDataContracts(),
                Total = total
            };
            return response;
        }

        #endregion


        #region Ward
        
        /// <summary>
        ///     See <see cref="IAssistantService.AddWard"/>
        /// </summary>
        public AddModelResponse AddWard(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel ward =
                ParseRuntimeModel(request.DynamicModel, ModelIndex.District);

            try
            {
                // TODO: Should support updating multiple models.
                m_AssistantBaker.AddWard(ward);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new AddModelResponse();
        }

        /// <summary>
        ///     See <see cref="IAssistantService.ModifyWard"/>
        /// </summary>
        public ModifyModelResponse ModifyWard(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel ward =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.District);

            try
            {
                // TODO: Should support updating multiple models.
                m_AssistantBaker.ModifyWard(ward);
            }
            catch (ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new ModifyModelResponse();
        }


        /// <summary>
        ///     See <see cref="IAssistantService.RemoveWard"/>
        /// </summary>
        public RemoveModelResponse RemoveWard(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);

            try
            {
                // TODO: Should support removing multiple models.
                m_AssistantBaker.RemoveWard(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse();
        }

        /// <summary>
        ///     See <see cref="IAssistantService.GetWards"/>
        /// </summary>
        public GetModelsResponse GetWards(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> provinces;

            provinces = m_AssistantBaker.GetWards(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = provinces.ToDataContracts(),
                Total = total
            };
            return response;
        }

        #endregion
    }
}
