
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

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
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return criteria.CreateCriteria(me.Member.Name);
        }

    }

}

