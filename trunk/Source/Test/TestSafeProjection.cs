
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
        public void Test_Property()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Property("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Property<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Alias(Projections.Property("Name"), "nameAlias"));

            string nameAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Alias(SafeProjection.Property<Person>(p => p.Name), () => nameAlias));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyAliasWithValueType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Alias(Projections.Property("Age"), "ageAlias"));

            int ageAlias = 0;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Alias(SafeProjection.Property<Person>(p => p.Age), () => ageAlias));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_PropertyAliasUsingFluentInterface()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Property("Age").As("ageAlias"));

            int ageAlias = 0;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Property<Person>(p => p.Age).As(() => ageAlias));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedProperty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Property("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.Property(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
