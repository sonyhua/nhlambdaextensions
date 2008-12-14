
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestLambdaSubquery : TestBase
    {

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

        [Test]
        public void Test_PropertyEqAll()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Subqueries.PropertyEqAll("Name", DetachedCriteriaSubquery));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(LambdaSubquery.WhereAll<Person>(p => p.Name == DetachedCriteriaSubquery.As<string>()));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyIn()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Subqueries.PropertyIn("Name", DetachedCriteriaSubquery));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(LambdaSubquery.Property<Person>(p => p.Name).In(DetachedCriteriaSubquery));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestAllBuilderOverloads()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Subqueries.PropertyIn("Name", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyNotIn("Name", DetachedCriteriaSubquery))

                    .Add(Subqueries.PropertyEq("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyNe("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyGt("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyGe("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLt("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLe("Age", DetachedCriteriaSubquery))

                    .Add(Subqueries.PropertyEqAll("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyGtAll("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyGeAll("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLtAll("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLeAll("Age", DetachedCriteriaSubquery))

                    .Add(Subqueries.PropertyGtSome("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyGeSome("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLtSome("Age", DetachedCriteriaSubquery))
                    .Add(Subqueries.PropertyLeSome("Age", DetachedCriteriaSubquery));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(LambdaSubquery.Property<Person>(p => p.Name).In(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Name).NotIn(DetachedCriteriaSubquery))

                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Eq(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Ne(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Gt(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Ge(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Lt(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).Le(DetachedCriteriaSubquery))

                    .Add(LambdaSubquery.Property<Person>(p => p.Age).EqAll(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).GtAll(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).GeAll(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).LtAll(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).LeAll(DetachedCriteriaSubquery))

                    .Add(LambdaSubquery.Property<Person>(p => p.Age).GtSome(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).GeSome(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).LtSome(DetachedCriteriaSubquery))
                    .Add(LambdaSubquery.Property<Person>(p => p.Age).LeSome(DetachedCriteriaSubquery));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
