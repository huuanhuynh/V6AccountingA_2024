using System;
using System.Collections.Generic;
using System.Linq;

using V6Soft.Services.Wcf.Common.Models;


namespace V6Soft.Services.Wcf.Common.TranslatorExtensions
{
    public static class Translator
    {
        /*
        #region Model

        [Obsolete]
        public static DynamicModel ToServiceModel(this RuntimeModelDC dataContract,
            ushort modelIndex)
        {
            if (dataContract == null) { return null; }

            var runtimeModel = DynamicModelFactory.CreateModel(modelIndex);
            runtimeModel.SetAllFields(dataContract.Fields);

            return runtimeModel;
        }

        public static DynamicModel ToServiceModel(this DynamicModelDC dataContract,
            ushort modelIndex)
        {
            if (dataContract == null) { return null; }

            var runtimeModel = DynamicModelFactory.CreateModel(modelIndex);
            runtimeModel.SetFields(dataContract.Fields);

            return runtimeModel;
        }

        [Obsolete]
        public static IList<DynamicModel> ToServiceModels(this IList<RuntimeModelDC> dataContracts,
            ushort modelIndex)
        {
            if (dataContracts == null) { return null; }

            return dataContracts.Select(d => d.ToServiceModel(modelIndex)).ToList();
        }

        public static IList<DynamicModel> ToServiceModels(this IList<DynamicModelDC> dataContracts,
            ushort modelIndex)
        {
            if (dataContracts == null) { return null; }

            return dataContracts.Select(d => d.ToServiceModel(modelIndex)).ToList();
        }

        public static DynamicModelDC ToDataContract(this DynamicModel serviceModel)
        {
            if (serviceModel == null) { return null; }

            var dataContract = new DynamicModelDC()
            {
                Fields = serviceModel.FieldMap
            };

            return dataContract;
        }

        public static IList<DynamicModelDC> ToDataContracts(this IList<DynamicModel> serviceModels)
        {
            if (serviceModels == null) { return null; }

            return serviceModels.Select(m => m.ToDataContract()).ToList();
        }

        public static NotNullFieldConstraintDC ToDataContract(this NotNullFieldConstraint serviceModel)
        {
            return new NotNullFieldConstraintDC();
        }

        public static LengthConstraintDC ToDataContract(this LengthConstraint serviceModel)
        {
            return new LengthConstraintDC()
            {
                MaxLength = serviceModel.MaxLength,
                MinLength = serviceModel.MinLength
            };
        }

        public static IList<IFieldConstraintDC> ToDataContracts(this IList<IFieldConstraint> serviceModels)
        {
            if (serviceModels == null || serviceModels.Count == 0) { return null; }
            
            IList<IFieldConstraintDC> dataContracts = 
                serviceModels.Select<IFieldConstraint, IFieldConstraintDC>(m =>
                {
                    switch (m.GetType().Name)
                    {
                        case "NotNullFieldConstraint":
                            return new NotNullFieldConstraintDC();
                        case "LengthConstraint":
                            var lc = m as LengthConstraint;
                            return new LengthConstraintDC()
                            {
                                MaxLength = lc.MaxLength,
                                MinLength = lc.MinLength
                            };
                        default:
                            return null;
                    }
                }).ToList();

            return dataContracts;
        }
        
        #endregion


        #region Definition

        /// <summary>
        ///     Creates data contracts by copying field values and alters names 
        ///     in these definitions to application names.
        /// </summary>
        public static IList<ModelDefinitionDC> ToDataContracts(this IList<ModelDefinition> rawDefinition)
        {
            if (rawDefinition == null) { return null; }

            return rawDefinition.Select(d => d.ToDataContract()).ToList();
        }

        /// <summary>
        ///     Creates a data contract by copying field values and alters names 
        ///     in this definition to application name.
        /// </summary>
        public static ModelDefinitionDC ToDataContract(this ModelDefinition rawDefinition)
        {
            ModelMap modelMap = DynamicModelFactory.DefinitionLoader.
                GetMapping(rawDefinition.Index);
            IList<ModelFieldDefinitionDC> newFields = rawDefinition.Fields.ToDataContracts(modelMap);
            var newDefinition = new ModelDefinitionDC()
            {
                Index = rawDefinition.Index,
                Name = modelMap.NameMapping.AppName,
                DBName = modelMap.NameMapping.DbName,
                Fields = newFields
            };
                 
            return newDefinition;
        }

        /// <summary>
        ///     Creates data contracts by copying field values and alters names 
        ///     in these definitions to application names.
        /// </summary>
        public static IList<ModelFieldDefinitionDC> ToDataContracts(this IList<ModelFieldDefinition> rawFields,
            ModelMap modelMap)
        {
            if (rawFields == null) { return new List<ModelFieldDefinitionDC>(); }

            var newFields = new List<ModelFieldDefinitionDC>(rawFields.Count);
            ModelFieldDefinition fieldDef;
            IList<FieldMapping> fieldMappings = modelMap.FieldMappings;

            for (int i = 0; i < rawFields.Count && i < fieldMappings.Count; i++)
            {
                fieldDef = rawFields[i];
                newFields.Add(fieldDef.ToDataContract(fieldMappings[i]));
            }

            return newFields;
        }

        /// <summary>
        ///     Creates a data contract by copying field values and alters names 
        ///     in this definition to application name.
        /// </summary>
        public static ModelFieldDefinitionDC ToDataContract(this ModelFieldDefinition rawDefinition,
            FieldMapping fieldMapping)
        {
            var newDefinition = new ModelFieldDefinitionDC()
            {
                Label = rawDefinition.Label,
                Name = fieldMapping.AppName,
                Type = rawDefinition.Type.FullName,
                Group = rawDefinition.Group,
                Constraints = rawDefinition.Constraints.ToDataContracts()
            };

            return newDefinition;

        }

        /// <summary>
        ///     Creates a data contract by copying field values and alters names 
        ///     to application name.
        /// </summary>
        public static ModelMapDC ToDataContract(this ModelMap rawModelMap)
        {
            var nameMapping = new NameMapping(rawModelMap.NameMapping.DbName, rawModelMap.NameMapping.AppName);
            IList<FieldMapping> fieldMappings = rawModelMap.FieldMappings.Select(
                fm => new FieldMapping(null, fm.AppName, fm.Label, fm.Group)
            ).ToList();
            var newModelMap = new ModelMapDC()
            {
                NameMapping = nameMapping,
                FieldMappings = fieldMappings,
                FieldGroups = rawModelMap.FieldGroups
            };

            return newModelMap;
        }

        /// <summary>
        ///     Creates data contracts by copying field values and alters names 
        ///     to application names.
        /// </summary>
        public static IList<ModelMapDC> ToDataContracts(this IEnumerable<ModelMap> rawModelMaps)
        {
            return rawModelMaps.Select(m => m.ToDataContract()).ToList();
        }

        #endregion
        //*/
    }
}
