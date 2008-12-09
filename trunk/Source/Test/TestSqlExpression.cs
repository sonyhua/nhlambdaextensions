
using System;

using NHibernate;
using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestRestrictions : TestBase
    {

        [Test]
        public void Test_Between()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Between("Age", 5, 10));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.Between<Person>(p => p.Age, 5, 10));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_BetweenUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.Between("personAlias.Age", 5, 10));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.Between(() => personAlias.Age, 5, 10));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Like()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Like("Name", "%test%"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.Like<Person>(p => p.Name, "%test%"));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_LikeUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.Like("personAlias.Name", "%test%"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.Like(() => personAlias.Name, "%test%"));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNull()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNull("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.IsNull<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNullUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.IsNull("personAlias.Name"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.IsNull(() => personAlias.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotNull()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNotNull("Name"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.IsNotNull<Person>(p => p.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotNullUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.IsNotNull("personAlias.Name"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.IsNotNull(() => personAlias.Name));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsEmpty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsEmpty("Children"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.IsEmpty<Person>(p => p.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsEmptyUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.IsEmpty("personAlias.Children"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.IsEmpty(() => personAlias.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotEmpty()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.IsNotEmpty("Children"));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.IsNotEmpty<Person>(p => p.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_IsNotEmptyUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.IsNotEmpty("personAlias.Children"));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.IsNotEmpty(() => personAlias.Children));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_In()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.In("Name", new string[] { "name1", "name2", "name3" }));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.In<Person>(p => p.Name, new string[] { "name1", "name2", "name3" }));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_InUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.In("personAlias.Name", new string[] { "name1", "name2", "name3" }));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.In(() => personAlias.Name, new string[] { "name1", "name2", "name3" }));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_GenericIn()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.InG<int>("Age", new int[] { 1, 2, 3 }));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.In<Person, int>(p => p.Age, new int[] { 1, 2, 3 }));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_GenericInUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.InG<int>("personAlias.Age", new int[] { 1, 2, 3 }));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.In<int>(() => personAlias.Age, new int[] { 1, 2, 3 }));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Not()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Not(Restrictions.Gt("Age", 5)));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(SqlExpression.Not<Person>(p => p.Age > 5));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_NotUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.Not(Restrictions.Gt("personAlias.Age", 5)));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(SqlExpression.Not(() => personAlias.Age > 5));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_And()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.And(
                        Restrictions.Eq("Name", "test"),
                        Restrictions.Gt("Age", 5)));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.And(
                        SqlExpression.CriterionFor<Person>(p => p.Name == "test"),
                        SqlExpression.CriterionFor<Person>(p => p.Age > 5)));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_AndUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .CreateAlias("Father", "fatherAlias")
                    .Add(Restrictions.And(
                        Restrictions.Eq("Name", "test"),
                        Restrictions.Gt("fatherAlias.Age", 5)));

            Person fatherAlias = null;
            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .CreateCriteria<Person>(p => p.Father, () => fatherAlias)
                    .Add(Restrictions.And(
                        SqlExpression.CriterionFor<Person>(p => p.Name == "test"),
                        SqlExpression.CriterionFor(() => fatherAlias.Age > 5)));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Or()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Or(
                        Restrictions.Eq("Name", "test"),
                        Restrictions.Gt("Age", 5)));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Or(
                        SqlExpression.CriterionFor<Person>(p => p.Name == "test"),
                        SqlExpression.CriterionFor<Person>(p => p.Age > 5)));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Conjunction()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Conjunction()
                            .Add(Restrictions.Eq("Name", "test"))
                            .Add(Restrictions.Gt("Age", 5)));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Conjunction()
                            .Add<Person>(p => p.Name == "test")
                            .Add<Person>(p => p.Age > 5));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_ConjunctionUsingAlias()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>("personAlias")
                    .Add(Restrictions.Conjunction()
                            .Add(Restrictions.Eq("personAlias.Name", "test"))
                            .Add(Restrictions.Gt("personAlias.Age", 5)));

            Person personAlias = null;
            DetachedCriteria actual =
                DetachedCriteria<Person>.Create(() => personAlias)
                    .Add(Restrictions.Conjunction()
                            .Add(() => personAlias.Name == "test")
                            .Add(() => personAlias.Age > 5));

            AssertCriteriaAreEqual(expected, actual);
        }

        [Test]
        public void Test_Disjunction()
        {
            DetachedCriteria expected =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Disjunction()
                            .Add(Restrictions.Eq("Name", "test"))
                            .Add(Restrictions.Gt("Age", 5)));

            DetachedCriteria actual =
                DetachedCriteria.For<Person>()
                    .Add(Restrictions.Disjunction()
                            .Add<Person>(p => p.Name == "test")
                            .Add<Person>(p => p.Age > 5));

            AssertCriteriaAreEqual(expected, actual);
        }

    }

}
