using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace V6Tools
{
    public class V6DllLoader
    {
        private Assembly DLL;
        private Type _type;
        public V6DllLoader(string file)
        {
            DLL = Assembly.LoadFile(file);

            foreach (Type type in DLL.GetExportedTypes())
            {
                var c = Activator.CreateInstance(type);
                type.InvokeMember("Output", BindingFlags.InvokeMethod, null, c, new object[] { @"Hello" });
            }

            Console.ReadLine();
        }
        
        public object CallInvoke(string methodName, IDictionary<string, object> All_Objects)
        {
            if (_type == null) return null;
            All_Objects["All_Objects"] = All_Objects;
            var method = _type.GetMethod(methodName);
            if (method != null)
            {
                var parameters = method.GetParameters();
                var listObj = new List<object>();
                foreach (ParameterInfo info in parameters)
                {
                    if (All_Objects.ContainsKey(info.Name))
                    {
                        listObj.Add(All_Objects[info.Name]);
                    }
                    else
                    {
                        listObj.Add(null);
                    }
                }
                return method.Invoke(null, listObj.ToArray());
            }
            return null;
        }
    }
}
