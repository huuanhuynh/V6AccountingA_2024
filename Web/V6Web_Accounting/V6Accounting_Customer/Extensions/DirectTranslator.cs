using System;
using System.Dynamic;
using System.Linq;
using System.Collections.Generic;

using V6Soft.Models.Accounting;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Factories;

using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;
using AccModels = V6Soft.Models.Accounting;


namespace V6Soft.Dealers.Accounting.Customer.Extensions.DirectTranslator
{
    // TODO: Should move to Models project.
    public static class ServiceTranslator
    {
        public static CustomerGroup ToCustomerGroup(this DynamicModel serviceModel)
        {
            var appModel = new CustomerGroup(serviceModel);
            return appModel;
        }

        public static Model ToModel(this DynamicModel serviceModel)
        {
            var appModel = new Model(serviceModel);
            return appModel;
        }

        public static IList<CustomerGroup> ToCustomerGroups(this IEnumerable<DynamicModel> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToCustomerGroup()).ToList();
        }

        public static IList<Model> ToModels(this IEnumerable<DynamicModel> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToModel()).ToList();
        }

        public static AccModels.Customer ToCustomer(this DynamicModel serviceModel)
        {
            var appModel = new AccModels.Customer(serviceModel);
            return appModel;
        }

        public static IList<AccModels.Customer> ToCustomers(this IEnumerable<DynamicModel> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToCustomer()).ToList();
        }

        /*
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
        //*/
        

    }
}
