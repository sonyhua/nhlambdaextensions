
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
        public void TestBetween()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Between("Age", 5, 10));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.Between<Person>(p => p.Age, 5, 10));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
