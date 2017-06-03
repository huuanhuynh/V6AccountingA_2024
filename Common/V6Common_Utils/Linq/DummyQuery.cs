using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace V6Soft.Common.Utils.Linq
{
    public class DummyQuery<TQuery> : IQueryable<TQuery>, IQueryable, IEnumerable<TQuery>, IEnumerable, IOrderedQueryable<TQuery>, IOrderedQueryable
    {
        private IQueryProvider m_Provider;
        private Expression m_Expression;

        public DummyQuery(IQueryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            m_Provider = provider;
            m_Expression = Expression.Constant(this);
        }

        public DummyQuery(IQueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            if (!typeof(IQueryable<TQuery>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }
            m_Provider = provider;
            m_Expression = expression;
        }

        Expression IQueryable.Expression
        {
            get { return m_Expression; }
        }

        Type IQueryable.ElementType
        {
            get { return typeof(TQuery); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return m_Provider; }
        }

        public IEnumerator<TQuery> GetEnumerator()
        {
            var enumerator = ((IEnumerable<TQuery>)m_Provider.Execute(m_Expression)).GetEnumerator();
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var enumerator = ((IEnumerable)m_Provider.Execute(m_Expression)).GetEnumerator();
            return enumerator;
        }
    }
}