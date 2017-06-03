using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using V6Soft.Common.Logging;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.Utils;
using V6Soft.Services.Common.Constants;
using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Services.Wcf.Common
{
    public abstract class V6RuntimeModelServiceBase
    {
        protected ILogger m_Logger;

        public V6RuntimeModelServiceBase(ILogger logger)
        {
            Guard.ArgumentNotNull(logger, "logger");

            m_Logger = logger;
        }

        #region Private and protected methods

        protected void AssertRequest(object request)
        {
            if (request == null)
            {
                ThrowFaultException(ErrorCode.InvalidParameter);
            }
        }

        protected void AssertContract(object contract)
        {
            if (contract == null)
            {
                ThrowFaultException(ErrorCode.InvalidDataContract);
            }
        }

        protected void AssertContracts<T>(IEnumerable<T> contracts)
        {
            if (contracts == null || !contracts.Any())
            {
                ThrowFaultException(ErrorCode.InvalidDataContract);
            }
        }

        protected DynamicModel ParseRuntimeModel(
            RuntimeModelDC dataContract, ModelIndex modelIndex)
        {
            IList<DynamicModel> results = 
                ParseRuntimeModels(new List<RuntimeModelDC>() { dataContract },
                    modelIndex);
            return results.First();
        }

        protected IList<DynamicModel> ParseRuntimeModels(
            IList<RuntimeModelDC> dataContracts, ModelIndex modelIndex)
        {
            IList<DynamicModel> serviceModels = null;
            try
            {
                serviceModels =
                    dataContracts.ToServiceModels((ushort)modelIndex);
                if (serviceModels == null)
                {
                    ThrowFaultException(ErrorCode.InvalidDataContract);
                }
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.InvalidDataContract, ex);
            }
            return serviceModels;
        }

        protected DynamicModel ParseRuntimeModel(
            DynamicModelDC dataContract, ModelIndex modelIndex)
        {
            IList<DynamicModel> results =
                ParseDynamicModels(new List<DynamicModelDC>() { dataContract },
                    modelIndex);
            return results.First();
        }

        protected IList<DynamicModel> ParseDynamicModels(
            IList<DynamicModelDC> dataContracts, ModelIndex modelIndex)
        {
            IList<DynamicModel> serviceModels = null;
            try
            {
                serviceModels =
                    dataContracts.ToServiceModels((ushort)modelIndex);
                if (serviceModels == null)
                {
                    ThrowFaultException(ErrorCode.InvalidDataContract);
                }
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.InvalidDataContract, ex);
            }
            return serviceModels;
        }

        protected void ThrowConstraintException(ConstraintViolationException cause)
        {
            m_Logger.LogException(string.Empty, cause);
            throw new FaultException<ConstraintViolationFault>(
                new ConstraintViolationFault());
        }

        protected void ThrowFaultException(ErrorCode errorCode, Exception cause = null)
        {
            if (cause != null)
            {
                m_Logger.LogException(string.Empty, cause);
            }
            throw new FaultException<OperationFault>(new OperationFault(errorCode));
        }

        #endregion

    }
}
