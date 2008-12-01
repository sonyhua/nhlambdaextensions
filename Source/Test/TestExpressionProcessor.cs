
using System;
using System.Linq.Expressions;

using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestExpressionProcessor : TestBase
    {

        [Test]
        public void TestFindMemberExpressionReference()
        {
            Expression<Func<Person, string>> e = (Person p) => p.Name;
            MemberExpression me = ExpressionProcessor.FindMemberExpression(e.Body);
            Assert.AreEqual("Name", me.Member.Name);
        }

        [Test]
        public void TestFindMemberExpressionValue()
        {
            Expression<Func<Person, object>> e = (Person p) => p.Age;
            MemberExpression me = ExpressionProcessor.FindMemberExpression(e.Body);
            Assert.AreEqual("Age", me.Member.Name);
        }

        [Test]
        public void TestEvaluateMemberExpression()
        {
            string testName = "testName";
            ICriterion criterion = ExpressionProcessor.ProcessExpression<Person>(p => p.Name == testName);
            SimpleExpression simpleExpression = (SimpleExpression)criterion;
            Assert.AreEqual("testName", simpleExpression.Value);
        }

        [Test]
        public void TestFindMemberContainerReference()
        {
            Person alias = null;
            Expression<Func<string>> e = () => alias.Name;
            MemberExpression me = ExpressionProcessor.FindMemberContainer(e.Body);
            Assert.AreEqual("alias", me.Member.Name);
        }

        [Test]
        public void TestFindMemberContainerValue()
        {
            Person alias = null;
            Expression<Func<object>> e = () => alias.Age;
            MemberExpression me = ExpressionProcessor.FindMemberContainer(e.Body);
            Assert.AreEqual("alias", me.Member.Name);
        }

    }

}
