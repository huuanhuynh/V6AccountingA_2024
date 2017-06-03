using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;

using V6Soft.Services.Common.Infrastructure;


namespace V6Soft.Services.Wcf.Common.Attributes
{
    /// <summary>
    /// Provides utility extesion methods to serialize objects to strings,
    /// which are used for logging.
    /// </summary>
    public static class Extensions
    {
        private static Predicate<string> s_IsLoggable = ((item) => true);

        /// <summary>
        /// Stringify an object so that it can be concatenated with a pattern string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StringifyForPattern(this object value)
        {
            return value.Stringify(isLoggable: s_IsLoggable)
                .Replace("{", "{{").Replace("}", "}}");
        }

        /// <summary>
        /// A wrapper for MtpLogger.LogInfo() + string.Format().
        /// Caller can also issue concise calls like this:
        /// <code>"UserOId = {0}".Log(userOId)</code>
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="args"></param>
        public static void Log(this string pattern, params object[] args)
        {
            AppContext.Logger.LogInfo(string.Format(pattern, args));
        }

        /// <summary>
        /// Converts a string-string dictionary into a NameValueCollection object. 
        /// Always returns a non-null object.
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(
            this Dictionary<string, string> results)
        {
            var nameValueCollection = new NameValueCollection();
            foreach (KeyValuePair<string, string> keyValuePair in results)
            {
                nameValueCollection.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return nameValueCollection;
        }

        /// <summary>
        /// Return a simple string describing the object using these rules:
        /// <para>- If the object is null, return 'null'</para>
        /// <para>- If the object has no public fields/properties, return '{}'</para>
        /// <para>- Otherwise, return '{Member:Value, Member: Value, ...}' where: </para>        
        /// <para>-- null members become 'null'</para>
        /// <para>-- primitive members become 'their-value'</para>
        /// <para>-- array/collection members become 'array&lt;type>'</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="recursiveDepth"></param>
        /// <param name="usesClassFullName"></param>
        /// <param name="builder"></param>
        /// <param name="maxStringLen">If a string property/field is longer than this length,
        ///     it will be trimmed to this length and the trimmed part will be replaced 
        ///     by '...'.
        /// </param>
        /// <param name="isLoggable"></param>
        /// <returns></returns>
        public static string Stringify(this object obj, int recursiveDepth = 2, 
            bool usesClassFullName = false, StringBuilder builder = null, 
            int maxStringLen = 30, Predicate<string> isLoggable = null)
        {
            if (obj == null)
            {
                if (builder != null)
                {
                    builder.Append("null");
                }
                return "null";
            }

            if (builder == null)
            {
                builder = new StringBuilder();
            }

            Type type = obj.GetType();

            if (StringifyPrimitiveStringDatetimeMember(type, obj, maxStringLen, builder))
            {
                return builder.ToString();
            }

            builder.Append("{");

            PropertyInfo[] properties = type.GetProperties();
            int count = properties.Length;
            bool hasLoggedMember = false;

            if (count > 0)
            {
                PropertyInfo pi;
                for (int index = 0; index < count; index++)
                {
                    pi = properties[index];
                    if (isLoggable != null && isLoggable(pi.Name))
                    {
                        if (hasLoggedMember) { builder.Append(", "); }
                        else { hasLoggedMember = true; }

                        StringifyMember(obj, recursiveDepth, usesClassFullName, builder, pi, 
                            maxStringLen, isLoggable);
                    }
                }
            }

            FieldInfo[] fields = type.GetFields();
            count = fields.Length;
            if (count > 0)
            {
                FieldInfo fi;
                for (int index = 0; index < count; index++)
                {
                    fi = fields[index];
                    if (isLoggable != null && isLoggable(fi.Name))
                    {
                        if (hasLoggedMember) { builder.Append(", "); }
                        else { hasLoggedMember = true; }

                        StringifyMember(obj, recursiveDepth, usesClassFullName, builder, fi, 
                            maxStringLen, isLoggable);
                    }
                }
            }

            builder.Append("}");

            return builder.ToString();
        }

        private static void StringifyMember(object obj, int recursiveDepth, 
            bool usesClassFullName, StringBuilder builder, PropertyInfo pi, 
            int maxStringLen, Predicate<string> isLoggable = null)
        {
            if (!pi.CanRead) { return; }

            builder.Append(pi.Name).Append(": ");
            if (pi.GetIndexParameters().Length > 0)
            {
                builder.Append("<index>");
                return;
            }

            Type memberType = pi.PropertyType;
            object memberValue = pi.GetValue(obj, null);
            StringifyMemberValuePart(recursiveDepth, usesClassFullName, builder, memberType, 
                memberValue, maxStringLen, isLoggable);
        }

        private static void StringifyMember(object obj, int recursiveDepth, 
            bool usesClassFullName, StringBuilder builder, FieldInfo pi, int maxStringLen, 
            Predicate<string> isLoggable = null)
        {
            builder.Append(pi.Name).Append(": ");

            Type memberType = pi.FieldType;
            object memberValue = pi.GetValue(obj);

            StringifyMemberValuePart(recursiveDepth, usesClassFullName, builder, memberType, 
                memberValue, maxStringLen, isLoggable);
        }

        private static void StringifyMemberValuePart(int recursiveDepth, 
            bool usesClassFullName, StringBuilder builder, Type memberType, 
            object memberValue, int maxStringLen, Predicate<string> isLoggable = null)
        {
            if (recursiveDepth < 1)
            {
                builder.Append(memberType.Name);
                return;
            }

            if (memberValue == null)
            {
                builder.Append("null");
                return;
            }

            if (StringifyPrimitiveStringDatetimeMember(memberType, memberValue, maxStringLen,
                builder))
            {
                return;
            }

            if (memberType == typeof(Nullable<>))
            {
                Type encompassedType = memberType.GetGenericArguments()[0];

                if (!StringifyPrimitiveStringDatetimeMember(encompassedType, memberValue, 
                    maxStringLen, builder))
                {
                    builder.Append(memberValue.ToString());
                }
            }
            else if (memberType.IsArray)
            {
                builder.Append("array<").Append(memberType.GetElementType().Name);
                PropertyInfo lengthProperty = memberType.GetProperty("Length");
                builder
                    .Append("-").Append(lengthProperty.GetValue(memberValue, null))
                    .Append(">");
            }
            else if (memberType.GetInterfaces()
                .FirstOrDefault(i => i.Name.StartsWith("ICollection")) != null)
            {
                builder.Append("collection");
            }
            else
            {
                memberValue.Stringify(recursiveDepth - 1, usesClassFullName, builder, 
                    maxStringLen, isLoggable);
            }
        }

        private static string Trim(string value, int maxLen)
        {
            if (maxLen > 0 && value.Length > maxLen)
            {
                return value.Substring(0, maxLen) + "...";
            }
            return value;
        }

        /// <summary>
        /// Returns true if the supplied member is of primitive/string/datetime type. 
        /// In that case, the value is also stringified and appended to the builder.
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberValue"></param>
        /// <param name="maxStringLen"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static bool StringifyPrimitiveStringDatetimeMember(Type memberType, 
            object memberValue, int maxStringLen, StringBuilder builder)
        {
            if (memberType.IsPrimitive || memberType.IsEnum)
            {
                builder.Append(memberValue.ToString());
                return true;
            }
            if (memberType == typeof(string))
            {
                builder.Append("'")
                    .Append(Trim(memberValue.ToString(), maxStringLen))
                    .Append("'");
                return true;
            }
            if (memberType == typeof(DateTime) || memberType == typeof(DateTimeOffset))
            {
                builder.Append("'").Append(memberValue.ToString()).Append("'");
                return true;
            }

            return false;
        }
    }
}
