using System;

namespace V6Soft.Common.ModelFactory
{
    /// <summary>
    ///     Represents an error thrown when XML model definition
    ///     does not match a required constraint.
    /// </summary>
    public class MalformedDefinitionException : Exception
    {
        public MalformedDefinitionException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     Represents an error thrown when no definition is found for
    ///     specified definition index.
    /// </summary>
    public class NoDefinitionException : Exception
    {
        /// <summary>
        ///     Creates new instance of <see cref="NoDefinitionException"/>
        /// </summary>
        /// <param name="index">Definition Index</param>
        /// <param name="isModel">
        ///     If true, generates No Model Definition exception.
        ///     If false, generates No Field Definition exception.
        /// </param>
        public NoDefinitionException(ushort index, bool isModel = true)
            : base(string.Format("No definition for {0} with index/name #{1}.", 
                isModel ? "model" : "field", index))
        {
        }
    }

    /// <summary>
    ///     Represents an error thrown when working with
    ///     <see cref="V6Soft.Common.ModelFactory.RuntimeModel"/>
    /// </summary>
    public class RuntimeModelException : Exception
    {
        public RuntimeModelException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     Represents an error thrown when working with
    ///     <see cref="V6Soft.Common.ModelFactory.DynamicModel"/>
    /// </summary>
    public class DynamicModelException : Exception
    {
        public DynamicModelException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    ///     Represents an error thrown when runtime model values
    ///     do not satisfy the constraints of field definition.
    /// </summary>
    public class ConstraintViolationException : Exception
    {
        public ConstraintViolationException()
        {

        }

        public ConstraintViolationException(string message)
            : base(message)
        {
        }
    }
}
