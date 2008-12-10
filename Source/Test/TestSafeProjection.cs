
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestSafeProjection : TestBase
    {

        [Test]
        public void Test_SimpleProperty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Property("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Property<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
