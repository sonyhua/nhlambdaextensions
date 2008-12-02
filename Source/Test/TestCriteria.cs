
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
            Assert.AreEqual(expected.Alias, actual.Alias);
            Assert.AreEqual(expected.CriteriaClass, actual.CriteriaClass);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count);
            Assert.AreEqual(expected.ToString(), actual.ToString());

            for (int i=0; i<expected.SubcriteriaList.Count; i++)
            {
                Impl.CriteriaImpl.Subcriteria expectedSubcriteria = (Impl.CriteriaImpl.Subcriteria)expected.SubcriteriaList[i];
                Impl.CriteriaImpl.Subcriteria actualSubcriteria = (Impl.CriteriaImpl.Subcriteria)actual.SubcriteriaList[i];
                Assert.AreEqual(expectedSubcriteria.Alias, actualSubcriteria.Alias);
                Assert.AreEqual(expectedSubcriteria.Path, actualSubcriteria.Path);
                Assert.AreEqual(expectedSubcriteria.JoinType, actualSubcriteria.JoinType);
                Assert.AreEqual(expectedSubcriteria.ToString(), actualSubcriteria.ToString());
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
        public void Test_EqAlternativeSyntax()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .Add(Restrictions.Eq("Name", "test name"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .Add((Person p) => p.Name == "test name");

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

        [Test]
        public void Test_CreateCriteriaAssociationWithJoinType()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateCriteria("Children", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateCriteria((Person p) => p.Children, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(p => p.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociationWithAlias()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateCriteria("Children", "childAlias")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateCriteria((Person p) => p.Children, () => childAlias)
                        .Add<Child>(p => p.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociationWithAliasAndJoinType()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateCriteria("Children", "childAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateCriteria((Person p) => p.Children, () => childAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(p => p.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAlias()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias");

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAliasWithJoinType()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedEqProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Children", "childAlias")
                    .Add(Expression.EqProperty("Name", "childAlias.Nickname"));

            Child childAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Children, () => childAlias)
                    .Add<Person>(p => p.Name == childAlias.Nickname);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedNotEqProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Children", "childAlias")
                    .Add(Expression.NotEqProperty("Name", "childAlias.Nickname"));

            Child childAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Children, () => childAlias)
                    .Add<Person>(p => p.Name != childAlias.Nickname);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedGtProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.GtProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age > fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedGeProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.GeProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age >= fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedLtProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.LtProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age < fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedLeProperty()
        {
            ICriteria expected =
                CreateCriteria<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.LeProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual =
                CreateCriteria<Person>()
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age <= fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
