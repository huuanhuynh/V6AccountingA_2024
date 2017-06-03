using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using V6Soft.Common.Utils;
using V6Soft.Common.Utils.Linq;

namespace V6Soft.Accounting.Common.Dealers
{
    public abstract class DataDealerBase
    {
        protected Dictionary<string, Func<object, object>> m_StandardFunc = new Dictionary<string, Func<object, object>>()
        {
            //{ "TenKhachHang", (value) => VnCodec.TCVNtoUNICODE(value+"")} // Handled in base class
        };

        protected readonly DummyQueryProvider m_QueryProvider;

        public DataDealerBase(IQueryable query)
        {

            m_QueryProvider = new DummyQueryProvider(query);
            HandleQueryProviderEvents();
        }

        /// <summary>
        ///     See <see cref="IODataFriendly.Standardize"/>
        /// </summary>
        public void StandardizeMany(IEnumerable objSequence)
        {
            if (null == objSequence) { return; }
            foreach (var obj in objSequence)
            {
                Standardize(obj);
            }
        }
        
        /// <summary>
        ///     See <see cref="IODataFriendly.Standardize"/>
        /// </summary>
        public void Standardize(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            if (null == properties || properties.Length == 0) { return; }

            Func<object, object> standizeFunc;
            object value;
            foreach (var prop in properties)
            {
                StandizeToUnicode(prop, obj);

                // If there's no standize function for this property, let it go!
                if (m_StandardFunc.TryGetValue(prop.Name, out standizeFunc))
                {
                    value = prop.GetValue(obj);
                    if (null != value)
                    {
                        prop.SetValue(obj, standizeFunc(value));
                    }
                }

                StandardizeProperty(prop, obj);
            }
        }
        
        protected void StandizeToUnicode(PropertyInfo property, object instance)
        {
            // If this prop type is `string`
            if (typeof(string).Equals(property.PropertyType))
            {
                string value = (string)property.GetValue(instance);
                if (string.IsNullOrWhiteSpace(value)) { return; }

                property.SetValue(instance, VnCodec.TCVNtoUNICODE(value.TrimEnd()));
            }
        }

        /// <summary>
        ///     Further standardizes a property, for deriving classes to override if needed.
        /// </summary>
        protected virtual void StandardizeProperty(PropertyInfo property, object instance) { }


        protected virtual object HandleSingleQuery(object result, Expression expression)
        {
            Standardize(result);
            return result;
        }

        protected virtual IList HandleMultiQuery(IList results, Expression expression)
        {
            StandardizeMany(results);
            return results;
        }
        
        private void HandleQueryProviderEvents()
        {
            m_QueryProvider.OnSingleQuery += (object result, Expression expression) =>
            {
                return HandleSingleQuery(result, expression);
            };

            m_QueryProvider.OnMultiQuery += (IList results, Expression expression) =>
            {
                return HandleMultiQuery(results, expression);
            };
        }


    }
}
