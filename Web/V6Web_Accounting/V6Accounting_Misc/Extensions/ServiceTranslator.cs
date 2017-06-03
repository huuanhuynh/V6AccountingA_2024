using System;
using System.Dynamic;
using System.Linq;
using System.Collections.Generic;

using V6Soft.Models.Accounting;
using V6Soft.ServiceClients.Accounting.Customer.CustomerService;
using V6Soft.ServiceClients.Accounting.Customer.DefinitionService;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.Dealers.Accounting.Customer.Extensions.ServiceTranslator
{
    // TODO: Should move to Models project.
    public static class ServiceTranslator
    {
        public static CustomerGroup ToAppModel(this DynamicModelDC serviceModel)
        {
            var appModel = new CustomerGroup();
            //appModel.Definition = 
                //RuntimeModelFactory.DefinitionLoader[(ushort)ModelIndex.CustomerGroup];
            appModel.FieldMap = serviceModel.Fields;
            //appModel.SetAllFields(serviceModel.Fields.ToArray());
            return appModel;
        }
        public static V6Soft.Models.Accounting.Customer ToCustomer(this DynamicModelDC serviceModel)
        {
            var appModel = new V6Soft.Models.Accounting.Customer();
            appModel.Definition =
                RuntimeModelFactory.DefinitionLoader[(ushort)ModelIndex.Customer];
            appModel.FieldMap = serviceModel.Fields;
            //appModel.SetAllFields(serviceModel.Fields.ToArray());
            return appModel;
        }
        public static DynamicModel ToDynamicModel(this DynamicModelDC serviceModel, ushort modelIndex)
        {
            var appModel = new DynamicModel();
            appModel.Definition =
                RuntimeModelFactory.DefinitionLoader[modelIndex];
            appModel.FieldMap = serviceModel.Fields;
            //appModel.SetAllFields(serviceModel.Fields.ToArray());
            return appModel;
        }

        public static IList<CustomerGroup> ToAppModels(this IEnumerable<DynamicModelDC> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToAppModel()).ToList();
        }
        public static IList<V6Soft.Models.Accounting.Customer> ToCustomers(this IEnumerable<DynamicModelDC> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToCustomer()).ToList();
        }
        public static IList<DynamicModel> ToDynamicModels(this IEnumerable<DynamicModelDC> serviceModels, ushort modelIndex)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToDynamicModel(modelIndex)).ToList();
        }

        public static IList<ModelDefinition> ToAppModels(this IEnumerable<ModelDefinitionDC> rawDefinitions)
        {
            if (rawDefinitions == null) { return null; }

            return rawDefinitions.Select(d => d.ToAppModel()).ToList();
        }

        public static ModelDefinition ToAppModel(this ModelDefinitionDC rawDefinition)
        {
            var newDefinition = new ModelDefinition(
                rawDefinition.Index, rawDefinition.Name, 
                rawDefinition.Fields.ToAppModels()
            );

            return newDefinition;
        }

        public static IList<ModelFieldDefinition> ToAppModels(this IList<ModelFieldDefinitionDC> rawFields)
        {
            if (rawFields == null) { return null; }

            return rawFields.Select(f =>
                {
                    return new ModelFieldDefinition(
                        f.Label, f.Name, Type.GetType(f.Type), f.Group, 
                        f.Constraints.ToIFieldConstraints()
                    );
                }).ToList();
        }

        public static IList<IFieldConstraint> ToIFieldConstraints(this IEnumerable<object> rawObjects)
        {
            if (rawObjects == null) { return null; }

            return rawObjects.Select<object, IFieldConstraint>(constraint => 
            {
                switch (constraint.GetType().Name){
                    case "NotNullFieldConstraintDC":
                        return new NotNullFieldConstraint();
                    case "LengthConstraintDC":
                        var lenCon = constraint as LengthConstraintDC;
                        return new LengthConstraint(lenCon.MinLength, lenCon.MaxLength);
                    default:
                        return null;
                }
            }).ToList();
        }

        public static IList<ModelMap> ToAppModels(this IEnumerable<ModelMapDC> rawMaps)
        {
            if (rawMaps == null) { return null; }
            return rawMaps.Select(d => d.ToAppModel()).ToList();
        }

        public static ModelMap ToAppModel(this ModelMapDC rawMap)
        {
            var newDefinition = new ModelMap(
                rawMap.NameMapping, rawMap.FieldMappings, rawMap.FieldGroups.ToArray()
            );

            return newDefinition;
        }


        public static RuntimeModelDC ToServiceModel(this CustomerGroup appModel)
        {
            var serviceModel = new RuntimeModelDC()
            {
                Fields = appModel.Fields.ToList()
            };
            
            return serviceModel;
        }

        public static RuntimeModelDC ToServiceModel(this V6Soft.Models.Accounting.Customer appModel)
        {
            var serviceModel = new RuntimeModelDC()
            {
                Fields = appModel.Fields.ToList()
            };

            return serviceModel;
        }

        public static RuntimeModelDC ToServiceModel(this DynamicModel appModel)
        {
            var serviceModel = new RuntimeModelDC()
            {
                Fields = appModel.Fields.ToList()
            };

            return serviceModel;
        }

        public static RuntimeModelDC ToServiceModel(this PaymentMethod appModel)
        {
            var serviceModel = new RuntimeModelDC()
            {
                Fields = appModel.Fields.ToList()
            };

            return serviceModel;
        }


    }
}
