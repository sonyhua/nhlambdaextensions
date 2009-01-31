
using System;

using NUnit.Framework;

using NHibernate.Criterion;

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
                    .Add(Expression.Eq("Property1", "Value"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Expression.Eq("Property2", "Value"));

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

	}

}
