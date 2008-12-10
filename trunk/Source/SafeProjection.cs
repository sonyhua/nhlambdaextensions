
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
    /// Provides typesafe projections using lambda expressions to express properties
    /// </summary>
    public class SafeProjection
    {

        /// <summary>
        /// Protected constructor - class not for instantiation
        /// </summary>
        protected SafeProjection() { }

        /// <summary>
        /// Create an IProjection for the specified property expression
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="expression">lambda expression</param>
        /// <returns>return NHibernate.Criterion.PropertyProjection</returns>
        public static PropertyProjection Property<T>(Expression<Func<T, object>> expression)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Projections.Property(property);
        }

        /// <summary>
        /// Assign an alias to a projection by wrapping it
        /// </summary>
        /// <param name="projection">the projection to wrap</param>
        /// <param name="alias">LambdaExpression returning an alias</param>
        /// <returns>return NHibernate.Criterion.IProjection</returns>
        public static IProjection Alias(IProjection                 projection,
                                        Expression<Func<object>>    alias)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return Projections.Alias(projection, aliasContainer);
        }

    }

}

