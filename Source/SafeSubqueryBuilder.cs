
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
    /// Provides syntax extensions to allow building of subquery criterion for SafeSubquery class
    /// </summary>
    public class SafeSubqueryBuilder
    {

        private string _propertyName;

        /// <summary>
        /// Constructed with property name
        /// </summary>
        public SafeSubqueryBuilder(string propertyName)
        {
            _propertyName = propertyName;
        }

        private AbstractCriterion CreatePropertyCriterion(  Func<string, DetachedCriteria, AbstractCriterion>   factoryMethod,
                                                            DetachedCriteria                                    detachedCriterion)
        {
            return factoryMethod(_propertyName, detachedCriterion);
        }

        /// <summary>
        /// Create a property equal subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Eq(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyEq, detachedCriteria);
        }

    }

}

