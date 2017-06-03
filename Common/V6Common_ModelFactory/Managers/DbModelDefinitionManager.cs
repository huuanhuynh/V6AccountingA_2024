using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;
using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory.Managers
{
    public class DbModelDefinitionManager : IModelDefinitionManager
    {
        private SqlConnection m_Connection;
        private IList<ModelDefinition> m_ModelDefinitions;
        private IList<ModelMap> m_ModelMaps;


        #region Properties

        private SqlConnection Connection
        {
            get
            {
                if (m_Connection.State != ConnectionState.Open)
                {
                    m_Connection.Open();
                }
                return m_Connection;
            }
        }


        /// <summary>
        ///     See <see cref="IModelDefinitionManager.this[string modelName]"/>
        /// </summary>
        /// <param name="modelName">Model definition name, must not be null.</param>
        public ModelDefinition this[string modelName]
        {
            get
            {
                return Load(modelName);
            }
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionManager.this[int definitionIndex]"/>
        /// </summary>
        /// <param name="definitionIndex">Model definition index.</param>
        public ModelDefinition this[ushort definitionIndex]
        {
            get
            {
                return Load(definitionIndex);
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        ///     Creates new instance of <see cref="DbModelDefinitionManager"/>
        ///     with an existing connection.
        /// </summary>
        /// <param name="connection">Must not be null.</param>
        public DbModelDefinitionManager(SqlConnection connection)
        {
            Guard.ArgumentNotNull(connection, "connection");
            m_Connection = connection;
        }

        /// <summary>
        ///     Creates new instance of <see cref="DbModelDefinitionManager"/>
        ///     with given connection string.
        /// </summary>
        /// <param name="connectionString">Must not be null or empty.</param>
        public DbModelDefinitionManager(string connectionString)
        {
            Guard.ArgumentNotNullOrEmpty(connectionString, "connectionString");
            m_Connection = new SqlConnection(connectionString);
        }

        #endregion


        /// <summary>
        ///     See <see cref="IModelDefinitionManager.AddField"/>
        /// </summary>
        public bool AddField(ushort modelIndex, IList<ModelFieldDefinition> newFields)
        {
            SqlCommand command =
                SqlQueryFactory.CreateAlterCommand(modelIndex, newFields);
            return false;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionManager.Load"/>
        /// </summary>
        /// <param name="modelName">Model definition name, must not be null.</param>
        public ModelDefinition Load(string modelName)
        {
            if (m_ModelDefinitions == null) { return null; }

            ModelDefinition model = 
                m_ModelDefinitions.FirstOrDefault(d => d.Name == modelName);
            return model;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionManager.Load"/>
        /// </summary>
        /// <param name="definitionIndex">Model definition index.</param>
        public ModelDefinition Load(ushort definitionIndex)
        {
            if (m_ModelDefinitions == null ||
                definitionIndex < 0 ||
                definitionIndex >= m_ModelDefinitions.Count
                ) 
            { 
                return null; 
            }
            
            ModelDefinition model = m_ModelDefinitions[definitionIndex];
            return model;
        }
        
        /// <summary>
        ///     See <see cref="IModelDefinitionManager.LoadAll"/>
        /// </summary>
        public IList<ModelDefinition> LoadAll()
        {
            if (m_ModelDefinitions != null)
            {
                return m_ModelDefinitions;
            }

            /* Expected resultset:
                |   Name    |   DefinitionXml                           |   MappingXml
                 alKhachHang   <xsd:schema xmlns:schema="v6schema"....      <mappings><map name="Ma"...
             */

            var command = new SqlCommand(Names.Proc.GetModelDefinitions)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };

            ushort index;
            SqlXml definitionXml, mappingXml;
            string modelName;
            var defList = new List<ModelDefinition>();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            
            m_ModelDefinitions = new List<ModelDefinition>();
            m_ModelMaps = new List<ModelMap>();

            try
            {
                while (reader.Read())
                {
                    index = (ushort)reader.GetInt32(0);
                    modelName = reader.GetString(1);
                    definitionXml = reader.GetSqlXml(2);
                    mappingXml = reader.GetSqlXml(3);

                    ParseDefinitionAndMapping(modelName, definitionXml, mappingXml, 
                        index);
                }
            }
            finally
            {
                reader.Close();
            }

            if (!m_ModelDefinitions.Any())
            {
                m_ModelDefinitions = null;
            }
            if (!m_ModelMaps.Any())
            {
                m_ModelMaps = null;
            }

            return m_ModelDefinitions;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionManager.GetMapping"/>
        /// </summary>
        /// <param name="index">Model definition index.</param>
        public ModelMap GetMapping(ushort index)
        {
            if (m_ModelMaps == null || index < 0 || index >= m_ModelMaps.Count)
            {
                return null;
            }

            ModelMap mapping = m_ModelMaps[index];
            return mapping;
        }

        /// <summary>
        ///     See <see cref="IModelDefinitionManager.GetAllMappings"/>
        /// </summary>
        public IList<ModelMap> GetAllMappings()
        {
            if (m_ModelMaps == null)
            {
                return null;
            }
            return m_ModelMaps.ToList();
        }


        private void ParseDefinitionAndMapping(string modelName,
            SqlXml definitionXml, SqlXml mappingXml, ushort index)
        {
            Task<ModelDefinition> definitionTask =
                ParseModelDefinitionAsync(definitionXml, modelName);
            Task<ModelMap> mapTask =
                ParseModelMapsAsync(mappingXml);

            // Parses definition and mappings in parallel.
            Task.WaitAll(definitionTask, mapTask);
            
            ModelDefinition definition = definitionTask.Result;
            if (definition == null) { return; }
            m_ModelDefinitions.Add(definition);

            ModelMap modelMap = mapTask.Result;
            if (modelMap == null) { return; }

            int i = 0;
            ModelFieldDefinition field = null;
            IList<FieldMapping> fieldMappings = modelMap.FieldMappings;
            try
            {
                //*
                modelMap.NameMapping.DbName = definition.Name;
                definition.Name = modelMap.NameMapping.AppName;
                //definition.DBName = modelMap.NameMapping.DbName;
                foreach (FieldMapping map in fieldMappings)
                {
                    field = definition.Fields[map.DbName];
                    //map = fieldMappings[i];
                    
                    field.Label = map.Label;
                    field.Group = map.Group;
                    field.Name = map.AppName;
                    //map.DbName = field.Name;
                }
                //*/
                definition.Index = index;
                m_ModelMaps.Add(modelMap);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("An error occured when trying to map field ({0}) of definition #{1}", 
                    field , definition.Name),
                    ex);
            }
        }

        private Task<ModelDefinition> ParseModelDefinitionAsync(SqlXml definitionXml,
            string modelName)
        {
            /* Expected XML format:
                 <xsd:complexType xmlns:xsd="http://www.w3.org/2001/XMLSchema">
	                <xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" />
	                <xsd:attribute name="Ma" type="sqltypes:bigint" />
	                <xsd:attribute name="Ten">
		                <xsd:simpleType>
			                <xsd:restriction base="sqltypes:nvarchar" xmlns:p1="http://schemas.microsoft.com/sqlserver/2004/sqltypes" p1:localeId="1033" p1:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" p1:sqlSortId="52">
				                <xsd:maxLength value="50" />
			                </xsd:restriction>
		                </xsd:simpleType>
	                </xsd:attribute>
                 </xsd:complexType>
             */
            return Task.Factory.StartNew<ModelDefinition>(() =>
                {
                    IList<ModelFieldDefinition> fields;
                    using (XmlReader xmlReader = definitionXml.CreateReader())
                    {
                        fields = xmlReader.ParseFieldDefinitions();
                    }
                    if (fields == null) { return null; }

                    var modelDef = new ModelDefinition(modelName)
                    {
                        Fields = new ModelFieldDefinitionList(fields)
                    };
                    return modelDef;
                });
        }

        private Task<ModelMap> ParseModelMapsAsync(SqlXml mappingXml)
        {
            return Task.Factory.StartNew<ModelMap>(() =>
                {
                    using (XmlReader xmlReader = mappingXml.CreateReader())
                    {
                        return xmlReader.ParseModelMap();
                    }
                });
        }
        
        private void LoadAllModelNames()
        {

        }
        
    }
}
