using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;


namespace V6Soft.Services.Wcf.Common.Behaviors
{
    /// <summary>
    ///     Tells WCF service to keep circular references in data contracts.
    ///     <para/><see cref="http://blogs.msdn.com/b/sowmy/archive/2006/03/26/561188.aspx"/>
    /// </summary>
    public class ReferencePreservingDataContractSerializerOperationBehavior
        : DataContractSerializerOperationBehavior
    {
        public ReferencePreservingDataContractSerializerOperationBehavior(
          OperationDescription operationDescription)
            : base(operationDescription) 
        { 
        }

        public override XmlObjectSerializer CreateSerializer(Type type, string name,
            string ns, IList<Type> knownTypes)
        {
            return CreateDataContractSerializer(type, name, ns, knownTypes);
        }

        public override XmlObjectSerializer CreateSerializer(Type type, 
            XmlDictionaryString name, XmlDictionaryString ns, 
            IList<Type> knownTypes)
        {
            return new DataContractSerializer(type, name, ns, knownTypes,
                0x7FFF, // maxItemsInObjectGraph
                false, // ignoreExtensionDataObject
                true, // preserveObjectReferences
                null // dataContractSurrogate
            );
        }


        private static XmlObjectSerializer CreateDataContractSerializer(
          Type type, string name, string ns, IList<Type> knownTypes)
        {
            return CreateDataContractSerializer(type, name, ns, knownTypes);
        }
    }
}
