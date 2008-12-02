
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Extension methods for NHibernate DetachedCriteria
    /// </summary>
    public static class DetachedCriteriaExtensions
    {

        /// <summary>
        /// Add criterion expressed as a lambda expression
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria Add<T>(  this DetachedCriteria       criteria,
                                                Expression<Func<T, bool>>   expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression<T>(expression);
            criteria.Add(criterion);
            return criteria;
        }

        /// <summary>
        /// Add order expressed as a lambda expression
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lamba expression</param>
        /// <param name="orderDelegate">Order delegate (direction)</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria AddOrder<T>( this DetachedCriteria       criteria,
                                                    Expression<Func<T, object>> expression,
                                                    Func<string, Order>         orderDelegate)
        {
            Order order = ExpressionProcessor.ProcessOrder<T>(expression, orderDelegate);
            criteria.AddOrder(order);
            return criteria;
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria<T>(   this DetachedCriteria       criteria,
                                                            Expression<Func<T, object>> expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path);
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria<T>(   this DetachedCriteria       criteria,
                                                            Expression<Func<T, object>> expression,
                                                            JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path, joinType);
        }

        /// <summary>
        /// Join an association, assigning an alias to the joined entity
        /// </summary>
        /// <typeparam name="T">type of association</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria CreateAlias<T>(  this DetachedCriteria       criteria,
                                                        Expression<Func<T, object>> expression,
                                                        Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateAlias(path, aliasContainer);
        }

    }

}

