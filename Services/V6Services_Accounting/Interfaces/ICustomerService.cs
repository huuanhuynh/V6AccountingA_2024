using System.ServiceModel;
using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Accounting.Interfaces
{
    /// <summary>
    ///     Provides methods to operate customer data.
    /// </summary>
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>
        ///     Inserts a new customer group to database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddCustomerGroup(AddModelRequest request);
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddCustomer(AddModelRequest request);
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddModelItem(AddModelRequest request);

        /// <summary>
        ///     Modifies a customer group.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyCustomerGroup(ModifyModelRequest request);
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyCustomer(ModifyModelRequest request);
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyModelItem(ModifyModelRequest request);

        /// <summary>
        ///     Removes a customer group permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveCustomerGroup(RemoveModelRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveCustomer(RemoveModelRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveModelItem(RemoveModelRequest request);

        /// <summary>
        ///     Trash a customer group permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse TrashCustomerGroup(RemoveModelRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse TrashCustomer(RemoveModelRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse TrashModelItem(RemoveModelRequest request);

        /// <summary>
        ///     Gets list of customer groups.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomerGroups(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomers(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetModelItems(GetModelsRequest request);

        /// <summary>
        ///     Gets one customer groups by uid.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomerGroup_ByUID(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomer_ByUID(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetModelItem_ByUID(GetModelsRequest request);

        /// <summary>
        ///     Gets one customer groups by code.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomerGroup_ByCode(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetCustomer_ByCode(GetModelsRequest request);
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetModelItem_ByCode(GetModelsRequest request);
        
    }
}
