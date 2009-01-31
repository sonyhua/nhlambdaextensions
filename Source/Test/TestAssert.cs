
using System;

using NUnit.Framework;

using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestAssert : TestBase
    {

        private void AssertCriteriaAreNotEqual(ICriteria expected, ICriteria actual)
        {
            try
            {
                AssertCriteriaAreEqual(expected, actual);
            }
            catch
            {
                return;
            }
            Assert.Fail("No exception thrown");
        }

        private void AssertCriteriaAreNotEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            try
            {
                AssertCriteriaAreEqual(expected, actual);
            }
            catch
            {
                return;
            }
            Assert.Fail("No exception thrown");
        }

        [Test]
        public void TestCheckTypes()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>();

            DetachedCriteria actual =
                DetachedCriteria.For<Child>();

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestCheckAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias1");

            DetachedCriteria actual =
                DetachedCriteria.For<Person>("personAlias2");

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestCheckOperators()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Property", "Value"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Gt("Property", "Value"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestCheckPropertyNames()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("a.b.Property1", "Value"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("a.b.Property2", "Value"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestCheckValues()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Property", "Value1"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Property", "Value2"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestNested()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Not(Expression.Eq("Property", "Value")));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Not(Expression.Gt("Property", "Value")));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestOrder()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .AddOrder(Order.Asc("name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .AddOrder(Order.Desc("name"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestSubCriteria()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Child")
                        .Add(Expression.Eq("Name", "test"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Child")
                        .Add(Expression.Gt("Name", "test"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Child", JoinType.InnerJoin);

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Child", JoinType.LeftOuterJoin);

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestFetchMode()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetFetchMode("Father", FetchMode.Eager);

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetFetchMode("Father", FetchMode.Lazy);

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestLockMode()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .SetLockMode("fatherAlias", LockMode.Upgrade);

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .SetLockMode("fatherAlias", LockMode.Force);

            AssertCriteriaAreNotEqual(expected, actual);
        }

        [Test]
        public void TestProjections()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .SetProjection(Projections.Avg("Age"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .SetProjection(Projections.Max("Age"));

            AssertCriteriaAreNotEqual(expected, actual);
        }

	}

}
