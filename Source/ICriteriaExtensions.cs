
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
    /// Extension methods for NHibernate ICriteria interface
    /// </summary>
    public static class ICriteriaExtensions
    {

        /// <summary>
        /// Add criterion expressed as a lambda expression
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns>criteria instance</returns>
        public static ICriteria Add<T>( this ICriteria              criteria,
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
        public static ICriteria Add(this ICriteria          criteria,
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
        /// <param name="expression">Lambda expression</param>
        /// <param name="orderDelegate">Order delegate (direction)</param>
        /// <returns>criteria instance</returns>
        public static ICriteria AddOrder<T>(this ICriteria              criteria,
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
        /// <param name="expression">Lambda expression</param>
        /// <param name="orderDelegate">Order delegate (direction)</param>
        /// <returns>criteria instance</returns>
        public static ICriteria AddOrder(   this ICriteria              criteria,
                                            Expression<Func<object>>    expression,
                                            Func<string, Order>         orderDelegate)
        {
            Order order = ExpressionProcessor.ProcessOrder(expression, orderDelegate);
            criteria.AddOrder(order);
            return criteria;
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria<T>(  this ICriteria              criteria,
                                                    Expression<Func<T, object>> expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria( this ICriteria              criteria,
                                                Expression<Func<object>>    expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria<T>(  this ICriteria              criteria,
                                                    Expression<Func<T, object>> expression,
                                                    JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path, joinType);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria( this ICriteria              criteria,
                                                Expression<Func<object>>    expression,
                                                JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(path, joinType);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria<T>(  this ICriteria              criteria,
                                                    Expression<Func<T, object>> expression,
                                                    Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria( this ICriteria              criteria,
                                                Expression<Func<object>>    expression,
                                                Expression<Func<object>>    alias)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <typeparam name="T">Type (same as criteria type)</typeparam>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria<T>(  this ICriteria              criteria,
                                                    Expression<Func<T, object>> expression,
                                                    Expression<Func<object>>    alias,
                                                    JoinType                    joinType)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.CreateCriteria(path, aliasContainer, joinType);
        }

        /// <summary>
        /// Create a new NHibernate.ICriteria, "rooted" at the associated entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="expression">Lambda expression returning association path</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="joinType">The type of join to use</param>
        /// <returns>The created "sub criteria"</returns>
        public static ICriteria CreateCriteria( this ICriteria              criteria,
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
        public static ICriteria CreateAlias<T>( this ICriteria              criteria,
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
        public static ICriteria CreateAlias(this ICriteria              criteria,
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
        public static ICriteria CreateAlias<T>( this ICriteria              criteria,
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
        public static ICriteria CreateAlias(this ICriteria              criteria,
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
        public static ICriteria SetFetchMode<T>(this ICriteria              criteria,
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
        public static ICriteria SetFetchMode(   this ICriteria              criteria,
                                                Expression<Func<object>>    alias,
                                                FetchMode                   fetchMode)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.SetFetchMode(aliasContainer, fetchMode);
        }

        /// <summary>
        /// Set the lock mode of the aliased entity
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <param name="lockMode">the lock mode</param>
        /// <returns>criteria instance</returns>
        public static ICriteria SetLockMode(this ICriteria              criteria,
                                            Expression<Func<object>>    alias,
                                            LockMode                    lockMode)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return criteria.SetLockMode(aliasContainer, lockMode);
        }

        /// <summary>
		/// Alows to get a sub criteria by alias.
		/// Will return null if the criteria does not exists
        /// </summary>
        /// <param name="criteria">criteria instance</param>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>The "sub criteria"</returns>
        public static ICriteria GetCriteriaByAlias( this ICriteria              criteria,
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
        public static ICriteria GetCriteriaByPath<T>(   this ICriteria              criteria,
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
        public static ICriteria GetCriteriaByPath(  this ICriteria              criteria,
                                                    Expression<Func<object>>    expression)
        {
            string path = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.GetCriteriaByPath(path);
        }

    }

}

