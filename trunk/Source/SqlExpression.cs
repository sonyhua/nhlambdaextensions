
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
    /// cannot inherit from NHibernate.Criterion.Expression cos it's sealed
    /// </remarks>
    public class SqlExpression
    {

        /// <summary>
        /// lamda expression for between
        /// </summary>
        /// <typeparam name="T">type of expression</typeparam>
        /// <param name="expression">expression returning type's property</param>
        /// <param name="lo">low value of between</param>
        /// <param name="hi">high value of between</param>
        /// <returns></returns>
        public static ICriterion Between<T>(Expression<Func<T, object>> expression,
                                            object                      lo,
                                            object                      hi)
        {
            MemberExpression me = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.Between(me.Member.Name, lo, hi);
        }
        
    }

}

