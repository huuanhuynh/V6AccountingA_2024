using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;

using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils;
using V6Soft.Common.Utils.StringExtensions;
using V6Soft.Common.Utils.TypeExtensions;


namespace V6Soft.Common.ModelFactory.Factories
{
    public class SqlQueryFactory
    {
        /// <summary>
        ///     Pattern for SELECT query in single table.
        ///     <para/>{0}: table name.
        ///     <para/>{1}: comma-separated list of output column names.
        ///     <para/>{2}: search condition beginning with "AND"
        ///     <para/>     Ex: AND name LIKE '%mike%'
        ///     <para/>{3}: sorted column name.
        ///     <para/>{4}: ASC or DESC.
        /// </summary>
        private static string s_SingleSelectPattern = @"WITH KeyTable AS 
            (SELECT ROW_NUMBER() Over (ORDER BY {3} {4}) As Row, [UID]
                FROM {0}
                WHERE 1=1 {2}
            )
            SELECT [t].[UID] {1} FROM [KeyTable] k
            LEFT JOIN {0} t ON [k].[UID] = [t].[UID]
            WHERE k.Row BETWEEN @from AND @to";

        /// <summary>
        ///     Pattern for SELECT query in 2 or more tables.
        ///     <para/>{0}: primary table name (to get data from).
        ///     <para/>{1}: comma-separated table names (to join with primary table).
        ///     <para/>{2}: comma-separated list of output column names.
        ///     <para/>{3}: search condition beginning with "AND", including JOIN statements.
        ///     <para/>     Ex: AND name LIKE '%mike%'
        ///     <para/>{4}: sorted column name.
        ///     <para/>{5}: ASC or DESC.
        /// </summary>
        private static string s_ManySelectPattern = @"WITH KeyTable AS 
            (SELECT ROW_NUMBER() Over (ORDER BY {4} {5}) As Row, [UID]
                FROM {1}
                WHERE 1=1 {3}
            )
            SELECT [t].[UID] {2} FROM [KeyTable] k
            LEFT JOIN {0} t ON [k].[UID] = [t].[UID]
            WHERE k.Row BETWEEN @from AND @to";

        /// <summary>
        ///     Pattern for SELECT COUNT query.
        ///     <para/>{0}: table name
        ///     <para/>{1}: search condition beginning with "AND"
        ///     <para/>     Ex: AND name LIKE '%mike%'
        /// </summary>
        private static string s_CountPattern = @"SELECT COUNT([UID])
            FROM {0}
            WHERE 1=1 {1}";
        
        /// <summary>
        ///     Pattern for INSERT query.
        ///     <para/>{0}: table name.
        ///     <para/>{1}: comma-separated list of input column names.
        ///     <para/>{2}: comma-separated list of input params.
        ///     <para/>     Ex: @name, @age
        /// </summary>
        private static string s_InsertPattern = @"INSERT INTO {0} ({1}) VALUES ({2})";

        /// <summary>
        ///     Pattern for UPDATE query.
        ///     <para/>{0}: table name.
        ///     <para/>{1}: comma-separated list of col=@col.
        ///     <para/>     Ex: name=@name, age=@age
        /// </summary>
        private static string s_UpdatePattern = @"UPDATE {0}
            SET {1} WHERE [UID]=@uid";

        /// <summary>
        ///     Pattern for DELETE query.
        ///     <para/>{0}: table name.
        /// </summary>
        private static string s_DeletePattern = @"DELETE FROM {0} WHERE [UID]=@uid";

        /// <summary>
        ///     Pattern for ALTER TABLE ADD column query.
        ///     <para/>{0}: table name.
        /// </summary>
        private static string s_AlterPattern = @"ALTER TABLE {0} ADD ";

        /// <summary>
        ///     Gets or sets definition loader.
        /// </summary>
        public static IModelDefinitionManager DefinitionLoader { get; set; }

        /// <summary>
        ///     Creates a command with ALTER TABLE query.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="newFields">Must not be null or empty.</param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateAlterCommand(ushort modelIndex,
            IList<ModelFieldDefinition> newFields)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            var sqlBuilder = new StringBuilder();
            var command = new SqlCommand();
            int i = 1;

