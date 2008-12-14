
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
    /// Provides syntax extensions to allow building of subquery criterion for LambdaSubquery class
    /// </summary>
    public class LambdaSubqueryBuilder
    {

        private string _propertyName;

        /// <summary>
        /// Constructed with property name
        /// </summary>
        public LambdaSubqueryBuilder(string propertyName)
        {
            _propertyName = propertyName;
        }

        private AbstractCriterion CreatePropertyCriterion(  Func<string, DetachedCriteria, AbstractCriterion>   factoryMethod,
                                                            DetachedCriteria                                    detachedCriterion)
        {
            return factoryMethod(_propertyName, detachedCriterion);
        }

        /// <summary>
        /// Create a property in subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion In(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyIn, detachedCriteria);
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

        /// <summary>
        /// Create a property greater than subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Gt(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGt, detachedCriteria);
        }

    }

}

