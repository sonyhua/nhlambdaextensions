
using System;
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
        
    }

}

