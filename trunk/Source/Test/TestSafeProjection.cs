
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

        [Test]
        public void Test_Avg()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Avg("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Avg<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AvgUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Avg("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.Avg(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Count()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Count("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Count<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CountUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Count("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.Count(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CountDistinct()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.CountDistinct("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.CountDistinct<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CountDistinctUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.CountDistinct("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.CountDistinct(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_GroupProperty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.GroupProperty("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.GroupProperty<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_GroupPropertyUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.GroupProperty("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.GroupProperty(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Max()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Max("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Max<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_MaxUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Max("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.Max(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Min()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Min("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(SafeProjection.Min<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_MinUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Min("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(SafeProjection.Min(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
