using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Wcf.Common.ServiceContracts
{
    /// <summary>
    ///     Response to get model definition.
    /// </summary>
    [MessageContract]
    public class GetModelDefinitionsResponse
    {
        [MessageBodyMember]
        public ModelDefinitionDC ModelDefinition { get; set; }
        /// <summary>
        ///     Gets or sets model definitions.
        /// </summary>
        [MessageBodyMember]
        public IList<ModelDefinitionDC> ModelDefinitions { get; set; }

        /// <summary>
        ///     Gets or sets model mappings.
        /// </summary>
        [MessageBodyMember]
        public IList<ModelMapDC> ModelMaps { get; set; }
    }
}
