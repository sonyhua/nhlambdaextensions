
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
                        .Add<Child>(p => p.Nickname == "test");

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
                        .Add<Child>(p => p.Nickname == "test");

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
                        .Add<Child>(p => p.Nickname == "test");

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
                        .Add<Child>(p => p.Nickname == "test");

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

    }

}
