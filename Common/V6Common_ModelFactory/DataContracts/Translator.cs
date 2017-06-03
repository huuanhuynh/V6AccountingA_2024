using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.ModelFactory.DataContracts
{
    public static class Translator
    {
        public static ModelFieldDC ToDataContract(this ModelField serviceModel,
            RuntimeModelDC ownerModel)
        {
            if (serviceModel == null) { return null; }
            return new ModelFieldDC()
            {
                DefinitionIndex = serviceModel.DefinitionIndex,
                Value = serviceModel.Value,
                OwnerModel = ownerModel
            };
        }

        public static IEnumerable<ModelFieldDC> ToDataContracts(this IEnumerable<ModelField> serviceModels,
            RuntimeModelDC ownerModel)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToDataContract(ownerModel));
        }

        public static RuntimeModelDC ToDataContract(this RuntimeModel serviceModel)
        {
            if (serviceModel == null) { return null; }
            var dataContract = new RuntimeModelDC(); ;

            //dataContract.Fields = serviceModem_Fieldsds.ToDataContracts(dataContract).ToList();

            return dataContract;
        }

        public static IEnumerable<RuntimeModelDC> ToDataContracts(this IEnumerable<RuntimeModel> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToDataContract());
        }
    }
}