            sqlBuilder.AppendFormat(s_AlterPattern, def.Name);
            foreach (var fieldDef in newFields)
            {
                // [{ColumnName}] [{DataType}]({Length}) NULL,
                // Eg: [Story] [nvarchar](255) NULL,
                sqlBuilder.AppendFormat("[{0}] [{1}]{2} NULL{3}", fieldDef.Name,
                    fieldDef.Type.ConvertToXmlSqlType(), GetMaxLength(fieldDef),
                    (i < newFields.Count ? "," : string.Empty));
                i++;
            }
            command.CommandText = sqlBuilder.ToString();
            return command;
        }

        /// <summary>
        ///     Creates a command with SELECT COUNT query.
        /// </summary>
        /// <exception cref="NoDefinitionException"/>
        public static SqlCommand CreateCountCommand(ushort modelIndex,
            IList<SearchCriterion> criteria)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelMap mapping = DefinitionLoader.GetMapping(modelIndex);
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            string whereClause = CreateWhereClause(def, criteria, sqlParams);
            
            // Uses model db name and field db name to create SQL query.
            command.CommandText = string.Format(s_CountPattern,
                mapping.NameMapping.DbName, whereClause);

            return command;
        }

        /// <summary>
        ///     Creates a command with DELETE query.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateDeleteCommand(ushort modelIndex, Guid uid)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            var command = new SqlCommand();

            command.Parameters.Add(new SqlParameter("@uid", uid));
            command.CommandText = string.Format(s_DeletePattern, def.Name);

            return command;
        }

        /// <summary>
        ///     Creates a command with INSERT query.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="fieldValueMap">Must not be null or empty.</param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateInsertCommand(ushort modelIndex,
            IDictionary<string, object> fieldValueMap)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelMap mapping = DefinitionLoader.GetMapping(modelIndex);
            
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            StringBuilder columnNames = new StringBuilder(),
                values = new StringBuilder();
            int i = 0;
            string param;

            foreach (var fieldValue in fieldValueMap)
            {
                //fieldDef = def.Fields[int.Parse(fieldValue.Key)];
                //param = "@p" + i;
                param = "@p" + fieldValue.Key;

                columnNames.AppendFormat("[{0}]", fieldValue.Key).Append(",");
                values.Append(param).Append(",");
                sqlParams.Add(new SqlParameter(param, fieldValue.Value));
                ++i;
            }

            //foreach (var item in def.Fields)
            //{
            //    param = "@p" + item.Name;
            //    columnNames.Append(item.Name).Append(",");
            //    values.Append(param).Append(",");
            //    if (fieldValueMap.ContainsKey(item.Name))
            //    {
            //        sqlParams.Add(new SqlParameter(param, fieldValueMap[item.Name]));
            //    }
            //    else // add default
            //    {
            //        sqlParams.Add(new SqlParameter(param, ""));
            //    }
                
            //}

            command.CommandText = string.Format(s_InsertPattern,
                //def.DBName,
                mapping.NameMapping.DbName,
                columnNames.ToString(0, columnNames.Length - 1),
                values.ToString(0, values.Length - 1)
            );
            return command;
        }

        /// <summary>
        ///     Creates a command with SELECT query.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="outputFieldNames">Must not be null or empty.</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateSelectCommand(ushort modelIndex,
            IList<string> outputFieldNames, IList<SearchCriterion> criteria,
            ushort pageNum, ushort pageSize)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelMap mapping = DefinitionLoader.GetMapping(modelIndex);
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            string whereClause = CreateWhereClause(def, criteria, sqlParams),
                
                // Expect: "field1,field2,field3" or "<empty>".
                outputClause = outputFieldNames.ToString(",");
            
            // outputFieldNames is never null.
            // if outputClause is not empty, prepends a comma ",".
            if (!string.IsNullOrEmpty(outputClause))
            {
                outputClause = "," + outputClause;
            }

            // Use model dbName and field db name to create SQL query.
            command.CommandText = string.Format(s_SingleSelectPattern, 
                mapping.NameMapping.DbName,
                outputClause, whereClause, "UID", "ASC");
                // TODO: Should implement ORDER BY.

            int fromIndex = (pageNum - 1) * pageSize + 1,
                toIndex = fromIndex + pageSize - 1;
            sqlParams.Add(new SqlParameter("@from", fromIndex));
            sqlParams.Add(new SqlParameter("@to", toIndex));

            return command;
        }

        /// <summary>
        ///     Creates a command with SELECT query.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="outputFieldNames">Must not be null or empty.</param>
        /// <param name="criteria">Optional</param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateManySelectCommand(ushort modelIndex,
            IList<string> outputFieldNames, IList<SearchCriterion> criteria,
            ushort pageNum, ushort pageSize)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelMap mapping = DefinitionLoader.GetMapping(modelIndex);
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            string whereClause = CreateWhereClause(def, criteria, sqlParams),

                // Expect: "field1,field2,field3" or "<empty".
                outputClause = outputFieldNames.ToString(",");

            // outputFieldNames is never null.
            // if outputClause is not empty, prepends a comma ",".
            if (!string.IsNullOrEmpty(outputClause))
            {
                outputClause = "," + outputClause;
            }

            // Use model dbName and field db name to create SQL query.
            command.CommandText = string.Format(s_ManySelectPattern,
                mapping.NameMapping.DbName,
                outputClause, whereClause, "UID", "ASC");
            // TODO: Should implement ORDER BY.

            int fromIndex = (pageNum - 1) * pageSize + 1,
                toIndex = fromIndex + pageSize - 1;
            sqlParams.Add(new SqlParameter("@from", fromIndex));
            sqlParams.Add(new SqlParameter("@to", toIndex));

            return command;
        }

        /// <summary>
        ///     Creates a command with UPDATE query.
        /// </summary>
        /// <param name="modelIndex"></param>
        /// <param name="fieldValueMap">
        ///     Must not be null or empty. ID field must have value.
        /// </param>
        /// <exception cref="NoDefinitionException"/>
        /// <exception cref="NotSupportedException">
        ///     Thrown when a field's type can't be matched to an equivalent 
        ///     Xml Sql data type.
        /// </exception>
        public static SqlCommand CreateUpdateCommand(ushort modelIndex, Guid id,
            IDictionary<string, object> fieldValueMap)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelFieldDefinition fieldDef;
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            StringBuilder setClause = new StringBuilder();
            int i = 0;
            string param;

            foreach (KeyValuePair<string, object> field in fieldValueMap)
            {
                // TODO: Use field name directly
                //fieldDef = def.Fields[(int)field.Key];
                param = "@p" + i;
                setClause.AppendFormat("{0}={1},", field.Key, param);
                sqlParams.Add(new SqlParameter(param, field.Value));
                ++i;
            }
            sqlParams.Add(new SqlParameter("@uid", id));

            command.CommandText = string.Format(s_UpdatePattern,
                def.Name, setClause.ToString(0, setClause.Length - 1));
            return command;
        }


        private static string CreateWhereClause(ModelDefinition modelDef,
            IList<SearchCriterion> criteria, SqlParameterCollection sqlParams)
        {
            if (criteria == null || criteria.Count == 0) { return string.Empty; }

            var whereBuilder = new StringBuilder();
            ModelFieldDefinition fieldDef;
            bool isLikeOperation;
            int i = 0;
            string paramName;
            object conditionValue;

            foreach (var criterion in criteria)
            {
                //fieldDef = modelDef.Fields[criterion.FieldIndex];
                //if (fieldDef == null)
                //{
                //    throw new NoDefinitionException(criterion.FieldIndex, false);
                //}
                isLikeOperation = false;
                conditionValue = criterion.ConditionValue;
                paramName = "@p" + i++;
                whereBuilder.Append(" AND ").Append(criterion.FieldName);
                switch(criterion.CompareOperator)
                {
                    case CompareOperator.BeginWith:
                        conditionValue = conditionValue + "%";
                        isLikeOperation = true;
                        break;
                    case CompareOperator.EndWith:
                        conditionValue = "%" + conditionValue;
                        isLikeOperation = true;
                        break;
                    case CompareOperator.Contain:
                        conditionValue = "%" + conditionValue + "%";
                        isLikeOperation = true;
                        break;
                    case CompareOperator.Equal:
                        whereBuilder.Append("=");
                        break;
                    case CompareOperator.NotEqual:
                        whereBuilder.Append("!=");
                        break;
                    case CompareOperator.Greater:
                        whereBuilder.Append(">");
                        break;
                    case CompareOperator.GreaterOrEqual:
                        whereBuilder.Append(">=");
                        break;
                    case CompareOperator.Less:
                        whereBuilder.Append("<");
                        break;
                    case CompareOperator.LessOrEqual:
                        whereBuilder.Append("<=");
                        break;
                }
                if (isLikeOperation)
                {
                    whereBuilder.Append(string.Format(" LIKE {0}", paramName));
                }
                else
                {
                    whereBuilder.Append(paramName);
                }
                sqlParams.Add(new SqlParameter(paramName, conditionValue));
            }
            return whereBuilder.ToString();
        }

        private static ModelDefinition GetModelDef(ushort modelIndex)
        {
            ModelDefinition def = DefinitionLoader[modelIndex];
            if (def == null)
            {
                throw new NoDefinitionException(modelIndex);
            }
            return def;
        }

        private static string GetMaxLength(ModelFieldDefinition fieldDef)
        {
            if (fieldDef.Constraints != null && fieldDef.Constraints.Count > 0)
            {
                var constraint = (LengthConstraint)fieldDef.Constraints
                    .FirstOrDefault(c => c is LengthConstraint);
                if (constraint != null)
                {
                    return string.Format("({0})", constraint.MaxLength);
                }
            }
            return string.Empty;
        }
    }
}
