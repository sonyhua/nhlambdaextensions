
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
        /// Create a property not in subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion NotIn(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyNotIn, detachedCriteria);
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
        /// Create a property not equal subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Ne(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyNe, detachedCriteria);
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

        /// <summary>
        /// Create a property greater than or equal subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Ge(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGe, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Lt(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLt, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than or equal subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion Le(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLe, detachedCriteria);
        }

        /// <summary>
        /// Create a property equal all subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion EqAll(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyEqAll, detachedCriteria);
        }

        /// <summary>
        /// Create a property greater than all subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion GtAll(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGtAll, detachedCriteria);
        }

        /// <summary>
        /// Create a property greater than or equal all subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion GeAll(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGeAll, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than all subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion LtAll(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLtAll, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than or equal all subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion LeAll(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLeAll, detachedCriteria);
        }

        /// <summary>
        /// Create a property greater than some subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion GtSome(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGtSome, detachedCriteria);
        }

        /// <summary>
        /// Create a property greater than or equal some subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion GeSome(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyGeSome, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than some subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion LtSome(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLtSome, detachedCriteria);
        }

        /// <summary>
        /// Create a property less than or equal some subquery criterion
        /// </summary>
        /// <param name="detachedCriteria">detached criteria subquery</param>
        /// <returns>returns NHibernate.Criterion.AbstractCriterion</returns>
        public AbstractCriterion LeSome(DetachedCriteria detachedCriteria)
        {
            return CreatePropertyCriterion(Subqueries.PropertyLeSome, detachedCriteria);
        }

    }

}

