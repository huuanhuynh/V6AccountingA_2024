using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using V6Soft.Common.ModelFactory;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.DataAccess.Accounting.Common.DataFarmers;
using V6Soft.Interfaces.Accounting.Customer.DataFarmers;
using ModelIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Model;


namespace V6Soft.DataAccess.Accounting.Customer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ICustomerDataFarmer"/>
    /// </summary>
    public class DynamicCustomerDataFarmer : RuntimeDataFarmerBase, ICustomerDataFarmer
    {
        public DynamicCustomerDataFarmer(string connectionString)
            : base(connectionString)
        {
        }

        #region Customer Group

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.AddCustomerGroup"/>
        /// </summary>
        public bool AddCustomerGroup(DynamicModel addedGroup)
        {
            bool result = Add((ushort)ModelIndex.CustomerGroup, addedGroup);
            return result;
        }

        public bool AddCustomer(DynamicModel addedGroup)
        {
            bool result = Add((ushort)ModelIndex.Customer, addedGroup);
            return result;
        }

        public bool AddModel(ushort modelIndex, DynamicModel addedModel)
        {
            bool result = Add(modelIndex, addedModel);
            return result;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.ModifyCustomerGroup"/>
        /// </summary>
        public bool ModifyCustomerGroup(DynamicModel modifiedGroup)
        {
            bool result = Modify((ushort)ModelIndex.CustomerGroup, modifiedGroup);
            return result;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.RemoveCustomerGroup"/>
        /// </summary>
        public bool RemoveCustomerGroup(Guid uid)
        {
            bool result = Remove((ushort)ModelIndex.CustomerGroup, uid);
            return result;
        }

        public IList<DynamicModel> SearchCustomerGroups(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search((byte)ModelIndex.CustomerGroup,
                outputFields, criteria, pageIndex, pageSize, out total);
            return results;
        }

        public IList<DynamicModel> SearchModels(ushort modelIndex, IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search((byte)modelIndex,
                outputFields, criteria, pageIndex, pageSize, out total);
            return results;
        }

        public IList<DynamicModel> SearchModelsAutoCorrect(ushort modelIndex, IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            string field = "", search = "";
            if (criteria.Count > 0)
            {
                field = criteria[0].FieldName;
                search = criteria[0].ConditionValue.ToString();
            }

            IList<DynamicModel> results = Search((byte)modelIndex,
                outputFields, criteria, pageIndex, pageSize, out total);

            if(total > pageSize)
            {
                if(criteria != null && criteria.Count>0)
                {
                    ulong tempTotal = 0;
                    while (results.Count > 0)
                    {
                        results.RemoveAt(0);
                    }
                    
                    string typeChars = "0123456789abcdefghijklmnopqrstuvwxyz";
                    foreach (char c in typeChars)
                    {
                        var criteria2 = new List<SearchCriterion>();
                        criteria2.Add(
                            new SearchCriterion()
                            {
                                FieldName = field,
                                CompareOperator = CompareOperator.Contain,
                                ConditionValue = search + c
                            }
                        );
                        
                        IList<DynamicModel> results1 = Search((byte)modelIndex,
                        outputFields, criteria2, 1, 1, out tempTotal);
                        if (results1!=null && results1.Count > 0)
                            results.Add(results1[0]);
                    }
                }
                
                return results;
            }
            else
            {
                return results;
            }
        }

        
        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.TrashCustomerGroup"/>
        /// </summary>
        public bool TrashCustomerGroup(Guid uid)
        {
            bool result = Trash((ushort)ModelIndex.CustomerGroup, uid);
            return result;
        }

        /// <summary>
        ///     See <see cref="ICustomerDataFarmer.GetGroupsByCustomerUid"/>
        /// </summary>
        public IList<DynamicModel> GetGroupsByCustomerUid(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Customer
        
        public IList<DynamicModel> SearchCustomers(IList<string> outputFields, IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize, out ulong total)
        {
            IList<DynamicModel> results = Search((byte)ModelIndex.Customer,
                outputFields, criteria, pageIndex, pageSize, out total);
            return results;
        }
        
        #endregion

        #region Private methods


        /// <summary>
        ///     See <see cref="IRuntimeDataFarmerBase.Search"/>
        /// </summary>
        public IList<DynamicModel> SearchCustomerGroupByCustomerUid(ushort modelIndex, IList<string> outputFields,
            IList<SearchCriterion> criteria, ushort pageIndex, ushort pageSize,
            out ulong total)
        {
            ModelMap modelMap = GetModelMap(modelIndex);
            string customerUID;

            // Maps from in-app field names to in-db column names.
            if (criteria != null && criteria.Count > 0)
            {
                foreach (var c in criteria)
                {
                    if (c.FieldName == "CustomerUID")
                    {
                        customerUID = c.ConditionValue + "";
                    }
                    else
                    {
                        c.FieldName = modelMap.MapName(c.FieldName);
                    }
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
            else // Select ALL columns if `outputFields` is empty.
            {
                fieldNames = fieldDefs
                        .Where(f => f.Name != DefinitionName.Fields.UID)
                        .Select(f => modelMap.MapName(f.Name)).ToList();
                //fieldNames = new List<string>();
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


        #endregion
    }
}
