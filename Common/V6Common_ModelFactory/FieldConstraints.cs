using System;

using V6Soft.Common.Utils;


namespace V6Soft.Common.ModelFactory
{
    public interface IFieldConstraint
    {
        /// <summary>
        ///     Checks if the specified <paramref name="fieldValue"/> meets
        ///     constraint.
        /// </summary>
        bool Validate(object fieldValue);
    }


    /// <summary>
    ///     Restricts an object field value not to be null.
    /// </summary>
    public class NotNullFieldConstraint : IFieldConstraint
    {
        /// <summary>
        ///     See <see cref="IFieldConstraint.Validate"/>
        /// </summary>
        public bool Validate(object fieldValue)
        {
            // Cast to an object instance, in case <T> is a struct or primative type.
            object obj = fieldValue as object;
            return (obj != null);
        }
    }
        
    /// <summary>
    ///     Restricts a string field value's length not to be out of specified range.
    /// </summary>
    public class LengthConstraint : IFieldConstraint
    {
        /// <summary>
        ///     Gets minimum length constraint.
        /// </summary>
        public int MinLength { get; private set; }

        /// <summary>
        ///     Gets maximum length constraint.
        /// </summary>
        public int MaxLength { get; private set; }
        
        
        /// <summary>
        ///     Creates new instance of LengthConstraint.
        /// </summary>
        /// <param name="maxLength">
        ///     Maximum length. Negative value means disabling maximum length check.
        /// </param>
        public LengthConstraint(int maxLength)
            : this(-1, maxLength)
        {
        }

        /// <summary>
        ///     Creates new instance of LengthConstraint.
        /// </summary>
        /// <param name="minLength">
        ///     Minimum length. Negative value means disabling minimum length check.
        /// </param>
        /// <param name="maxLength">
        ///     Maximum length. Negative value means disabling maximum length check.
        /// </param>
        public LengthConstraint(int minLength, int maxLength = -1)
        {
            if (maxLength == 0)
            {
                throw new ArgumentException("Max length must be other than 0.");
            }

            MinLength = minLength;
            MaxLength = maxLength;
        }

        /// <summary>
        ///     See <see cref="IFieldConstraint.Validate"/>
        /// </summary>
        public bool Validate(object fieldValue)
        {
            Guard.ArgumentOfType(fieldValue, "fieldValue", typeof(string));

            string strValue = fieldValue as string;
            if (strValue == null) { return false; }

            bool meetMinLength = (MinLength <= 0 || strValue.Length >= MinLength);
            bool meetMaxLength = (MaxLength < 0 || strValue.Length <= MaxLength);

            return (meetMinLength && meetMaxLength);
        }

        public override string ToString()
        {
            return string.Format("MinLength={0}, MaxLength={1}", MinLength, MaxLength);
        }
    }
}
