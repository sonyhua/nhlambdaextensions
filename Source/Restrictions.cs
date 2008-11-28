
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Provides extra Expression factory methods
    /// </summary>
    /// <remarks>
    /// cannot inherit from NHibernate.Criterion.Restrictions cos it's constructor is internal
    /// </remarks>
    public class Restrictions<T>
    {

        /// <summary>
        /// Apply a "between" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <param name="lo">low value of between</param>
        /// <param name="hi">high value of between</param>
        /// <returns>A NHibernate.Criterion.BetweenExpression.</returns>
        public static ICriterion Between(   Expression<Func<T, object>> expression,
                                            object                      lo,
                                            object                      hi)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.Between(me.Member.Name, lo, hi);
        }

        /// <summary>
        /// Apply a "like" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <param name="value">The value for the Property.</param>
        /// <returns>A NHibernate.Criterion.LikeExpression.</returns>
        public static ICriterion Like(  Expression<Func<T, object>> expression,
                                        object                      value)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.Like(me.Member.Name, value);
        }
        
        /// <summary>
        /// Apply an "is null" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <returns>A NHibernate.Criterion.NullExpression.</returns>
        public static ICriterion IsNull(Expression<Func<T, object>> expression)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.IsNull(me.Member.Name);
        }

        /// <summary>
        /// Apply an "is not null" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <returns>A NHibernate.Criterion.NotNullExpression.</returns>
        public static ICriterion IsNotNull(Expression<Func<T, object>> expression)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.IsNotNull(me.Member.Name);
        }

        /// <summary>
        /// Apply an "is empty" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <returns>A NHibernate.Criterion.IsEmptyExpression.</returns>
        public static ICriterion IsEmpty(Expression<Func<T, IEnumerable>> expression)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.IsEmpty(me.Member.Name);
        }

        /// <summary>
        /// Apply an "is not empty" constraint to the named property
        /// </summary>
        /// <param name="expression">lambda expression returning type's property</param>
        /// <returns>A NHibernate.Criterion.IsNotEmptyExpression.</returns>
        public static ICriterion IsNotEmpty(Expression<Func<T, IEnumerable>> expression)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.IsNotEmpty(me.Member.Name);
        }

    }

}

