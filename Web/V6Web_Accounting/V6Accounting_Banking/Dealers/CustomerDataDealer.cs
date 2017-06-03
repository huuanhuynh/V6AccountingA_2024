using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ServiceModel;

using V6Soft.Common.ExceptionHandling;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.Utils.TaskExtensions;
using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.ServiceClients.Accounting.Customer.Constants;
using V6Soft.ServiceClients.Accounting.Customer.CustomerService;
using V6Soft.Services.Common.Exceptions;
using V6Soft.Services.Common.Infrastructure;
using V6Soft.ServiceClients.Accounting.Customer.Extensions;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;


namespace V6Soft.Dealers.Accounting.Customer.DataDealers
{
    /// <summary>
    ///     Implements <see cref="ICustomerDataDealer"/>
    /// </summary>
    public class CustomerDataDealer : ICustomerDataDealer
    {
        private V6ServiceClient<CustomerServiceClient, ICustomerService> m_ServiceClient;

        /// <summary>
        ///     Should only be used by UnitTest to inject mock instance.
        /// </summary>
        private ICustomerService m_CustomerServiceClient;

        private ILogger m_Logger;

        
        private ICustomerService CustomerSvcClient
        {
            get
            {
                if (m_CustomerServiceClient != null) { return m_CustomerServiceClient; }

                ICustomerService client = m_ServiceClient.Client;
                if (client == null)
                {
                    throw new ServiceUnavailableException();
                }
                return client;
            }
        }


