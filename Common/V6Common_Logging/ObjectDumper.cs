using System;
using System.Collections;
using System.Reflection;
using System.Text;



namespace V6Soft.Common.Logging
{
    /// <summary>   
    /// This class dumps an object to a string. 
    /// http://stackoverflow.com/questions/852181/c-printing-all-properties-of-an-object
    /// c:/Program Files/Microsoft Visual Studio 9.0/Samples/1033/CSharpSamples.zip
    /// </summary>
    public class ObjectDumper
    {
        private readonly int m_MaxLevel;
        private int m_Level;
        private readonly int m_IndentSize;
        private readonly StringBuilder m_StringBuilder;

        private ObjectDumper(int maxLevel, int indentSize)
        {
            m_MaxLevel = maxLevel;
            m_IndentSize = indentSize;
            m_StringBuilder = new StringBuilder();
        }

        /// <summary>
        /// Dumps an object.
        /// </summary>
        /// <param name="element">The element to dump.</param>
        public static string Dump(object element)
        {
            return Dump(element, 3, 2);
        }

        /// <summary>
        /// Dumps an object with an indent size.
        /// </summary>
        /// <param name="element">The element to dump.</param>
        /// <param name="indentSize">Size of the indent.</param>
        /// <param name="maxLevel">Maximum level to dig in.</param>
        public static string Dump(object element, int maxLevel, int indentSize)
        {
            var instance = new ObjectDumper(maxLevel, indentSize);
            return instance.DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (m_Level >= m_MaxLevel)
            {
                return string.Empty;
            }

            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    Write("{{{0}}}", objectType.FullName);
                    m_Level++;
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            m_Level++;
                            DumpElement(item);
                            m_Level--;
                        }
                        else
                        {
                            DumpElement(item);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;

                        try
                        {
                            object value = fieldInfo != null ? fieldInfo.GetValue(element) : propertyInfo.GetValue(element, null);

                            if (type.IsValueType || type == typeof(string))
                            {
                                Write("{0}: {1}", memberInfo.Name, FormatValue(value));
                            }
                            else
                            {
                                Write("{0}: {1}", memberInfo.Name, typeof(IEnumerable).IsAssignableFrom(type) ? "..." : "{ }");
                                m_Level++;
                                DumpElement(value);
                                m_Level--;
                            }
                        }
                        catch (Exception exception)
                        {
                            Write("{0}: Exception during getting property - {1}", memberInfo.Name, exception.Message);
                        }
                    }
                }

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    m_Level--;
                }
            }

            return m_StringBuilder.ToString();
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', m_Level * m_IndentSize);

            if (args != null)
                value = string.Format(value, args);

            m_StringBuilder.AppendLine(space + value);
        }

        private string FormatValue(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime)o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\"", o);

            if (o is ValueType)
                return (o.ToString());

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}
