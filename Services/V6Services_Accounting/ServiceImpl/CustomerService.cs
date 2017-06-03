using System;
using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Common.Logging;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.Utils;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Common.Constants;
using V6Soft.Services.Wcf.Common;
using V6Soft.Services.Wcf.Common.Attributes;
using V6Soft.Services.Wcf.Common.TranslatorExtensions;
using V6Soft.Services.Wcf.Common.ServiceContracts;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Services.Accounting.ServiceImpl
{
    /// <summary>
    ///     Implements <see cref="ICustomerService"/>
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ServiceLogging] // TODO: Should exclude FaultException
    public class CustomerService : V6RuntimeModelServiceBase, ICustomerService
    {
        private ICustomerDataBaker m_CustomerBaker;

        public CustomerService(ICustomerDataBaker customerBaker, ILogger logger)
            : base(logger)
        {
            Guard.ArgumentNotNull(customerBaker, "customerBaker");

            m_CustomerBaker = customerBaker;
        }

        /// <summary>
        ///     See <see cref="ICustomerService.AddCustomerGroup"/>
        /// </summary>
        public AddModelResponse AddCustomerGroup(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel customerGroup =
                ParseRuntimeModel(request.DynamicModel, ModelIndex.CustomerGroup);

            try
            {
                // TODO: Should support adding multiple models.
                m_CustomerBaker.AddCustomerGroup(customerGroup);
            }
            catch(ConstraintViolationException cve)
            {
                ThrowConstraintException(cve);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }

            return new AddModelResponse()
            {
                    NewUID = (Guid)customerGroup.GetField((byte) FieldIndex.ID)
            };
        }

        /// <summary>
        ///     See <see cref="ICustomerService.ModifyCustomerGroup"/>
        /// </summary>
        public ModifyModelResponse ModifyCustomerGroup(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel customerGroup =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.CustomerGroup);

            try
            {
                // TODO: Should support updating multiple models.
                m_CustomerBaker.ModifyCustomerGroup(customerGroup);
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
        ///     See <see cref="ICustomerService.RemoveCustomerGroup"/>
        /// </summary>
        public RemoveModelResponse RemoveCustomerGroup(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                result = m_CustomerBaker.RemoveCustomerGroup(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse() { Success = result };
        }

        /// <summary>
        ///     See <see cref="ICustomerService.GetCustomerGroups"/>
        /// </summary>
        public GetModelsResponse GetCustomerGroups(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> customerGroups;

            customerGroups = m_CustomerBaker.GetCustomerGroups(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = customerGroups.ToDataContracts(),
                Total = total
            };
            return response;
        }





        public AddModelResponse AddCustomer(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel customer =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.Customer);

            try
            {
                // TODO: Should support adding multiple models.
                m_CustomerBaker.AddCustomer(customer);
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
                NewUID = (Guid)customer.GetField((byte)FieldIndex.ID)
            };
        }

        public AddModelResponse AddModelItem(AddModelRequest request)
        {
            AssertRequest(request);

            DynamicModel modelItem =
                ParseRuntimeModel(request.RuntimeModel, (ModelIndex)request.ModelIndex);

            try
            {
                // TODO: Should support adding multiple models.
                m_CustomerBaker.AddModelItem(request.ModelIndex, modelItem);
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
                NewUID = (Guid)modelItem.GetField((byte)FieldIndex.ID)
            };
        }

        public ModifyModelResponse ModifyCustomer(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel customer =
                ParseRuntimeModel(request.RuntimeModel, ModelIndex.Customer);

            try
            {
                // TODO: Should support updating multiple models.
                m_CustomerBaker.ModifyCustomer(customer);
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

        public ModifyModelResponse ModifyModelItem(ModifyModelRequest request)
        {
            AssertRequest(request);

            DynamicModel modelItem =
                ParseRuntimeModel(request.RuntimeModel, (ModelIndex)request.ModelIndex);

            try
            {
                // TODO: Should support updating multiple models.
                m_CustomerBaker.ModifyModelItem(request.ModelIndex, modelItem);
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

        public RemoveModelResponse RemoveCustomer(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                m_CustomerBaker.RemoveCustomer(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse() { Success = result };
        }

        public RemoveModelResponse RemoveModelItem(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                m_CustomerBaker.RemoveModelItem(request.ModelIndex, request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse() { Success = result };
        }

        public GetModelsResponse GetCustomers(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> customers;

            customers = m_CustomerBaker.GetCustomers(
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = customers.ToDataContracts(),
                Total = total
            };
            return response;
        }

        public GetModelsResponse GetModelItems(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total;
            IList<DynamicModel> modelItems;

            modelItems = m_CustomerBaker.GetModelItems(request.ModelIndex,
                request.OutputFields,
                request.Criteria, request.PageIndex, request.PageSize, out total);

            var response = new GetModelsResponse()
            {
                DynamicModels = modelItems.ToDataContracts(),
                Total = total
            };
            return response;
        }


        public RemoveModelResponse TrashCustomerGroup(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                result = m_CustomerBaker.TrashCustomerGroup(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse()
            {
                Success = result
            };
        }

        public RemoveModelResponse TrashCustomer(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                result = m_CustomerBaker.TrashCustomer(request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse()
            {
                Success = result
            };
        }

        public RemoveModelResponse TrashModelItem(RemoveModelRequest request)
        {
            AssertRequest(request);
            AssertContract(request.UID);
            bool result = false;
            try
            {
                // TODO: Should support removing multiple models.
                result = m_CustomerBaker.TrashModelItem(request.ModelIndex, request.UID);
            }
            catch (Exception ex)
            {
                ThrowFaultException(ErrorCode.Unknown, ex);
            }
            return new RemoveModelResponse()
            {
                Success = result
            };
        }


        public GetModelsResponse GetCustomerGroup_ByUID(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customerGroups = new List<DynamicModel>();
            DynamicModel customerGroup = null;

            customerGroup = m_CustomerBaker
                .GetCustomerGroup(request.UID, request.OutputFields);
            if (customerGroup != null)
            {
                customerGroups.Add(customerGroup);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customerGroups.ToDataContracts(),
                Total = total
            };
            return response;
        }

        public GetModelsResponse GetCustomer_ByUID(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customers = new List<DynamicModel>();
            DynamicModel customer = null;

            customer = m_CustomerBaker
                .GetCustomer(request.UID, request.OutputFields);
            if (customer != null)
            {
                customers.Add(customer);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customers.ToDataContracts(),
                Total = total
            };
            return response;
        }

        public GetModelsResponse GetModelItem_ByUID(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customers = new List<DynamicModel>();
            DynamicModel customer = null;

            customer = m_CustomerBaker
                .GetModelItem(request.ModelIndex, request.UID, request.OutputFields);
            if (customer != null)
            {
                customers.Add(customer);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customers.ToDataContracts(),
                Total = total
            };
            return response;
        }


        public GetModelsResponse GetCustomerGroup_ByCode(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customerGroups = new List<DynamicModel>();
            DynamicModel customerGroup = null;

            customerGroup = m_CustomerBaker
                .GetCustomerGroup(request.Code, request.OutputFields);
            if (customerGroup != null)
            {
                customerGroups.Add(customerGroup);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customerGroups.ToDataContracts(),
                Total = total
            };
            return response;
        }

        public GetModelsResponse GetCustomer_ByCode(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customers = new List<DynamicModel>();
            DynamicModel customer = null;

            customer = m_CustomerBaker
                .GetCustomer(request.Code, request.OutputFields);
            if (customer != null)
            {
                customers.Add(customer);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customers.ToDataContracts(),
                Total = total
            };
            return response;
        }

        public GetModelsResponse GetModelItem_ByCode(GetModelsRequest request)
        {
            if (request == null) { return null; }

            ulong total = 0;
            IList<DynamicModel> customers = new List<DynamicModel>();
            DynamicModel customer = null;

            customer = m_CustomerBaker
                .GetModelItem(request.ModelIndex, request.Code, request.OutputFields);
            if (customer != null)
            {
                customers.Add(customer);
                total = 1;
            }

            var response = new GetModelsResponse()
            {
                DynamicModels = customers.ToDataContracts(),
                Total = total
            };
            return response;
        }
    }
}
