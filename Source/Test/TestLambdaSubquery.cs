
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestLambdaSubquery : TestBase
    {

        // In
        // NotIn

        // Eq
        // Ne
        // Gt
        // Lt
        // Ge
        // Le

        // EqAll
        // GtAll
        // LtAll
        // GeAll
        // LeAll

        // GtSome
        // LtSome
        // GeSome
        // LeSome

        private Person _subqueryPersonAlias = null;

        private DetachedCriteria DetachedCriteriaSubquery
        {
            get
            {
                return DetachedCriteria<Person>.Create(() => _subqueryPersonAlias)
                    .Add(() => _subqueryPersonAlias.Name == "subquery name");
            }
        }

        [Test]
        public void Test_PropertyEq()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Subqueries.PropertyEq("Name", DetachedCriteriaSubquery));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(LambdaSubquery.Property<Person>(p => p.Name).Eq(DetachedCriteriaSubquery));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyEqAlternativeSyntax()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Subqueries.PropertyEq("Name", DetachedCriteriaSubquery));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(LambdaSubquery.Where<Person>(p => p.Name == DetachedCriteriaSubquery.As<string>()));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyGtUsingAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .Add(Subqueries.PropertyGt("personAlias.Age", DetachedCriteriaSubquery));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .Add(LambdaSubquery.Property(() => personAlias.Age).Gt(DetachedCriteriaSubquery));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyGtUsingAliasAlternativeSyntax()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .Add(Subqueries.PropertyGt("personAlias.Age", DetachedCriteriaSubquery));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .Add(LambdaSubquery.Where(() => personAlias.Age > DetachedCriteriaSubquery.As<int>()));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
