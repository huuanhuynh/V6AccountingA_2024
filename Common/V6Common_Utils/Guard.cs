using System;
using System.Collections.Generic;
using System.Linq;

using V6Soft.Common.Utils.TypeExtensions;


namespace V6Soft.Common.Utils
{
    /// <summary>
    /// The Guard class contains static functions for performing ArgumentNullException and other similar checks.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Checks if the sent in argument is null, if so an NullReferenceException is thrown.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="message">Message to show if the argument is null.</param>
        /// <param name="parameters">Formatting parameters for message</param>
        public static void ValueNotNull(object value, string message, params object[] parameters)
        {
            if (value == null)
            {
                try
                {
                    message = string.Format(message, parameters);
                }
                catch (Exception)
                {
                    throw new FormatException("An error message during null value check cannot be formatted. Original message: " + message);
                }

                throw new NullReferenceException(message);
            }
        }

        /// <summary>
        /// Checks if the sent in argument is null, if so an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.</exception>
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Checks if the sent in argument list is null or empty, if so an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.</exception>
        public static void ArgumentNotNullOrEmpty<T>(IEnumerable<T> argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (!argument.Any())
            {
                throw new ArgumentException(string.Format("{0} cannot be empty", argumentName), argumentName);
            }
        }

        /// <summary>
        /// Checks if the sent in argument string is null or whitespace, if so an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentException">Thrown if argument is null.</exception>
        public static void ArgumentNotNullOrWhiteSpace(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument) || argument.Trim() == string.Empty)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Checks if the sent in argument string is null or empty, if so an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentNullException">Thrown if argument is null.</exception>
        public static void ArgumentNotNullOrEmpty(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Checks if the sent in argument is of given type and throws ArgumentException otherwise
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="type">The type to check.</param>
        /// <exception cref="ArgumentException">Thrown if argument is not of given type.</exception>
        public static void ArgumentOfType(object argument, string argumentName, Type type)
        {
            ArgumentNotNull(type, "type");

            if (!type.IsInstanceOfType(argument))
            {
                throw new ArgumentException(string.Format("Argument {0} is not of type {1}", argumentName, type));
            }
        }

        /// <summary>
        /// Checks if the sent in argument is greater than 0
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if argument is less or equal to 0</exception>
        public static void ArgumentIsPositive(int argument, string argumentName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Argument {0} ({1}) has to be positive", argumentName, argument));
            }
        }

        /// <summary>
        /// Checks if the sent in argument is greater or equal to 0
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if argument is less than 0</exception>
        public static void ArgumentIsNotNegative(int argument, string argumentName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(string.Format("Argument {0} ({1}) has to not negative", argumentName, argument));
            }
        }

        /// <summary>
        /// Checks if the sent in argument is greater or equal to lower bound and lesser or equal to upper bound
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="lowerBound">Lower bound that the argument should be greater or equal to.</param>
        /// <param name="upperBound">Upper bound that the argument should be lesser or equal to.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <exception cref="ArgumentException">Thrown if argument is not within the bounds</exception>
        public static void ArgumentIsInRange(int argument, int lowerBound, int upperBound, string argumentName)
        {
            if (argument < lowerBound || argument > upperBound)
            {
                throw new ArgumentOutOfRangeException(string.Format("Argument {0} with value {1} is out of range [{2};{3}]", argumentName, argument, lowerBound, upperBound));
            }
        }
       
        /// <summary>
        ///     Checks if the specified argument is of numeric type.
        /// </summary>
        /// <param name="argument">The value to be checked</param>
        /// <param name="argumentName">The argument name which is included in exception message.</param>
        public static void ArgumentIsNumeric(object argument, string argumentName)
        {
            if (argument == null || !argument.GetType().IsNumeric())
            {
                throw new ArgumentException(string.Format("Argument {0} must be numeri types.", argumentName));
            }
        }

        /// <summary>
        ///     Checks if the specified type is one of numeric types.
        /// </summary>
        /// <typeparam name="TArg">Type to be checked</typeparam>
        public static void ArgumentTypeIsNumeric<TArg>()
        {
            if (!typeof(TArg).IsNumeric())
            {
                throw new ArgumentException(string.Format("Argument type must be numeric type."));
            }
        }
    }
}
