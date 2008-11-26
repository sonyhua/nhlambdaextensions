
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Converts lambda expressions to NHibernate criterion/order
    /// </summary>
    public static class ExpressionProcessor
    {

        private readonly static IDictionary<ExpressionType, Func<string, object, ICriterion>> _simpleExpressionCreators = null;

        static ExpressionProcessor()
        {
            _simpleExpressionCreators = new Dictionary<ExpressionType, Func<string, object, ICriterion>>();
            _simpleExpressionCreators[ExpressionType.Equal] = Eq;
            _simpleExpressionCreators[ExpressionType.NotEqual] = Ne;
            _simpleExpressionCreators[ExpressionType.GreaterThan] = Gt;
            _simpleExpressionCreators[ExpressionType.GreaterThanOrEqual] = Ge;
            _simpleExpressionCreators[ExpressionType.LessThan] = Lt;
            _simpleExpressionCreators[ExpressionType.LessThanOrEqual] = Le;
        }

        private static ICriterion Eq(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Eq(propertyName, value);
        }
        
        private static ICriterion Ne(string propertyName, object value)
        {
            return
                NHibernate.Criterion.Expression.Not(
                    NHibernate.Criterion.Expression.Eq(propertyName, value));
        }
        
        private static ICriterion Gt(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Gt(propertyName, value);
        }

        private static ICriterion Ge(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Ge(propertyName, value);
        }

        private static ICriterion Lt(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Lt(propertyName, value);
        }

        private static ICriterion Le(string propertyName, object value)
        {
            return NHibernate.Criterion.Expression.Le(propertyName, value);
        }

        /// <summary>
        /// Retrieves the MemberExpression from the expression
        /// </summary>
        /// <param name="expression">An expression tree that can contain either a member, or a conversion from a member</param>
        /// <returns>The appropriate MemberExpression</returns>
        public static MemberExpression FindMemberExpression(System.Linq.Expressions.Expression expression)
        {
            if (expression is MemberExpression)
                return (MemberExpression)expression;

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception("Cannot interpret member from " + expression.ToString());

                return (MemberExpression)unaryExpression.Operand;
            }
            throw new Exception("Could not determine member from " + expression.ToString());
        }

        /// <summary>
        /// Convert a lambda expression to NHibernate ICriterion
        /// </summary>
        /// <typeparam name="T">The type of the lambda expression</typeparam>
        /// <param name="expression">The lambda expression to convert</param>
        /// <returns>NHibernate ICriterion</returns>
        public static ICriterion ProcessExpression<T>(Expression<Func<T, bool>> expression)
        {
            BinaryExpression be = (BinaryExpression)expression.Body;
            MemberExpression me = FindMemberExpression(be.Left);

            var valueExpression = System.Linq.Expressions.Expression.Lambda(be.Right).Compile();
            var value = valueExpression.DynamicInvoke();

            if (!_simpleExpressionCreators.ContainsKey(be.NodeType))
                throw new Exception("Unhandled expression type: " + be.NodeType);

            Func<string, object, ICriterion> simpleExpressionCreator = _simpleExpressionCreators[be.NodeType];
            ICriterion criterion = simpleExpressionCreator(me.Member.Name, value);
            return criterion;
        }

        /// <summary>
        /// Convert a lambda expression to NHibernate Order
        /// </summary>
        /// <typeparam name="T">The type of the lambda expression</typeparam>
        /// <param name="expression">The lambda expression to convert</param>
        /// <param name="orderDelegate">The appropriate order delegate (order direction)</param>
        /// <returns>NHibernate Order</returns>
        public static Order ProcessOrder<T>(Expression<Func<T, object>> expression,
                                            Func<string, Order>         orderDelegate)
        {
            MemberExpression me = FindMemberExpression(expression.Body);
            Order order = orderDelegate(me.Member.Name);
            return order;
        }
        
    }

}

