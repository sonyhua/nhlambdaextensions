
using System;

using NHibernate.Criterion;
using NHibernate.SqlCommand;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestDetachedCriteria : TestBase
    {

        [Test]
        public void TestAdd()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Eq("Name", "test name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestAddWithAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.Eq("personAlias.Name", "test name"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(() => personAlias.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestOrder()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .AddOrder(Order.Desc("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestOrderUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .AddOrder(Order.Desc("personAlias.Name"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .AddOrder(() => personAlias.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCombinedAddAndOrder()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Eq("Name", "test name"))
                    .AddOrder(Order.Desc("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add<Person>(p => p.Name == "test name")
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateWithAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias");

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateDetachedCriteriaAssociation()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Children")
                        .Add(Restrictions.Eq("Nickname", "test"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAssociationWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Children", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAssociationWithAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Children", "childAlias")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children, () => childAlias)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociationWithAliasAndJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Children", "childAlias", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children, () => childAlias, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateDetachedCriteriaAliasAssociation()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociationWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociationWithAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children", "childAlias")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociationWithAliasAndJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children", "childAlias", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateAlias("Father", "fatherAlias");

            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAliasWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateAlias("Father", "fatherAlias", JoinType.LeftOuterJoin);

            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias, JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAliasFromAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateAlias("personAlias.Father", "fatherAlias");

            Person personAlias = null;
            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAliasFromAliasWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateAlias("personAlias.Father", "fatherAlias", JoinType.LeftOuterJoin);

            Person personAlias = null;
            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias, JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_SetFetchMode()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .SetFetchMode("Father", FetchMode.Eager);

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .SetFetchMode<Person>(p => p.Father, FetchMode.Eager);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSetFetchModeUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .SetFetchMode("fatherAlias", FetchMode.Eager);

            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .SetFetchMode(() => fatherAlias, FetchMode.Eager);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestGetCriteriaByAlias()
        {
            Person personAlias = null;
            Person fatherAlias = null;
            DetachedCriteria criteria =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias);

            Assert.AreEqual("personAlias", criteria.GetCriteriaByAlias(() => personAlias).Alias);
            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByAlias(() => fatherAlias).Alias);
        }

        [Test]
        public void TestGetCriteriaByPath()
        {
            Person fatherAlias = null;
            DetachedCriteria criteria =
                DetachedCriteria.For<Person>()
                    .CreateCriteria<Person>(p => p.Father, () => fatherAlias);

            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByPath<Person>(p => p.Father).Alias);
        }

        [Test]
        public void TestGetCriteriaByPathUsingAlias()
        {
            Person personAlias = null;
            Person fatherAlias = null;
            DetachedCriteria criteria =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Father, () => fatherAlias);

            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByPath(() => personAlias.Father).Alias);
        }

    }

}
