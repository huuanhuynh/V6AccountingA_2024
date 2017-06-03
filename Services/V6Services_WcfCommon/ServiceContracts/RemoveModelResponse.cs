﻿using System.ServiceModel;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Response message contract to remove runtime model(s).
    /// </summary>
    [MessageContract]
    public class RemoveModelResponse
    {
        [MessageBodyMember]
        public bool Success;
    }
}
