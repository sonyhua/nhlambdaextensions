
using System;

using NHibernate.Criterion;

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
        public void Test_CreateDetachedCriteriaAssociation()
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
        public void Test_CreateCriteriaAssociationWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateCriteria("Children", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociationWithAlias()
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
                    .CreateCriteria("Children", "childAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria((Person p) => p.Children, () => childAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateDetachedCriteriaAliasAssociation()
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
        public void Test_CreateCriteriaAliasAssociationWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAliasAssociationWithAlias()
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
        public void Test_CreateCriteriaAliasAssociationWithAliasAndJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateCriteria("personAlias.Children", "childAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
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
        public void Test_CreateAliasWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateAlias("Father", "fatherAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

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
        public void Test_CreateAliasFromAliasWithJoinType()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .CreateAlias("personAlias.Father", "fatherAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            Person personAlias = null;
            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_SetFetchMode()
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

    }

}
