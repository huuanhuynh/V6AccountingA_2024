using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils;
using V6Soft.Models.Core.Constants;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Wcf.Common
{
    public abstract class RuntimeDataFarmerBase : IRuntimeDataFarmerBase
    {
        #region Constants & Static
        
        protected static SequentialGuid s_UID = new SequentialGuid();

        #endregion


        #region Properties

        protected Guid NextUID
        {
            get
            {
                s_UID++;
                return s_UID.Value;
            }
        }

        protected string ConnectionString { get; set; }

        #endregion


        #region Constructor

        public RuntimeDataFarmerBase(string connectionString)
        {
            Guard.ArgumentNotNull(connectionString, "connectionString");

            ConnectionString = connectionString;
            SqlQueryFactory.DefinitionLoader = DynamicModelFactory.DefinitionLoader;
        }

        #endregion


        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Add"/>
        /// </summary>
        public bool Add(ushort modelIndex, DynamicModel addedModel)
        {
            if (addedModel == null) { return false; }

            addedModel.Field(DefinitionName.Fields.UID, NextUID);
            
            // Gets field name-value dictionary, with in-db field names.
            IDictionary<string, object> serializedModel = SerializeModel(addedModel);

            SqlCommand command = SqlQueryFactory.CreateInsertCommand(modelIndex,
                serializedModel);
            bool result;
            command.Connection = new SqlConnection(ConnectionString);
            command.Connection.Open();
            try
            {
                result = (1 == command.ExecuteNonQuery());
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Modify"/>
        /// </summary>
        public bool Modify(ushort modelIndex, DynamicModel changedModel)
        {
            if (changedModel == null) { return false; }

            /* TODO: Switch from index-based models to name-based models
            IDictionary<byte, object> serializedModel = changedModel.ToDictionary();
            Guid id = (Guid)serializedModel[(byte)FieldIndex.ID];
            serializedModel.Remove((byte)FieldIndex.ID);
            //*/
            IDictionary<string, object> serializedModel = changedModel.ToDictionary();
            Guid id = (Guid)serializedModel[DefinitionName.Fields.UID];
            serializedModel.Remove(DefinitionName.Fields.UID);

            SqlCommand command = SqlQueryFactory.CreateUpdateCommand(modelIndex,
                id, serializedModel);
            bool success = false;
            command.Connection = new SqlConnection(ConnectionString);
            command.Connection.Open();
            try
            {
                success = (0 < command.ExecuteNonQuery());
            }
            finally
            {
                command.Connection.Close();
            }
            return success;
        }

        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Remove"/>
        /// </summary>
        public bool Remove(ushort modelIndex, Guid uid)
        {
            SqlCommand command = SqlQueryFactory.CreateDeleteCommand(modelIndex, uid);
            bool success = false;
            command.Connection = new SqlConnection(ConnectionString);
            command.Connection.Open();
            try
            {
                success = (1 == command.ExecuteNonQuery());
            }
            finally
            {
                command.Connection.Close();
            }
            return success;
        }

        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Search"/>
        /// </summary>
        public IList<DynamicModel> Search(ushort modelIndex, IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total)
        {
            ModelMap modelMap = GetModelMap(modelIndex);

            // Maps from in-app field names to in-db column names.
            if (criteria != null && criteria.Count > 0)
            {
                foreach (var c in criteria)
                {
                    c.FieldName = modelMap.MapName(c.FieldName);
                }
            }

            var selectTask = Task.Factory.StartNew<IList<DynamicModel>>(() =>
            {
                return ExecuteSelect(modelIndex, outputFields, criteria, pageIndex,
                    pageSize);
            });

            var countTask = Task.Factory.StartNew<ulong>(() =>
            {
                return ExecuteCount(modelIndex, criteria);
            });

            Task.WaitAll(selectTask, countTask);
            IList<DynamicModel> models = selectTask.Result;
            total = countTask.Result;
            return models;
        }
        
        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Trash"/>
        /// </summary>
        public bool Trash(ushort modelIndex, Guid uid)
        {
            // Should:
            // - Covert to XML string.
            // - Call a stored procedure to insert this XML to trash table.
            // - Delete model from its table.
            // - Log this action to audit table.
            return false;
        }


        #region Protected & Private methods

        protected ModelDefinition GetModelDef(ushort modelIndex)
        {
            IModelDefinitionManager loader = DynamicModelFactory.DefinitionLoader;
            ModelDefinition modelDef = loader.Load(modelIndex);
            return modelDef;
        }

        protected ModelMap GetModelMap(ushort modelIndex)
        {
            IModelDefinitionManager loader = DynamicModelFactory.DefinitionLoader;
            ModelMap modelMap = loader.GetMapping(modelIndex);
            return modelMap;
        }

        private ulong ExecuteCount(ushort modelIndex, IList<SearchCriterion> criteria)
        {
            SqlCommand command = SqlQueryFactory.CreateCountCommand(modelIndex, criteria);
            command.Connection = new SqlConnection(ConnectionString);
            command.Connection.Open();
            object result = command.ExecuteScalar();
            return Convert.ToUInt64(result);
        }

        private IList<DynamicModel> ExecuteSelect(ushort modelIndex, 
            IList<string> outputFields, IList<SearchCriterion> criteria, 
            ushort pageIndex, ushort pageSize)
        {
            ModelDefinition modelDef = GetModelDef(modelIndex);
            IList<ModelFieldDefinition> fieldDefs = modelDef.Fields;
            ModelMap modelMap = GetModelMap(modelIndex);

            IList<string> fieldNames;

            // Maps from in-app field names to in-db column names.
            if (outputFields != null && outputFields.Count > 0)
            {                
                fieldNames = outputFields.AsParallel().Select(f =>
                {
                    return modelMap.MapName(f);
                }).ToList();
            }
            else // Creates empty list if the given instance is null.
            {
                fieldNames = new List<string>();
                //if (outputFields == null)
                //{
                //    outputFields = new List<string>();
                //}
            }
            
            SqlCommand command = SqlQueryFactory.CreateSelectCommand(modelIndex,
                fieldNames, criteria, pageIndex, pageSize);
            command.Connection = new SqlConnection(ConnectionString);
            command.Connection.Open();

            // Adds column UID
            //outputFields.Add((byte)FieldIndex.ID);
            fieldNames.Add(DefinitionName.Fields.UID);
            var resultModels = new List<DynamicModel>();
            using (SqlDataReader reader =
                command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                DynamicModel model;
                object dbValue;
                while (reader.Read())
                {
                    // Creates and fills values to new model.
                    model = DynamicModelFactory.CreateModel(modelIndex);
                    // TODO: Should use DatabaseExtensions to convert from db type to c# type.
                    foreach (string fieldName in fieldNames)
                    {
                        dbValue = reader[fieldName];
                        dbValue = (DBNull.Value == dbValue) ? null : dbValue;
                        // Sets value to field using in-app name.
                        model.TrySetMember(modelMap.MapName(fieldName), dbValue);
                    }
                    resultModels.Add(model);
                }
            }

            return (resultModels.Any() ? resultModels : null);
        }

        private IDictionary<string, object> SerializeModel(DynamicModel model)
        {
            ModelMap modelMap = GetModelMap(model.DefinitionIndex);

            // Maps field names to in-db names.
            IDictionary<string, object> serializedModel =
                model.FieldMap.ToDictionary(
                    item => modelMap.MapName(item.Key),
                    item => item.Value);

            return serializedModel;
        }

        #endregion
    }
}
