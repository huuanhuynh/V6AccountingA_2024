using System.ServiceModel;

using V6Soft.Services.Wcf.Common.Models;
using V6Soft.Services.Wcf.Common.ServiceContracts;


namespace V6Soft.Services.Assistant.Interfaces
{
    /// <summary>
    ///     Provides methods to operate product data.
    /// </summary>
    [ServiceContract]
    public interface IAssistantService
    {
        #region Province

        /// <summary>
        ///     Inserts a new province to database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddProvince(AddModelRequest request);

        /// <summary>
        ///     Modifies a provice.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyProvince(ModifyModelRequest request);

        /// <summary>
        ///     Removes a province permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveProvince(RemoveModelRequest request);
        
        /// <summary>
        ///     Gets list of provinces.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetProvinces(GetModelsRequest request);

        #endregion


        #region District

        /// <summary>
        ///     Inserts a new district to database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddDistrict(AddModelRequest request);

        /// <summary>
        ///     Modifies a district.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyDistrict(ModifyModelRequest request);

        /// <summary>
        ///     Removes a district permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveDistrict(RemoveModelRequest request);
        
        /// <summary>
        ///     Gets list of districts.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetDistricts(GetModelsRequest request);

        #endregion


        #region Ward
        /// <summary>
        ///     Inserts a new ward to database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        AddModelResponse AddWard(AddModelRequest request);

        /// <summary>
        ///     Modifies a ward.
        /// </summary>
        /// <param name="model">Must specify UID field.</param>
        [OperationContract]
        [FaultContract(typeof(ConstraintViolationFault))]
        [FaultContract(typeof(OperationFault))]
        ModifyModelResponse ModifyWard(ModifyModelRequest request);

        /// <summary>
        ///     Removes a ward permanently from database.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        RemoveModelResponse RemoveWard(RemoveModelRequest request);
        
        /// <summary>
        ///     Gets list of wards.
        /// </summary>
        [OperationContract]
        [FaultContract(typeof(OperationFault))]
        GetModelsResponse GetWards(GetModelsRequest request);

        #endregion
    }
}
