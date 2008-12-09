
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
        private readonly static IDictionary<ExpressionType, Func<string, string, ICriterion>> _propertyExpressionCreators = null;

        static ExpressionProcessor()
        {
            _simpleExpressionCreators = new Dictionary<ExpressionType, Func<string, object, ICriterion>>();
            _simpleExpressionCreators[ExpressionType.Equal] = Eq;
            _simpleExpressionCreators[ExpressionType.NotEqual] = Ne;
            _simpleExpressionCreators[ExpressionType.GreaterThan] = Gt;
            _simpleExpressionCreators[ExpressionType.GreaterThanOrEqual] = Ge;
            _simpleExpressionCreators[ExpressionType.LessThan] = Lt;
            _simpleExpressionCreators[ExpressionType.LessThanOrEqual] = Le;

            _propertyExpressionCreators = new Dictionary<ExpressionType, Func<string, string, ICriterion>>();
            _propertyExpressionCreators[ExpressionType.Equal] = Restrictions.EqProperty;
            _propertyExpressionCreators[ExpressionType.NotEqual] = Restrictions.NotEqProperty;
            _propertyExpressionCreators[ExpressionType.GreaterThan] = Restrictions.GtProperty;
            _propertyExpressionCreators[ExpressionType.GreaterThanOrEqual] = Restrictions.GeProperty;
            _propertyExpressionCreators[ExpressionType.LessThan] = Restrictions.LtProperty;
            _propertyExpressionCreators[ExpressionType.LessThanOrEqual] = Restrictions.LeProperty;
        }

        private static ICriterion Eq(string propertyName, object value)
        {
            return Restrictions.Eq(propertyName, value);
        }
        
        private static ICriterion Ne(string propertyName, object value)
        {
            return
                NHibernate.Criterion.Restrictions.Not(
                    NHibernate.Criterion.Restrictions.Eq(propertyName, value));
        }
        
        private static ICriterion Gt(string propertyName, object value)
        {
            return NHibernate.Criterion.Restrictions.Gt(propertyName, value);
        }

        private static ICriterion Ge(string propertyName, object value)
        {
            return NHibernate.Criterion.Restrictions.Ge(propertyName, value);
        }

        private static ICriterion Lt(string propertyName, object value)
        {
            return NHibernate.Criterion.Restrictions.Lt(propertyName, value);
        }

        private static ICriterion Le(string propertyName, object value)
        {
            return NHibernate.Criterion.Restrictions.Le(propertyName, value);
        }

        /// <summary>
        /// Retrieves the name of the property from a member expression
        /// </summary>
        /// <param name="expression">An expression tree that can contain either a member, or a conversion from a member.
        /// If the member is referenced from a ..., then the container is treated as an alias.</param>
        /// <returns>The name of the member property</returns>
        public static string FindMemberExpression(System.Linq.Expressions.Expression expression)
        {
            MemberExpression me = null;
            if (expression is MemberExpression)
                me = (MemberExpression)expression;

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception("Cannot interpret member from " + expression.ToString());

                me = (MemberExpression)unaryExpression.Operand;
            }

            if (me == null)
                throw new Exception("Could not determine member from " + expression.ToString());

            string member = me.Member.Name;

            if (me.Expression.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression alias = (MemberExpression)me.Expression;
                member = alias.Member.Name + "." + member;
            }

            return member;
        }

        private static bool EvaluatesToNull(System.Linq.Expressions.Expression expression)
        {
            var valueExpression = System.Linq.Expressions.Expression.Lambda(expression).Compile();
            object value = valueExpression.DynamicInvoke();
            return (value == null);
        }

        private static bool IsMemberExpression(System.Linq.Expressions.Expression expression)
        {
            MemberExpression me = null;

            if (expression is MemberExpression)
            {
                me = (MemberExpression)expression;
            }

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception("Cannot interpret member from " + expression.ToString());

                me = (MemberExpression)unaryExpression.Operand;
            }

            if (me == null)
                return false;

            if (me.Expression.NodeType == ExpressionType.Parameter)
                return true;

            if (me.Expression.NodeType == ExpressionType.MemberAccess)
            {
                // if the member has a null value, it was an alias
                if (EvaluatesToNull(me.Expression))
                    return true;
            }

            return false;
        }

        private static ICriterion ProcessSimpleExpression(BinaryExpression be)
        {
            string property = FindMemberExpression(be.Left);

            var valueExpression = System.Linq.Expressions.Expression.Lambda(be.Right).Compile();
            var value = valueExpression.DynamicInvoke();

            if (!_simpleExpressionCreators.ContainsKey(be.NodeType))
                throw new Exception("Unhandled simple expression type: " + be.NodeType);

            Func<string, object, ICriterion> simpleExpressionCreator = _simpleExpressionCreators[be.NodeType];
            ICriterion criterion = simpleExpressionCreator(property, value);
            return criterion;
        }

        private static ICriterion ProcessMemberExpression(BinaryExpression be)
        {
            string leftProperty = FindMemberExpression(be.Left);
            string rightProperty = FindMemberExpression(be.Right);

            if (!_propertyExpressionCreators.ContainsKey(be.NodeType))
                throw new Exception("Unhandled property expression type: " + be.NodeType);

            Func<string, string, ICriterion> propertyExpressionCreator = _propertyExpressionCreators[be.NodeType];
            ICriterion criterion = propertyExpressionCreator(leftProperty, rightProperty);
            return criterion;
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

            if (IsMemberExpression(be.Right))
                return ProcessMemberExpression(be);
            else
                return ProcessSimpleExpression(be);
        }

        /// <summary>
        /// Convert a lambda expression to NHibernate ICriterion
        /// </summary>
        /// <param name="expression">The lambda expression to convert</param>
        /// <returns>NHibernate ICriterion</returns>
        public static ICriterion ProcessExpression(Expression<Func<bool>> expression)
        {
            BinaryExpression be = (BinaryExpression)expression.Body;

            if (IsMemberExpression(be.Right))
                return ProcessMemberExpression(be);
            else
                return ProcessSimpleExpression(be);
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
            string property = FindMemberExpression(expression.Body);
            Order order = orderDelegate(property);
            return order;
        }

        /// <summary>
        /// Convert a lambda expression to NHibernate Order
        /// </summary>
        /// <param name="expression">The lambda expression to convert</param>
        /// <param name="orderDelegate">The appropriate order delegate (order direction)</param>
        /// <returns>NHibernate Order</returns>
        public static Order ProcessOrder(   Expression<Func<object>>    expression,
                                            Func<string, Order>         orderDelegate)
        {
            string property = FindMemberExpression(expression.Body);
            Order order = orderDelegate(property);
            return order;
        }

    }

}

