
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestLExtension : TestBase
    {

        private void AssertCriteriaAreEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void Test_Between()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Between("Age", 5, 10));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.Between(p => p.Age, 5, 10));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Like()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Like("Name", "%test%"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.Like(p => p.Name, "%test%"));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNull()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNull("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.IsNull(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotNull()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNotNull("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.IsNotNull(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsEmpty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsEmpty("Children"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.IsEmpty(p => p.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotEmpty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNotEmpty("Children"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.IsNotEmpty(p => p.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_In()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.In("Name", new string[] { "name1", "name2", "name3" }));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions<Person>.In(p => p.Name, new string[] { "name1", "name2", "name3" }));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
