
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestLambdaProjection : TestBase
    {

        [Test]
        public void Test_Property()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Property("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.Property<Person>(p => p.Name));

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
                    .SetProjection(LambdaProjection.Alias(LambdaProjection.Property<Person>(p => p.Name), () => nameAlias));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestPropertyAliasWithValueType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Alias(Projections.Property("Age"), "ageAlias"));

            int ageAlias = 0;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.Alias(LambdaProjection.Property<Person>(p => p.Age), () => ageAlias));

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
                    .SetProjection(LambdaProjection.Property<Person>(p => p.Age).As(() => ageAlias));

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
                    .SetProjection(LambdaProjection.Property(() => personAlias.Age));

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
                    .SetProjection(LambdaProjection.Avg<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestAvgUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Avg("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.Avg(() => personAlias.Age));

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
                    .SetProjection(LambdaProjection.Count<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCountUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Count("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.Count(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCountDistinct()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.CountDistinct("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.CountDistinct<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCountDistinctUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.CountDistinct("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.CountDistinct(() => personAlias.Age));

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
                    .SetProjection(LambdaProjection.GroupProperty<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestGroupPropertyUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.GroupProperty("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.GroupProperty(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestMax()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Max("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.Max<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestMaxUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Max("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.Max(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestMin()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Min("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.Min<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestMinUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Min("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.Min(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSum()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetProjection(Projections.Sum("Age"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetProjection(LambdaProjection.Sum<Person>(p => p.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSumUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .SetProjection(Projections.Sum("personAlias.Age"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .SetProjection(LambdaProjection.Sum(() => personAlias.Age));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
