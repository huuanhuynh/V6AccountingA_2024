using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.QueryExtensions
{
    public static class QueryExtensions
    {
        public static IList<T> ToList<T>(this IQueryable query)
        {
            var list = new List<T>();
            var resultEnum = query.GetEnumerator();
            while (resultEnum.MoveNext())
            {
                list.Add((T)resultEnum.Current);
            }
            return list;
        }
    }
}
