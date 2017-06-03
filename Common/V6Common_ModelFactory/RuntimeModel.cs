using System;
using System.Collections.Generic;
using System.Linq;

using V6Soft.Common.ModelFactory.Factories;


namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Acts like a class instance.
    /// </summary>
    public class RuntimeModel
    {       
        /// <summary>
        ///     Gets array of fields.
        /// </summary>
        protected object[] m_Fields;

        protected ModelDefinition m_Definition;

                 
        /// <summary>
        ///     Gets or sets model definition.
        ///     <para/>Warning: Setting new definition will ERASE all field values.
        /// </summary>
        public ModelDefinition Definition
        {
            get
            {
                return m_Definition;
            }
            set
            {
                m_Definition = value;
                int len = m_Definition.Fields.Count;
                m_Fields = new object[len];
            }
        }

        /// <summary>
        ///     Gets a copy of field array.
        /// </summary>
        public object[] Fields
        {
            get
            {
                if (m_Fields == null) { return null; }
                return m_Fields.ToArray();
            }
        }

        internal RuntimeModel()
        {
        }

        /// <summary>
        ///     Creates new instance of <see cref="RuntimeModel"/>.
        /// </summary>
        internal RuntimeModel(int fieldCount)
        {
            m_Fields = new object[fieldCount];
        }

        /// <summary>
        ///     Creates new instance of <see cref="RuntimeModel"/>,
        ///     then copies fields value from <paramref name="otherModel"/>
        /// </summary>
        /// <param name="otherModel"></param>
        public RuntimeModel(RuntimeModel otherModel)
        {
            if (otherModel != null)
            {
                m_Definition = otherModel.m_Definition;
                SetAllFields(otherModel.Fields);
            }
        }

        /// <summary>
        ///     Gets value of the field at specified index.
        /// </summary>
        public object GetField(byte index)
        {
            return m_Fields[index];
        }

        /// <summary>
        ///     Sets value for the field at specified index.
        /// </summary>
        public void SetField(byte index, object value)
        {
            ModelDefinition def = 
                RuntimeModelFactory.DefinitionLoader.Load(Definition.Index);
            ModelFieldDefinition fieldDef = def.Fields[index];
            try
            {
                m_Fields[index] = (value == null)
                    ? null
                    : Convert.ChangeType(value, fieldDef.Type);
            }
            catch (InvalidCastException iex)
            {
                var message = string.Format("Data type mismatched for field {0}. " +
                     "Expects: {1}. Actual: {2}",
                     fieldDef.Name,
                    fieldDef.Type.FullName,
                    (value != null ? value.GetType().FullName : "null"));
                throw new RuntimeModelException(message);
            }
        }

        /// <summary>
        ///     Sets values for all fields.
        /// </summary>
        public void SetAllFields(object[] values)
        {
            ModelDefinition def =
                RuntimeModelFactory.DefinitionLoader.Load(Definition.Index);
            var length = def.Fields.Count;

            if (values == null || values.Length < length) { return; }
            if (m_Fields == null)
            {
                m_Fields = new object[length];
            }

            for (var index = 0; index < length; index++)
            {
                var value = values[index];
                ModelFieldDefinition fieldDef = def.Fields[index];
                m_Fields[index] = (value == null)
                    ? null
                    : Convert.ChangeType(value, fieldDef.Type);
            }
        }
        
        public override string ToString()
        {
            if (m_Fields != null)
            {
                return string.Format("Name={0}, {1} field(s)",
                    Definition.Name, m_Fields.Length);
            }
            
            return string.Format("Def Index={0}", Definition);
        }
        
        public IDictionary<byte, object> ToDictionary()
        {
            var dic = new Dictionary<byte, object>();
            for (byte i = 0; i < m_Fields.Length; i++)
            {
                if (m_Fields[i] != null)
                {
                    dic.Add(i, m_Fields[i]);
                }
            }
            return dic;
        }
    }
}
