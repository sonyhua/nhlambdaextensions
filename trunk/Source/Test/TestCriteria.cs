
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
            expected = expected.GetCriteriaByAlias(expected.RootAlias);
            actual = actual.GetCriteriaByAlias(expected.RootAlias);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreEqual(expected.Alias, actual.Alias);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count);

            for (int i=0; i<expected.SubcriteriaList.Count; i++)
            {
                Impl.CriteriaImpl.Subcriteria expectedSubcriteria = (Impl.CriteriaImpl.Subcriteria)expected.SubcriteriaList[i];
                Impl.CriteriaImpl.Subcriteria actualSubcriteria = (Impl.CriteriaImpl.Subcriteria)actual.SubcriteriaList[i];
                Assert.AreEqual(expectedSubcriteria.ToString(), actualSubcriteria.ToString());
                Assert.AreEqual(expectedSubcriteria.Alias, actualSubcriteria.Alias);
                Assert.AreEqual(expectedSubcriteria.Path, actualSubcriteria.Path);
            }
        }

        private ICriteria CreateCriteria<T>()
        {
            return new NHibernate.Impl.CriteriaImpl(typeof(T), null);
        }

        [Test]
        public void Test_Eq()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Eq("Name", "test name"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_EqWithMemberExpression()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Eq("Name", "test name"));

            string name = "test name";
            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name == name);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Gt()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Gt("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age > 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Ne()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Not(Restrictions.Eq("Name", "test name")));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Name != "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Ge()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Ge("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age >= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Lt()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Lt("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age < 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Le()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Le("Age", 10));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add<Person>(p => p.Age <= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_OrderByStringProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .AddOrder(Order.Desc("Name"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_OrderByInt32Property()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .AddOrder(Order.Asc("Age"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .AddOrder<Person>(p => p.Age, Order.Asc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociation()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateCriteria("Children")
                        .Add(Restrictions.Eq("Nickname", "test"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateCriteria((Person p) => p.Children)
                        .Add<Child>(p => p.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
