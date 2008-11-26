
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestCriteria : TestBase
    {

        private void AssertCriteriaAreEqual(ICriteria expected, ICriteria actual)
        {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        private ICriteria CreateCriteria<T>()
        {
            return new NHibernate.Impl.CriteriaImpl(typeof(T), null);
        }

        [Test]
        public void TestEq()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Expression.Eq("Name", "test name"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSimpleEqWithMemberExpression()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Expression.Eq("Name", "test name"));

            string name = "test name";
            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name == name);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSimpleGt()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Expression.Gt("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age > 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSimpleNe()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Expression.Not(Expression.Eq("Name", "test name")));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name != "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestGe()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Expression.Ge("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age >= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestOrderAsc()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .AddOrder(Order.Desc("Name"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
