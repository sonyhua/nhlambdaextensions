
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
        /// Add criterion expressed as a lambda expression
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria Add( this DetachedCriteria   criteria,
                                            Expression<Func<bool>>  expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression(expression);
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
        /// Add order expressed as a lambda expression
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lamba expression</param>
        /// <param name="orderDelegate">Order delegate (direction)</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria AddOrder(this DetachedCriteria       criteria,
                                                Expression<Func<object>>    expression,
                                                Func<string, Order>         orderDelegate)
        {
            Order order = ExpressionProcessor.ProcessOrder(expression, orderDelegate);
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
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria(  this DetachedCriteria       criteria,
                                                        Expression<Func<object>>    expression)
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
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria(  this DetachedCriteria       criteria,
                                                        Expression<Func<object>>    expression,
                                                        JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path, joinType);
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria<T>(   this DetachedCriteria       criteria,
                                                            Expression<Func<T, object>> expression,
                                                            Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer);
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria(  this DetachedCriteria       criteria,
                                                        Expression<Func<object>>    expression,
                                                        Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer);
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria<T>(   this DetachedCriteria       criteria,
                                                            Expression<Func<T, object>> expression,
                                                            Expression<Func<object>>    alias,
                                                            JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer, joinType);
        }

        /// <summary>
        /// Create a new DetachedCriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static DetachedCriteria CreateCriteria(  this DetachedCriteria       criteria,
                                                        Expression<Func<object>>    expression,
                                                        Expression<Func<object>>    alias,
                                                        JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer, joinType);
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

        /// <summary>
        /// Join an association, assigning an alias to the joined entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria CreateAlias( this DetachedCriteria       criteria,
                                                    Expression<Func<object>>    expression,
                                                    Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateAlias(path, aliasContainer);
        }

        /// <summary>
        /// Join an association, assigning an alias to the joined entity
        /// </summary>
        /// <typeparam name="T">type of association</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria CreateAlias<T>(  this DetachedCriteria       criteria,
                                                        Expression<Func<T, object>> expression,
                                                        Expression<Func<object>>    alias,
                                                        JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateAlias(path, aliasContainer, joinType);
        }

        /// <summary>
        /// Join an association, assigning an alias to the joined entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria CreateAlias( this DetachedCriteria       criteria,
                                                    Expression<Func<object>>    expression,
                                                    Expression<Func<object>>    alias,
                                                    JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateAlias(path, aliasContainer, joinType);
        }

        /// <summary>
        /// Specify an association fetching strategy.
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="fetchMode">The Fetch mode.</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria SetFetchMode<T>( this DetachedCriteria       criteria,
                                                        Expression<Func<T, object>> alias,
                                                        FetchMode                   fetchMode)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.SetFetchMode(aliasContainer, fetchMode);
        }

        /// <summary>
        /// Specify an association fetching strategy.
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="fetchMode">The Fetch mode.</param>
        /// <returns>criteria instance</returns>
        public static DetachedCriteria SetFetchMode(this DetachedCriteria       criteria,
                                                    Expression<Func<object>>    alias,
                                                    FetchMode                   fetchMode)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.SetFetchMode(aliasContainer, fetchMode);
        }

        /// <summary>
        /// Extension method to allow comparison of detached criteria (always returns null)
        /// </summary>
        /// <typeparam name="T">type returned by detached criteria</typeparam>
        /// <param name="criteria">the DetachedCriteria instance</param>
        /// <returns>returns null (or value-type default)</returns>
        public static T As<T>(this DetachedCriteria criteria)
        {
            return default(T);
        }

        /// <summary>
		/// Alows to get a sub criteria by alias.
		/// Will return null if the criteria does not exists
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The "sub criteria"</returns>
        public static DetachedCriteria GetCriteriaByAlias(  this DetachedCriteria       criteria,
                                                            Expression<Func<object>>    alias)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.GetCriteriaByAlias(aliasContainer);
        }

        /// <summary>
        /// Allows to get a sub criteria by path.  Will return null if the criteria does
        /// not exists.
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The "sub criteria"</returns>
        public static DetachedCriteria GetCriteriaByPath<T>(this DetachedCriteria       criteria,
                                                            Expression<Func<T, object>> expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.GetCriteriaByPath(path);
        }

        /// <summary>
        /// Allows to get a sub criteria by path.  Will return null if the criteria does
        /// not exists.
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The "sub criteria"</returns>
        public static DetachedCriteria GetCriteriaByPath(   this DetachedCriteria       criteria,
                                                            Expression<Func<object>>    expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.GetCriteriaByPath(path);
        }

    }

}

