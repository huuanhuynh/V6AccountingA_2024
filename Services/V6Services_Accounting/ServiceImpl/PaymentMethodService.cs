using System;
using System.Collections.Generic;
using System.ServiceModel;
using V6Soft.Common.Logging;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.Utils;
using V6Soft.Services.Accounting.Extensions;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Common.Constants;
using V6Soft.Services.Wcf.Common;
using V6Soft.Services.Wcf.Common.Attributes;
using V6Soft.Services.Wcf.Common.ServiceContracts;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;
using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;

namespace V6Soft.Services.Accounting.ServiceImpl
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ServiceLogging] // TODO: Should exclude FaultException
    public class PaymentMethodService : V6RuntimeModelServiceBase, IPaymentMethodService
    {
        private readonly IPaymentMethodDataBaker m_PaymentMethodBaker;

        public PaymentMethodService(IPaymentMethodDataBaker paymentMethodBaker, ILogger logger)
            : base(logger)
        {
            Guard.ArgumentNotNull(paymentMethodBaker, "paymentMethodBaker");
            m_PaymentMethodBaker = paymentMethodBaker;
        }
        public AddModelResponse AddPaymentMethod(AddModelRequest request)
        {
            AssertRequest(request);
            DynamicModel paymentMethod = ParseRuntimeModel(request.DynamicModel, 
                ModelIndex.PaymentMethod);
            try
            {
                // TODO: Should support adding multiple models.
                m_PaymentMethodBaker.AddPaymentMethod(paymentMethod);
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
                NewUID = (Guid)paymentMethod.GetField((byte)FieldIndex.ID)
            };
        }

        public ModifyModelResponse ModifyPaymentMethod(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel paymentMethod =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.PaymentMethod);

            try
            {
                // TODO: Should support updating multiple models.
                m_PaymentMethodBaker.ModifyPaymentMethod(paymentMethod);
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

        public RemoveModelResponse RemovePaymentMethod(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);

            try
            {
                // TODO: Should support removing multiple models.
                m_PaymentMethodBaker.RemovePaymentMethod(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse();
        }

        public GetModelsResponse GetPaymentMethods(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;

            IList<DynamicModel> paymentMethods = m_PaymentMethodBaker.SearchPaymentMethod(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = paymentMethods.ToDataContracts(),
                Total = total
            };
            return response;
        }
    }
}
