using System.Collections.Generic;


namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents a model mapping collection for a definition.
    /// </summary>
    public class ModelMap
    {
        /// <summary>
        ///     Gets or internally sets model mapping.
        /// </summary>
        public NameMapping NameMapping { get; internal set; }

        /// <summary>
        ///     Gets or internally sets field groups
        /// </summary>
        public string[] FieldGroups { get; internal set; }

        /// <summary>
        ///     Gets or internally sets list of field mappings.
        /// </summary>
        public IList<FieldMapping> FieldMappings
        {
            get
            {
                return m_FieldMappings;
            }
            internal set
            {
                m_FieldMappings = value;
                if (m_FieldMappings != null)
                {
                    //TODO: Should generate new group list
                    GenerateQuickMapping();
                }
            }
        }

        private IList<FieldMapping> m_FieldMappings;
        private IDictionary<string, string> m_NameMappingCache;

        public ModelMap()
        {
        }

        public ModelMap(NameMapping nameMapping, IList<FieldMapping> fieldMappings,
            string[] fieldGroups)
        {
            NameMapping = nameMapping;
            FieldMappings = fieldMappings;
            FieldGroups = fieldGroups;
        }

        /// <summary>
        ///     Get in-db name based on specified in-app name, or vice versa.
        /// </summary>
        /// <param name="name">Database name or application name</param>
        public string MapName(string name)
        {
            if (!m_NameMappingCache.ContainsKey(name))
            {
                return null;
            }
            return m_NameMappingCache[name];
        }
        
        private void GenerateQuickMapping()
        {
            if (m_NameMappingCache == null)
            {
                m_NameMappingCache = new Dictionary<string, string>(m_FieldMappings.Count);
            }
            else
            {
                m_NameMappingCache.Clear();
            }

            foreach (var mapping in m_FieldMappings)
            {
                // Only caches mapping on service-side where in-app name is available
                // Do not do this on web-server-side.
                if (!string.IsNullOrEmpty(mapping.DbName))
                {
                    m_NameMappingCache[mapping.AppName] = mapping.DbName;
                    m_NameMappingCache[mapping.DbName] = mapping.AppName;
                }
            }
        }
    }
}
