
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

using Rhino.Mocks;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestCriteria : TestBase
    {

        private ICriteria CreateCriteriaStub(System.Type persistentType)
        {
            return new NHibernate.Impl.CriteriaImpl(persistentType, null);
        }

        private ICriteria CreateCriteriaStub(System.Type persistentType, string alias)
        {
            return new NHibernate.Impl.CriteriaImpl(persistentType, alias, null);
        }

        private ISession CreateSession()
        {
            MockRepository mocks = new MockRepository();
            ISession session = mocks.Stub<ISession>();

            Expect
                .Call(session.CreateCriteria(null))
                .IgnoreArguments().Repeat.Any()
                .Do((Func<System.Type, ICriteria>)CreateCriteriaStub);

            Expect
                .Call(session.CreateCriteria(null, null))
                .IgnoreArguments().Repeat.Any()
                .Do((Func<System.Type, string, ICriteria>)CreateCriteriaStub);

            mocks.ReplayAll();
            return session;
        }

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
        public void Test_Gt()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Gt("Age", 10));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Age > 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Ne()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Not(Restrictions.Eq("Name", "test name")));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Name != "test name");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Ge()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Ge("Age", 10));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Age >= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Lt()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Lt("Age", 10));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Age < 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Le()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add(Restrictions.Le("Age", 10));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .Add<Person>(p => p.Age <= 10);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_OrderByStringProperty()
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
        public void Test_OrderByInt32Property()
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
        public void Test_CreateCriteriaAssociation()
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
        public void Test_CreateCriteriaAssociationWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
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
        public void Test_CreateCriteriaAssociationWithAliasAndJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria("Children", "childAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateCriteria((Person p) => p.Children, () => childAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAliasAssociation()
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
        public void Test_CreateCriteriaAliasAssociationWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person), "personAlias")
                    .CreateCriteria("personAlias.Children", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add<Child>(c => c.Nickname == "test");

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateCriteriaAliasAssociationWithAlias()
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
                    .CreateCriteria("personAlias.Children", "childAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin)
                        .Add(Restrictions.Eq("Nickname", "test"));

            Person personAlias = null;
            Child childAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateCriteria(() => personAlias.Children, () => childAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
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
        public void Test_CreateAliasWithJoinType()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_CreateAliasUsingAlias()
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
                    .CreateAlias("personAlias.Father", "fatherAlias", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

            Person personAlias = null;
            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person), () => personAlias)
                    .CreateAlias(() => personAlias.Father, () => fatherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin);

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
        public void Test_SetFetchModeUsingAlias()
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
        public void Test_AliasedNotEqProperty()
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
        public void Test_AliasedGtProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.GtProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age > fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedGeProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.GeProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age >= fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedLtProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.LtProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age < fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AliasedLeProperty()
        {
            ICriteria expected = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Expression.LeProperty("Age", "fatherAlias.Age"));

            Person fatherAlias = null;
            ICriteria actual = CreateSession()
                .CreateCriteria(typeof(Person))
                    .CreateAlias<Person>(p => p.Father, () => fatherAlias)
                    .Add<Person>(p => p.Age <= fatherAlias.Age);

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
