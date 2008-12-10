
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Extension methods for NHibernate projections
    /// </summary>
    public static class ProjectionExtensions
    {

        /// <summary>
        /// Create an alias for a projection
        /// </summary>
        /// <param name="projection">the projection instance</param>
        /// <param name="alias">LambdaExpression returning an alias</param>
        /// <returns>return NHibernate.Criterion.IProjection</returns>
        public static IProjection As(   this SimpleProjection       projection,
                                        Expression<Func<object>>    alias)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return projection.As(aliasContainer);
        }

    }

}