        public CustomerDataDealer(ILogger logger)
        {
            Guard.ArgumentNotNull(logger, "logger");

            m_Logger = logger;
            m_ServiceClient =
                new V6ServiceClient<CustomerServiceClient, ICustomerService>(
                    string.Empty,
                    Values.ServiceInformation.CustomerServiceBindingName,
                    Values.ServiceInformation.CustomerServiceAddressFormula
                );
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.AddCustomerGroup"/>
        /// </summary>
        public Task<AddResult> AddCustomerGroup(CustomerGroup addedGroup)
        {
            var source = new TaskCompletionSource<AddResult>();
            var request = new AddModelRequest()
            {
                RuntimeModel = addedGroup.ToServiceModel()
            };

            CustomerSvcClient
                .AddCustomerGroupAsync(request)
                .Then(response =>
                {
                    var result = new AddResult()
                    {
                         UID = response.NewUID,
                         Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.ModifyCustomerGroup"/>
        /// </summary>
        public Task<OperationResult> ModifyCustomerGroup(CustomerGroup modifiedGroup)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new ModifyModelRequest()
            {
                RuntimeModel = modifiedGroup.ToServiceModel()
            };

            CustomerSvcClient.ModifyCustomerGroupAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.RemoveCustomerGroup"/>
        /// </summary>
        public Task<OperationResult> RemoveCustomerGroup(Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                UID = uid
            };

            CustomerSvcClient.RemoveCustomerAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.GetCustomerGroups"/>
        /// </summary>
        public Task<PagedList<CustomerGroup>> GetCustomerGroups(IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            var source = new TaskCompletionSource<PagedList<CustomerGroup>>();
            var request = new GetModelsRequest()
                {
                    OutputFields = outputFields.Select(f => f).ToList(),
                    Criteria = (criteria != null ? criteria.ToList() : null),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

            CustomerSvcClient
                .GetCustomerGroupsAsync(request).Then(response =>
                {
                    var results = new PagedList<CustomerGroup>()
                    {
                        Items = response.DynamicModels.ToAppModels(),
                        Total = response.Total
                    };
                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataBaker.TrashCustomerGroup"/>
        /// </summary>
        public Task<OperationResult> TrashCustomerGroup(Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                UID = uid
            };

            CustomerSvcClient.TrashCustomerGroupAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }


        protected Exception ProcessException(AggregateException aggException)
        {
            Exception innerException = aggException.InnerException;
            string errorLabel;
            if (innerException is FaultException<ConstraintViolationFault>)
            {
                errorLabel = ErrorLabels.FormValuesAreInvalid;
            }
            else if (innerException is FaultException<OperationFault>)
            {
                var operationFault = ((FaultException<OperationFault>)innerException).Detail;
                m_Logger.LogException("Operation failed with error codes: ", operationFault.ErrorCodes);
                errorLabel = ErrorLabels.AnErrorOccurred;
            }
            else // Unpredicted exceptions
            {
                m_Logger.LogException(string.Empty, innerException);
                errorLabel = ErrorLabels.AnErrorOccurred;
            }
            return new V6Exception(errorLabel);
        }



        public Task<AddResult> AddCustomer(V6Soft.Models.Accounting.Customer addedGroup)
        {
            var source = new TaskCompletionSource<AddResult>();
            var request = new AddModelRequest()
            {
                RuntimeModel = addedGroup.ToServiceModel()
            };

            CustomerSvcClient.AddCustomerAsync(request)
                .Then(response =>
                {
                    var result = new AddResult()
                    {
                        UID = response.NewUID,
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<AddResult> Add(ushort modelIndex, DynamicModel addedGroup)
        {
            var source = new TaskCompletionSource<AddResult>();
            var request = new AddModelRequest()
            {
                RuntimeModel = addedGroup.ToServiceModel()
                ,ModelIndex = modelIndex
            };

            CustomerSvcClient.AddModelItemAsync(request)
                .Then(response =>
                {
                    var result = new AddResult()
                    {
                        UID = response.NewUID,
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<OperationResult> ModifyCustomer(V6Soft.Models.Accounting.Customer modifiedGroup)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new ModifyModelRequest()
            {
                RuntimeModel = modifiedGroup.ToServiceModel()
            };

            CustomerSvcClient.ModifyCustomerAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<OperationResult> Modify(ushort modelIndex, DynamicModel modifiedGroup)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new ModifyModelRequest()
            {
                RuntimeModel = modifiedGroup.ToServiceModel()
            };

            CustomerSvcClient.ModifyModelItemAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<OperationResult> RemoveCustomer(Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                UID = uid
            };

            CustomerSvcClient.RemoveCustomerAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<OperationResult> Remove(ushort modelIndex, Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                ModelIndex = modelIndex,
                UID = uid
            };

            CustomerSvcClient.RemoveModelItemAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }
        

        public Task<PagedList<V6Soft.Models.Accounting.Customer>> GetCustomers(IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            V6Soft.Models.Accounting.Customer customer = new Models.Accounting.Customer();
            List<string> listField = customer.Fields.Select(f => (string)f).ToList();
            return GetCustomers(listField, criteria, pageIndex, pageSize);
        }
        public Task<PagedList<V6Soft.Models.Accounting.Customer>> GetCustomers(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            var source = new TaskCompletionSource<PagedList<V6Soft.Models.Accounting.Customer>>();
            var request = new GetModelsRequest()
                {
                    OutputFields = outputFields.Select(f => f).ToList(),
                    Criteria = (criteria != null ? criteria.ToList() : null),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

            CustomerSvcClient
                .GetCustomerGroupsAsync(request).Then(response =>
                {
                    var results = new PagedList<V6Soft.Models.Accounting.Customer>()
                    {
                        Items = response.DynamicModels.ToCustomers(),
                        Total = response.Total
                    };
                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<PagedList<DynamicModel>> Gets(ushort modelIndex, IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        {
            var source = new TaskCompletionSource<PagedList<DynamicModel>>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                Criteria = (criteria != null ? criteria.ToList() : null),
                PageIndex = pageIndex,
                PageSize = pageSize
                ,ModelIndex = modelIndex
            };

            CustomerSvcClient.GetModelItemsAsync(request).Then(response =>
                {
                    var results = new PagedList<DynamicModel>()
                    {
                        Items = response.DynamicModels.ToDynamicModels(modelIndex),//.ToCustomers(),
                        Total = response.Total
                    };
                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        //public Task<PagedList<V6Soft.Common.ModelFactory.RuntimeModel>> GetAutoListModels(
        //    ushort modelIndex, List<string> outputFields,
        //    IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize)
        //{
        //    var source = new TaskCompletionSource<PagedList<V6Soft.Common.ModelFactory.RuntimeModel>>();
        //    var request = new GetModelsRequest()
        //    {
        //        ModelIndex = modelIndex,
        //        OutputFields = outputFields,//.Select(f => (byte)f).ToList(),
        //        Criteria = (criteria != null ? criteria.ToList() : null),
        //        PageIndex = pageIndex,
        //        PageSize = pageSize
        //    };

        //    CustomerSvcClient.GetModelsAsync(request)
        //        .Then(response =>
        //        {
        //            var results = new PagedList<V6Soft.Common.ModelFactory.RuntimeModel>()
        //            {
        //                Items = response.RuntimeModels.ToModel(modelIndex),//Thay doi may chut
        //                Total = response.Total
        //                //,ColumnsIndex = outputFields
        //            };
        //            source.SetResult(results);
        //        })
        //        .Catch(ex =>
        //        {
        //            var internalEx = ProcessException(ex);
        //            source.SetException(internalEx);
        //        });

        //    return source.Task;
        //}
        

        public Task<OperationResult> TrashCustomer(Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                UID = uid
            };

            CustomerSvcClient.TrashCustomerAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<OperationResult> Trash(ushort modelIndex, Guid uid)
        {
            var source = new TaskCompletionSource<OperationResult>();
            var request = new RemoveModelRequest()
            {
                ModelIndex = modelIndex,
                UID = uid
            };

            CustomerSvcClient.TrashModelItemAsync(request)
                .Then(response =>
                {
                    var result = new OperationResult()
                    {
                        Success = true
                    };
                    source.SetResult(result);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<V6Soft.Models.Accounting.CustomerGroup> GetCustomerGroup_ByCode(IList<string> outputFields, string code)
        {
            var source = new TaskCompletionSource<V6Soft.Models.Accounting.CustomerGroup>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                Code = code
            };

            CustomerSvcClient
                .GetCustomer_ByCodeAsync(request).Then(response =>
                {
                    var results = new V6Soft.Models.Accounting.CustomerGroup();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToAppModel();

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<V6Soft.Models.Accounting.Customer> GetCustomer_ByCode(IList<string> outputFields, string code)
        {
            var source = new TaskCompletionSource<V6Soft.Models.Accounting.Customer>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                Code = code
            };

            CustomerSvcClient
                .GetCustomer_ByCodeAsync(request).Then(response =>
                {
                    var results = new V6Soft.Models.Accounting.Customer();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToCustomer();

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<DynamicModel> Get_ByCode(ushort modelIndex, IList<string> outputFields, string code)
        {
            var source = new TaskCompletionSource<DynamicModel>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                Code = code
            };

            CustomerSvcClient
                .GetModelItem_ByCodeAsync(request).Then(response =>
                {
                    var results = new DynamicModel();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToDynamicModel(modelIndex);

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<CustomerGroup> GetCustomerGroup_ByUID(IList<string> outputFields, Guid uid)
        {
            var source = new TaskCompletionSource<V6Soft.Models.Accounting.CustomerGroup>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                UID = uid
            };

            CustomerSvcClient
                .GetCustomer_ByCodeAsync(request).Then(response =>
                {
                    var results = new V6Soft.Models.Accounting.CustomerGroup();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToAppModel();

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<V6Soft.Models.Accounting.Customer> GetCustomer_ByUID(IList<string> outputFields, Guid uid)
        {
            var source = new TaskCompletionSource<V6Soft.Models.Accounting.Customer>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                UID = uid
            };

            CustomerSvcClient
                .GetCustomer_ByUIDAsync(request).Then(response =>
                {
                    var results = new V6Soft.Models.Accounting.Customer();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToCustomer();

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }

        public Task<DynamicModel> Get_ByUID(ushort modelIndex, IList<string> outputFields, Guid uid)
        {
            var source = new TaskCompletionSource<DynamicModel>();
            var request = new GetModelsRequest()
            {
                OutputFields = outputFields.Select(f => f).ToList(),
                UID = uid
            };

            CustomerSvcClient
                .GetModelItem_ByUIDAsync(request).Then(response =>
                {
                    var results = new DynamicModel();
                    if (response.DynamicModels.Count == 1)
                        results = response.DynamicModels[0].ToDynamicModel(modelIndex);

                    source.SetResult(results);
                })
                .Catch(ex =>
                {
                    var internalEx = ProcessException(ex);
                    source.SetException(internalEx);
                });

            return source.Task;
        }
    }
}
