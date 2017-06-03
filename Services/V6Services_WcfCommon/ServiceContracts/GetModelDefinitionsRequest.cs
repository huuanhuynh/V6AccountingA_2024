using System.ServiceModel;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Request to get model definition.
    /// </summary>
    [MessageContract]
    public class GetModelDefinitionsRequest
    {
        [MessageBodyMember]
        public string ModelName { get; set; }
    }
}
