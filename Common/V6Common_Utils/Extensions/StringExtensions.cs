using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace V6Soft.Common.Utils.StringExtensions
{
    public static class StringExtensions
    {
        public static string ToString<T>(this IEnumerable<T> collection, string delimiter = "")
        {
            if (collection == null) { return null; }
            if (!collection.Any()) { return string.Empty; }
            
            var resultBuilder = new StringBuilder();
            foreach (var item in collection)
            {
                if (item != null)
                {
                    resultBuilder.Append(item.ToString());
                    if (!string.IsNullOrEmpty(delimiter))
                    {
                        resultBuilder.Append(delimiter);
                    }
                }
            }

            if (!string.IsNullOrEmpty(delimiter))
            {
                int delLen = delimiter.Length;
                resultBuilder.Remove(resultBuilder.Length - delLen, delLen);
            }

            return resultBuilder.ToString();
        }
    }
}
