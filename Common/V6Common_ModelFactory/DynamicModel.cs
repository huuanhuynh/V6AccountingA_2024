using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.ModelFactory.Factories;

namespace V6Soft.Common.ModelFactory
{
    public class DynamicModel : DynamicObject
    {
        /// <summary>
        ///     Gets or sets model's definition index.
        /// </summary>
        public ushort DefinitionIndex { get; set; }

        /// <summary>
        ///     Gets or sets model definition.
        ///     <para/>Warning: Setting new definition will ERASE all field values.
        /// </summary>
        /*
        protected ModelDefinition m_Definition;
         * 
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
                m_Fields = new Dictionary<string, object>(len);
            }
        }
        //*/

        /// <summary>
        ///     Gets a copy of field array.
        /// </summary>
        public object[] Fields
        {
            get
            {
                if (m_Fields == null) { return null; }
                return m_Fields.Values.ToArray();
            }
        }

        /// <summary>
        ///     Gets a copy of field array.
        /// </summary>
        public IDictionary<string, object> FieldMap
        {
            get
            {
                return m_Fields;
            }
            set
            {
                m_Fields = value;
            }
        }

        public object this[string fieldName]
        {
            get
            {
                return Field(fieldName);
            }

            set
            {
                Field(fieldName, value);
            }
        }

        public int Count { get { return m_Fields.Keys.Count; } }


        protected IDictionary<string, object> m_Fields;


        public DynamicModel()
        {
            m_Fields = new Dictionary<string, object>();
        }

        public DynamicModel(DynamicModel otherModel)
        {
            DefinitionIndex = otherModel.DefinitionIndex;
            m_Fields = otherModel.m_Fields;
        }
        
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetMember(binder.Name, out result);
        }

        public bool TryGetMember(string name, out object result)
        {
            if (m_Fields.ContainsKey(name))
            {
                result = m_Fields[name];
                return true;
            }
            result = null;
            return false;
        }

        public object Field(string name)
        {
            if (!m_Fields.ContainsKey(name))
            {
                throw new DynamicModelException("There is no field named " + name);
            }
            return m_Fields[name];
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetMember(binder.Name, value);
        }

        public bool TrySetMember(string name, object value)
        {
            /*
            if (!m_Fields.ContainsKey(name)) // TODO: Should look up with Definition.HasField
            {
                m_Fields.Add(name, value);
            }
            else
            {
                m_Fields[name] = value;
            }
            */
            try
            {
                Field(name, value);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Field(string name, object value)
        {
            ModelDefinition def =
                DynamicModelFactory.DefinitionLoader.Load(DefinitionIndex);

            ModelFieldDefinition fieldDef = 
                def.Fields[name]; // Gets field definition by field name.
                        
            if (!def.Fields.HasField(name))
            {
                throw new DynamicModelException("There is no field named " + name);
            }

            // Converts field value to appropriate type before storing.
            value = (value == null)
                ? GetDefault(fieldDef.Type) // TODO: It should be `null`, but changed based on An's requirement
                : Convert.ChangeType(value, fieldDef.Type);
            m_Fields[name] = value;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, 
		    out object result)
        {
            if (m_Fields.ContainsKey(binder.Name) && 
			    m_Fields[binder.Name] is Delegate)
            {
                var del = m_Fields[binder.Name] as Delegate;
                result = del.DynamicInvoke(args);
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryDeleteMember(DeleteMemberBinder binder)
        {
            if (m_Fields.ContainsKey(binder.Name))
            {
                m_Fields.Remove(binder.Name);
                return true;
            }

            return base.TryDeleteMember(binder);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (string name in m_Fields.Keys)
                yield return name;
        }

        [Obsolete("Use SetMember(string, object)")]
        public void SetField(byte index, object value)
        {
        }

        [Obsolete("Use SetAllFields(IDictionary<string, string>)")]
        public void SetAllFields(object[] values)
        {
        }

        public void SetFields(IDictionary<string, object> fieldValues)
        {
            ModelDefinition def =
                RuntimeModelFactory.DefinitionLoader.Load(DefinitionIndex);

            if (fieldValues == null || !fieldValues.Any()) { return; }
            if (m_Fields == null)
            {
                m_Fields = new Dictionary<string, object>();
            }

            string fieldName;
            foreach(var field in fieldValues)
            {
                fieldName = field.Key;

                // Will throw exception if field name does not exist.
                Field(fieldName, field.Value);
            }
        }

        [Obsolete("Use GetMember(string)")]
        public object GetField(byte index)
        {
            return new object();
        }

        [Obsolete("Use this.FieldMap")]
        public IDictionary<string, object> ToDictionary()
        {
            return m_Fields;
        }

        private object GetDefault(Type type)
        {
            if (type.Equals(typeof(DateTime)))
            {
                return "1900/1/1";
            }
            else if (type.Equals(typeof(string)))
            {
                return string.Empty;
            } 
            else if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
