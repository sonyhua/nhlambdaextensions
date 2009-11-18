
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using NHibernate;

namespace NHibernate.LambdaExtensions
{

    /// <summary>
    /// Extension methods for NHibernate ISession interface
    /// </summary>
    public static class ISessionExtensions
    {

        /// <summary>
        /// Creates a new Criteria for the entity class with a specific alias
        /// </summary>
        /// <param name="session">session instance</param>
        /// <param name="persistentType">The class to Query</param>
        /// <param name="alias">The alias of the entity</param>
        /// <returns>An ICriteria object</returns>
        public static ICriteria CreateCriteria( this ISession               session,
                                                System.Type                 persistentType,
                                                Expression<Func<object>>    alias)
        {
            string aliasContainer = ExpressionProcessor.FindMemberExpression(alias.Body);
            return session.CreateCriteria(persistentType, aliasContainer);
        }

    }

}

