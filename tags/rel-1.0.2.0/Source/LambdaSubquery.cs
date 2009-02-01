
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
    /// Provides typesafe sub-queries using lambda expressions to express properties
    /// </summary>
    public class LambdaSubquery
    {

        /// <summary>
        /// Protected constructor - class not for instantiation
        /// </summary>
        protected LambdaSubquery() { }

        /// <summary>
        /// Create a ICriterion for the specified property subquery expression
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="expression">lambda expression</param>
        /// <returns>returns LambdaSubqueryBuilder</returns>
        public static LambdaSubqueryBuilder Property<T>(Expression<Func<T, object>> expression)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return new LambdaSubqueryBuilder(property);
        }

        /// <summary>
        /// Create a ICriterion for the specified property subquery expression
        /// </summary>
        /// <param name="expression">lambda expression</param>
        /// <returns>returns LambdaSubqueryBuilder</returns>
        public static LambdaSubqueryBuilder Property(Expression<Func<object>> expression)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return new LambdaSubqueryBuilder(property);
        }

        /// <summary>
        /// Create ICriterion for subquery expression using lambda syntax
        /// </summary>
        /// <typeparam name="T">type of property</typeparam>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion Where<T>(Expression<Func<T, bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery<T>(LambdaSubqueryType.Exact, expression);
            return criterion;
        }

        /// <summary>
        /// Create ICriterion for (exact) subquery expression using lambda syntax
        /// </summary>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion Where(Expression<Func<bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery(LambdaSubqueryType.Exact, expression);
            return criterion;
        }

        /// <summary>
        /// Create ICriterion for (all) subquery expression using lambda syntax
        /// </summary>
        /// <typeparam name="T">type of property</typeparam>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion WhereAll<T>(Expression<Func<T, bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery<T>(LambdaSubqueryType.All, expression);
            return criterion;
        }

        /// <summary>
        /// Create ICriterion for (all) subquery expression using lambda syntax
        /// </summary>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion WhereAll(Expression<Func<bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery(LambdaSubqueryType.All, expression);
            return criterion;
        }

        /// <summary>
        /// Create ICriterion for (some) subquery expression using lambda syntax
        /// </summary>
        /// <typeparam name="T">type of property</typeparam>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion WhereSome<T>(Expression<Func<T, bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery<T>(LambdaSubqueryType.Some, expression);
            return criterion;
        }

        /// <summary>
        /// Create ICriterion for (some) subquery expression using lambda syntax
        /// </summary>
        /// <param name="expression">lambda expression</param>
        /// <returns>NHibernate.ICriterion.AbstractCriterion</returns>
        public static AbstractCriterion WhereSome(Expression<Func<bool>> expression)
        {
            AbstractCriterion criterion = ExpressionProcessor.ProcessSubquery(LambdaSubqueryType.Some, expression);
            return criterion;
        }

    }

}

