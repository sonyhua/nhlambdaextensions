
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
    /// Provides extra factory method for DetachedCriteria to allow creation with an alias
    /// </summary>
    /// <remarks>
    /// cannot add extension methods to simulate static method on DetachedCriteria
    /// </remarks>
    public class DetachedCriteria<T>
    {

        /// <summary>
        /// Create a DetachedCriteria with a strongly typed alias
        /// </summary>
        /// <param name="alias">Lambda expression returning alias reference</param>
        /// <returns>Newly created DetachedCriteria</returns>
        public static DetachedCriteria Create(Expression<Func<object>> alias)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return DetachedCriteria.For<T>(aliasContainer);
        }

    }

}

