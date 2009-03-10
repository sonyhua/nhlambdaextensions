
using System;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestCriteria : TestBase
    {

        [Test]
        public void Test_CreateCriteriaWithAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias");

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Eq()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Eq("Name", "test name"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_EqAlternativeSyntax()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Eq("Name", "test name"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add((Person p) => p.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_EqWithAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .Add(Restrictions.Eq("personAlias.Name", "test name"));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .Add(() => personAlias.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_EqUsingMetaProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .Add(Restrictions.Eq("personAlias.class", typeof(Person)));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .Add(() => personAlias.GetType() == typeof(Person));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestExpressionCombinations()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Gt("Age", 10))
                    .Add(Restrictions.Not(Restrictions.Eq("Name", "test name")))
                    .Add(Restrictions.Ge("Age", 10))
                    .Add(Restrictions.Lt("Age", 10))
                    .Add(Restrictions.Le("Age", 10));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Age > 10)
                    .Add<Person>(p => p.Name != "test name")
                    .Add<Person>(p => p.Age >= 10)
                    .Add<Person>(p => p.Age < 10)
                    .Add<Person>(p => p.Age <= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Order()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .AddOrder(Order.Desc("Name"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .AddOrder<Person>(p => p.Name, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestOrderByInt32Property()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .AddOrder(Order.Asc("Age"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .AddOrder<Person>(p => p.Age, Order.Asc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_OrderUsingAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .AddOrder(Order.Asc("personAlias.Name"))
                    .AddOrder(Order.Desc("personAlias.Age"));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .AddOrder(() => personAlias.Name, Order.Asc)
                    .AddOrder(() => personAlias.Age, Order.Desc);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAssociation()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children")
                        .Add(Restrictions.Eq("Nickname", "test"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAssociationWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAssociationWithAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children", "childAlias")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children, () => childAlias)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAssociationWithAliasAndJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children", "childAlias", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children, () => childAlias, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociation()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateCriteria("personAlias.Children")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociationWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateCriteria("personAlias.Children", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateCriteriaAliasAssociationWithAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateCriteria("personAlias.Children", "childAlias")
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAliasAssociationWithAliasAndJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateCriteria("personAlias.Children", "childAlias", JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias, JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias");

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestEqOnAliasNestedProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Restrictions.Eq("fatherAlias.Father.Name", "test name"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                .CreateAlias<Person>(x=>x.Father, ()=>fatherAlias)
                .Add(() => fatherAlias.Father.Name == "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAliasWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias", JoinType.LeftOuterJoin);

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias, JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestCreateAliasUsingAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateAlias("personAlias.Father", "fatherAlias");

            Person personAlias = null;
            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAliasUsingAliasWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateAlias("personAlias.Father", "fatherAlias", JoinType.LeftOuterJoin);

            Person personAlias = null;
            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias, JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_SetFetchMode()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .SetFetchMode("Father", FetchMode.Eager);

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .SetFetchMode<Person>(p => p.Father, FetchMode.Eager);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestSetFetchModeUsingAlias()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .SetFetchMode("fatherAlias", FetchMode.Eager);

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .SetFetchMode(() => fatherAlias, FetchMode.Eager);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_SetLockMode()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .SetLockMode("fatherAlias", LockMode.Upgrade);

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .SetLockMode(() => fatherAlias, LockMode.Upgrade);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedEqProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Children", "childAlias")
                    .Add(Expression.EqProperty("Name", "childAlias.Nickname"));

            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Children, () => childAlias)
                    .Add<Person>(p => p.Name == childAlias.Nickname);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestAliasedNotEqProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Children", "childAlias")
                    .Add(Expression.NotEqProperty("Name", "childAlias.Nickname"));

            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Children, () => childAlias)
                    .Add<Person>(p => p.Name != childAlias.Nickname);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestAliasedPropertyCombinations()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.GtProperty("Age", "fatherAlias.Age"))
                    .Add(Expression.GeProperty("Age", "fatherAlias.Age"))
                    .Add(Expression.LtProperty("Age", "fatherAlias.Age"))
                    .Add(Expression.LeProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age > fatherAlias.Age)
                    .Add<Person>(p => p.Age >= fatherAlias.Age)
                    .Add<Person>(p => p.Age < fatherAlias.Age)
                    .Add<Person>(p => p.Age <= fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void TestGetCriteriaByAlias()
        {
            Person personAlias = null;
            Person fatherAlias = null;
            ICriteria criteria = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias);

            Assert.AreEqual("personAlias", criteria.GetCriteriaByAlias(() => personAlias).Alias);
            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByAlias(() => fatherAlias).Alias);
        }

        [Test]
        public void TestGetCriteriaByPath()
        {
            Person fatherAlias = null;
            ICriteria criteria = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria<Person>(p => p.Father, () => fatherAlias);

            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByPath<Person>(p => p.Father).Alias);
        }

        [Test]
        public void TestGetCriteriaByPathUsingAlias()
        {
            Person personAlias = null;
            Person fatherAlias = null;
            ICriteria criteria = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Father, () => fatherAlias);

            Assert.AreEqual("fatherAlias", criteria.GetCriteriaByPath(() => personAlias.Father).Alias);
        }

    }

}
