using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using V6Soft.Common.ModelFactory.Managers;
using V6Soft.Common.Utils.StringExtensions;


namespace V6Soft.Common.ModelFactory.Factories
{
    public sealed class SqlQueryBuilder
    {
        private static string s_LimitSelectPattern = @"WITH KeyTable AS 
            (SELECT ROW_NUMBER() Over (ORDER BY {3} {4}) As Row, [UID]
                FROM {0}
                WHERE 1=1 {2}
            )
            SELECT [t].[UID] {1} FROM [KeyTable] k
            LEFT JOIN {0} t ON [k].[UID] = [t].[UID]
            WHERE k.Row BETWEEN @from AND @to";


        public static SqlQueryBuilder NewInstance
        {
            get
            {
                return new SqlQueryBuilder();
            }
        }

        public static IModelDefinitionManager DefinitionLoader { get; set; }


        private  StringBuilder LeftJoinBuilder
        {
            get
            {
                if (m_LeftJoin == null)
                {
                    m_LeftJoin = new StringBuilder();
                }
                return m_LeftJoin;

            }
        }


        private string m_Query;
        private string m_PrimaryTable;
        private StringBuilder m_Where;
        private StringBuilder m_LeftJoin;
        private byte m_ParamCount;
        private string m_OrderByField;
        private uint m_LimitFrom;
        private uint m_LimitTo;

        private SqlQueryBuilder()
        {
            m_Where = new StringBuilder(" WHERE 1=1 ");
            m_LeftJoin = new StringBuilder();
            m_ParamCount = 0;
        }

        public SqlQueryBuilder From(string table)
        {
            m_PrimaryTable = table;
            return this;
        }

        public SqlQueryBuilder Into(string table)
        {
            m_PrimaryTable = table;
            return this;
        }

        public SqlQueryBuilder Where(string field, CompareOperator compareOperator, object conditionValue)
        {
            bool isLikeOperation = false;
            string paramName = "@p" + (m_ParamCount++);
            m_Where.Append(" AND ").Append(field);

            switch (compareOperator)
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
                    m_Where.Append("=");
                    break;
                case CompareOperator.NotEqual:
                    m_Where.Append("!=");
                    break;
                case CompareOperator.Greater:
                    m_Where.Append(">");
                    break;
                case CompareOperator.GreaterOrEqual:
                    m_Where.Append(">=");
                    break;
                case CompareOperator.Less:
                    m_Where.Append("<");
                    break;
                case CompareOperator.LessOrEqual:
                    m_Where.Append("<=");
                    break;
            }

            if (isLikeOperation)
            {
                m_Where.Append(string.Format(" LIKE {0}", paramName));
            }
            else
            {
                m_Where.Append(paramName);
            }
            
            return this;
        }

        public SqlQueryBuilder LeftJoin(string tableName, string srcField, string destField)
        {
            LeftJoinBuilder.AppendFormat(" LEFT JOIN {0} t ON [k].[{1}] = [t].[{2}]", tableName, srcField, destField);

            return this;
        }

        public SqlQueryBuilder Limit(string orderByField, uint from, uint to)
        {
            m_OrderByField = orderByField;
            m_LimitFrom = from;
            m_LimitFrom = to;
            return this;
        }

        public SqlCommand Select(params string[] fieldNames)
        {
            string query = "";
            if (m_LeftJoin == null) // Single table
            {
                var queryBuilder = new StringBuilder();
                queryBuilder.AppendFormat(@"WITH KeyTable AS 
                    (SELECT ROW_NUMBER() Over (ORDER BY {0} As Row, [UID]"
                );
            }


            return null;
        }

        public SqlCommand SelectSingle(ushort modelIndex, params string[] fieldNames)
        {
            ModelDefinition def = GetModelDef(modelIndex);
            ModelMap mapping = DefinitionLoader.GetMapping(modelIndex);
            var command = new SqlCommand();
            SqlParameterCollection sqlParams = command.Parameters;
            string outputClause = fieldNames.ToString(",");

            // outputFieldNames is never null.
            // if outputClause is not empty, prepends a comma ",".
            if (!string.IsNullOrEmpty(outputClause))
            {
                outputClause = "," + outputClause;
            }


            return null;
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
    }
}
