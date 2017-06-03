using System.ServiceModel;
using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.ServiceContracts;

namespace V6Soft.Services.Accounting.Interfaces
{
    [ServiceContract]
    interface IPaymentMethodService
    {
        /// <summary>
        ///     Inserts a new payment method to database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddPaymentMethod(AddModelRequest request);

        /// <summary>
        ///     Modifies a payment method.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyPaymentMethod(ModifyModelRequest request);

        /// <summary>
        ///     Removes a payment method permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemovePaymentMethod(RemoveModelRequest request);

        /// <summary>
        ///     Gets list of payment method.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetPaymentMethods(GetModelsRequest request);
    }
}
