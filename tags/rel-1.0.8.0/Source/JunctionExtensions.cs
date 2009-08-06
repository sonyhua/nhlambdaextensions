
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate.Criterion;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Extension methods for NHibernate Junction class
    /// </summary>
    public static class JunctionExtensions
    {

        /// <summary>
        /// Adds an NHibernate.Criterion.ICriterion to the list of NHibernate.Criterion.ICriterions
        /// to junction together.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="junction">NHibernate junction</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns>This NHibernate.Criterion.Junction instance.</returns>
        public static Junction Add<T>(  this Junction               junction,
                                        Expression<Func<T, bool>>   expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression<T>(expression);
            return junction.Add(criterion);
        }

        /// <summary>
        /// Adds an NHibernate.Criterion.ICriterion to the list of NHibernate.Criterion.ICriterions
        /// to junction together.
        /// </summary>
        /// <param name="junction">NHibernate junction</param>
        /// <param name="expression">Lambda expression</param>
        /// <returns>This NHibernate.Criterion.Junction instance.</returns>
        public static Junction Add( this Junction           junction,
                                    Expression<Func<bool>>  expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression(expression);
            return junction.Add(criterion);
        }

    }

}

