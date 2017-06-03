using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SummerBreeze.Linq
{
    internal class QueryTranslator : ExpressionVisitor
    {
        private StringBuilder m_QueryBuilder;

        internal QueryTranslator()
        {
        }

        internal string Translate(Expression expression)
        {
            this.m_QueryBuilder = new StringBuilder();
            this.Visit(expression);
            return this.m_QueryBuilder.ToString();
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(Queryable) && m.Method.Name == "Where")
            {
                m_QueryBuilder.Append("SELECT * FROM (");
                this.Visit(m.Arguments[0]);
                m_QueryBuilder.Append(") AS T WHERE ");
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);
                return m;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    m_QueryBuilder.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException(string.Format("The unary operator '{0}' is not supported", u.NodeType));
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            m_QueryBuilder.Append("(");
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    m_QueryBuilder.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    m_QueryBuilder.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    m_QueryBuilder.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    m_QueryBuilder.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    m_QueryBuilder.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    m_QueryBuilder.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    m_QueryBuilder.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    m_QueryBuilder.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            this.Visit(b.Right);
            m_QueryBuilder.Append(")");
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
                // assume constant nodes w/ IQueryables are table references
                m_QueryBuilder.Append("SELECT * FROM ");
                m_QueryBuilder.Append(q.ElementType.Name);
            }
            else if (c.Value == null)
            {
                m_QueryBuilder.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        m_QueryBuilder.Append(((bool)c.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        m_QueryBuilder.Append("'");
                        m_QueryBuilder.Append(c.Value);
                        m_QueryBuilder.Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
                    default:
                        m_QueryBuilder.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                m_QueryBuilder.Append(m.Member.Name);
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
    }
}
