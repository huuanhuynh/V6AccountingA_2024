using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;


namespace V6Soft.Web.Common.JsonExtensions
{
    public static class JsonExtensions
    {
        private const string KeyValuePattern = "{0}.{1} = {2};";

        /// <summary>
        ///     Serializes all constant field of a class to JSON string.
        /// </summary>
        /// <param name="scope">JavaScript object to add JSON to.</param>
        /// <param name="filter">Action to do with each property.</param>
        public static string GetJsonFromConstants(this Type type, string scope = "window",
            Func<object, object> filter = null)
        {
            var constants = type
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .ToDictionary(
                    x => x.Name,
                    x => 
                    {
                        if (filter != null)
                        {
                            return filter(x.GetValue(null));
                        }
                        return x.GetValue(null);
                    });
            return String.Format(KeyValuePattern, scope, type.Name,
                new JavaScriptSerializer().Serialize(constants));
        }

        /// <summary>
        ///     Serializes all public properties of an object instance to JSON string.
        /// </summary>
        /// <param name="target">Object to get properties.</param>
        /// <param name="name">JavaScript variable name to store the JSON.</param>
        /// <param name="scope">JavaScript object to add JSON to.</param>
        public static string GetJsonFromProperties<T>(this T target, string name, string scope = "window")
        {
            Type type = target.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var properties = type
                .GetProperties(flags)
                .ToDictionary(x => x.Name, x => x.GetValue(target, null));
            return String.Format(KeyValuePattern, scope, name,
                new JavaScriptSerializer().Serialize(properties));
        }

        /// <summary>
        ///     Serializes an enum to JSON string.
        /// </summary>
        /// <param name="scope">JavaScript object to add JSON to.</param>
        public static string GetJsonFromEnum(this Type type, string scope = "window")
        {
            Array values = System.Enum.GetValues(type);
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var val in values)
            {
                string name = Enum.GetName(type, val);
                dict.Add(name, Enum.Format(type, val, "D"));
            }

            return String.Format(KeyValuePattern, scope, type.Name,
                new JavaScriptSerializer().Serialize(dict));
        }

    }
}
