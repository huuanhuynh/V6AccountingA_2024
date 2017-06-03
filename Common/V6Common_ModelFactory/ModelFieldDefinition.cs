using System;
using System.Collections.Generic;


namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents a field of a runtime model.
    /// </summary>
    public class ModelFieldDefinition
    {
        /// <summary>
        ///     Gets or internally sets label that is used to map a localized string.
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        ///     Gets or internally sets field name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Gets or internally sets field type.
        /// </summary>
        public Type Type { get; internal set; }

        /// <summary>
        ///     Gets or internally sets field group.
        /// </summary>
        public string Group { get; internal set; }

        /// <summary>
        ///     Gets or internally sets list of constraints applied to this field.
        ///     This property is never null.
        /// </summary>
        public IList<IFieldConstraint> Constraints { get; internal set; }


        /// <summary>
        ///     Creates new instance of <see cref="ModelFieldDefinition"/>
        ///     with an empty Constraints list.
        /// </summary>
        public ModelFieldDefinition()
        {
            Constraints = new List<IFieldConstraint>();
        }

        /// <summary>
        ///     Creates a new instance of type <see cref="ModelFieldDefinition"/>.
        /// </summary>
        public ModelFieldDefinition(string label, string name, Type type, string group,
            IList<IFieldConstraint> constraints = null)
        {
            Label = label;
            Name = name;
            Type = type;
            Group = group;
            Constraints = constraints ?? new List<IFieldConstraint>();
        }


        public override string ToString()
        {
            return string.Format("Name={0}, Type={1}, {2} constraint(s), Label={3}, Group={4}",
                Name, Type.Name, Constraints.Count, Label, Group);
        }
    }
}
