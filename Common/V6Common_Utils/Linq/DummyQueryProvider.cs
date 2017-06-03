using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace V6Soft.Common.Utils.Linq
{
    public class DummyQueryProvider : IQueryProvider
    {
        public delegate object SingleQueryEventHandler(object result, Expression expression);
        public delegate IList MultiQueryEventHandler(IList results, Expression expression);

        public event SingleQueryEventHandler OnSingleQuery;
        public event MultiQueryEventHandler OnMultiQuery;

        private IQueryable m_Query;
        private IQueryProvider m_OriginalProvider;

        public DummyQueryProvider(IQueryable query)
        {
            m_Query = query;
            m_OriginalProvider = query.Provider;
        }

        public IQueryable<T> CreateQuery<T>()
        {
            return new DummyQuery<T>(this, m_Query.Expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(System.Linq.Expressions.Expression expression)
        {
            m_Query = m_OriginalProvider.CreateQuery<TElement>(expression);
            return new DummyQuery<TElement>(this, m_Query.Expression);
        }

        public IQueryable CreateQuery(System.Linq.Expressions.Expression expression)
        {
            m_Query = m_OriginalProvider.CreateQuery(expression);

            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(DummyQuery<>).MakeGenericType(elementType), new object[] { this, m_Query.Expression });
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            IEnumerator enumerator = m_Query.GetEnumerator();
            object result = default(TResult);

            if (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }

            if (null != OnSingleQuery)
            {
                result = OnSingleQuery(result, expression);
            }

            return (TResult)result;
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            Type listType = typeof(List<>).MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(listType);
            MethodInfo addFunc = listType.GetMethod("Add");
            IEnumerator enumerator = m_Query.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                addFunc.Invoke(list, new object[] { enumerator.Current });
            }

            if (null != OnMultiQuery)
            {
                list = OnMultiQuery(list, expression);
            }

            return list;
        }
    }
}
