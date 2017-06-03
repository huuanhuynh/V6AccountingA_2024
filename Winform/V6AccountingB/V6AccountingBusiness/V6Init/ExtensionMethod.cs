using System;
using System.Collections.Generic;
using System.Reflection;
using V6Tools.V6Convert;

namespace V6Init
{
    public static class ExtensionMethod
    {
        /// <summary>
        /// Cắt chuỗi lấy phần bên trái.
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên trái với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Left(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(0, length);
        }
        /// <summary>
        /// Cắt chuỗi lấy phần bên phải
        /// </summary>
        /// <param name="str">Chuỗi nguồn</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Phần chuỗi bên phải với độ dài cần lấy, hoặc ngắn hơn nếu nguồn không đủ dài.</returns>
        public static string Right(this string str, int length)
        {
            return length >= str.Length ? str : str.Substring(str.Length - length, length);
        }
        
        /// <summary>
        /// Hàm mở rộng. Add source to target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                try
                {
                    target.Add(element);
                }
                catch
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target">Đích</param>
        /// <param name="source">Nguồn</param>
        /// <param name="overwrite">Lấy dữ liệu mới nếu trùng khóa</param>
        public static void AddRange(this IDictionary<string, object> target, IDictionary<string, object> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            if (overwrite)
            {
                foreach (KeyValuePair<string, object> item in source)
                {
                    target[item.Key] = item.Value;
                }
            }
            else foreach (var element in source)
            {
                if (!target.ContainsKey(element.Key))
                {
                    target.Add(element.Key, element.Value);
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="overwrite"></param>
        public static void AddRange(this IDictionary<string, object> target, IDictionary<string, string> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                if (target.ContainsKey(element.Key))
                {
                    if (overwrite)
                    {
                        target[element.Key] = element.Value;
                    }
                }
                else
                {
                    target.Add(element.Key, element.Value);
                }
            }
        }

        /// <summary>
        /// Add source to target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="overwrite"></param>
        public static void AddRange(this IDictionary<string, string> target, IDictionary<string, object> source, bool overwrite = false)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
            {
                if (target.ContainsKey(element.Key))
                {
                    if (overwrite)
                    {
                        target[element.Key] = element.Value.ToString();
                    }
                }
                else
                {
                    target.Add(element.Key, ObjectAndString.ObjectToString(element.Value));
                }
            }
        }

        /// <summary>
        /// So sánh 2 đối tượng theo các thuộc tính và giá trị.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool ReflectiveEquals(this object first, object second)
        {
            if (first == null && second == null)
            {
                return true;
            }
            if (first == null || second == null)
            {
                return false;
            }
            Type firstType = first.GetType();
            if (second.GetType() != firstType)
            {
                return false; // Or throw an exception
            }
            // This will only use public properties. Is that enough?
            foreach (PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object firstValue = propertyInfo.GetValue(first, null);
                    object secondValue = propertyInfo.GetValue(second, null);
                    if (!Equals(firstValue, secondValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
