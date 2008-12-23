
using System;
using System.Collections;
using System.Reflection;

using NHibernate.Criterion; 
using NHibernate.Impl;

using NUnit.Framework;

using Rhino.Mocks;

namespace NHibernate.LambdaExtensions.Test
{

    public class TestBase
    {

        private ICriteria CreateCriteriaStub(System.Type persistentType)
        {
            return new CriteriaImpl(persistentType, null);
        }

        private ICriteria CreateCriteriaStub(System.Type persistentType, string alias)
        {
            return new CriteriaImpl(persistentType, alias, null);
        }

        protected ISession CreateSession()
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

        private void AssertFetchModesAreEqual(IDictionary expected, IDictionary actual)
        {
            Assert.AreEqual(expected.Keys.Count, actual.Keys.Count, "FetchModes count mismatch");
            foreach (string expectedAlias in expected.Keys)
            {
                Assert.IsTrue(actual.Contains(expectedAlias), "Expected alias " + expectedAlias);
                FetchMode expectedFetchMode = (FetchMode)expected[expectedAlias];
                FetchMode actualFetchMode = (FetchMode)actual[expectedAlias];
                Assert.AreEqual(expectedFetchMode, actualFetchMode);
            }
        }

        private void AssertLockModesAreEqual(IDictionary expected, IDictionary actual)
        {
            Assert.AreEqual(expected.Keys.Count, actual.Keys.Count, "LockModes count mismatch");
            foreach (string expectedAlias in expected.Keys)
            {
                Assert.IsTrue(actual.Contains(expectedAlias), "Expected alias " + expectedAlias);
                LockMode expectedLockMode = (LockMode)expected[expectedAlias];
                LockMode actualLockMode = (LockMode)actual[expectedAlias];
                Assert.AreEqual(expectedLockMode, actualLockMode);
            }
        }

        private void AssertProjectionsAreEqual(IProjection expected, IProjection actual)
        {
            if (expected == null)
            {
                Assert.IsNull(actual, "expected no projection");
                return;
            }

            Assert.IsNotNull(actual, "expected projection, got null");

            Assert.AreEqual(expected.ToString(), actual.ToString());
            if (expected is ProjectionList)
            {
                ProjectionList expectedList = (ProjectionList)expected;
                ProjectionList actualList = (ProjectionList)actual;
                Assert.AreEqual(expectedList.Length, actualList.Length, "Projection count mismatch");
                for (int i=0; i<expectedList.Length; i++)
                {
                    AssertProjectionsAreEqual(expectedList[i], actualList[i]);
                }
            }
        }

        private string FindPropertyName(PropertySubqueryExpression subqueryExpression)
        {
            FieldInfo fieldInfo = typeof(PropertySubqueryExpression).GetField("propertyName", BindingFlags.NonPublic | BindingFlags.Instance);
            return (string) fieldInfo.GetValue(subqueryExpression);
        }

        private void AssertRestrictonsAreEqual(IList expected, IList actual)
        {
            Assert.AreEqual(expected.Count, actual.Count, "Restrictons count mismatch");
            for (int i=0; i<expected.Count; i++)
            {
                CriteriaImpl.CriterionEntry expectedCriterionEntry = (CriteriaImpl.CriterionEntry)expected[i];
                CriteriaImpl.CriterionEntry actualCriterionEntry = (CriteriaImpl.CriterionEntry)actual[i];

                if (expectedCriterionEntry.Criterion is PropertySubqueryExpression)
                {
                    string expectedProperty = FindPropertyName((PropertySubqueryExpression) expectedCriterionEntry.Criterion);
                    string actualProperty = FindPropertyName((PropertySubqueryExpression) actualCriterionEntry.Criterion);
                    Assert.AreEqual(expectedProperty, actualProperty);
                }
            }
        }

        protected void AssertCriteriaAreEqual(ICriteria expected, ICriteria actual)
        {
            expected = expected.GetCriteriaByAlias(expected.RootAlias);
            actual = actual.GetCriteriaByAlias(actual.RootAlias);
            Assert.AreEqual(expected.CriteriaClass, actual.CriteriaClass);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreEqual(expected.Alias, actual.Alias);
            AssertFetchModesAreEqual(expected.FetchModes, actual.FetchModes);
            AssertLockModesAreEqual(expected.LockModes, actual.LockModes);
            AssertProjectionsAreEqual(expected.Projection, actual.Projection);
            AssertRestrictonsAreEqual(expected.Restrictions, actual.Restrictions);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count, "Subcriteria count mismatch");

            for (int i=0; i<expected.SubcriteriaList.Count; i++)
            {
                CriteriaImpl.Subcriteria expectedSubcriteria = (CriteriaImpl.Subcriteria)expected.SubcriteriaList[i];
                CriteriaImpl.Subcriteria actualSubcriteria = (CriteriaImpl.Subcriteria)actual.SubcriteriaList[i];
                Assert.AreEqual(expectedSubcriteria.Alias, actualSubcriteria.Alias);
                Assert.AreEqual(expectedSubcriteria.Path, actualSubcriteria.Path);
                Assert.AreEqual(expectedSubcriteria.JoinType, actualSubcriteria.JoinType);
                Assert.AreEqual(expectedSubcriteria.ToString(), actualSubcriteria.ToString());
            }
        }

        protected void AssertCriteriaAreEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            expected = expected.GetCriteriaByAlias(expected.RootAlias);
            actual = actual.GetCriteriaByAlias(actual.RootAlias);
            Assert.AreEqual(expected.CriteriaClass, actual.CriteriaClass);
            Assert.AreEqual(expected.ToString(), actual.ToString());
            Assert.AreEqual(expected.Alias, actual.Alias);
            AssertFetchModesAreEqual(expected.FetchModes, actual.FetchModes);
            AssertProjectionsAreEqual(expected.Projection, actual.Projection);
            AssertRestrictonsAreEqual(expected.Restrictions, actual.Restrictions);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count, "Subcriteria count mismatch");

            for (int i=0; i<expected.SubcriteriaList.Count; i++)
            {
                CriteriaImpl.Subcriteria expectedSubcriteria = (CriteriaImpl.Subcriteria)expected.SubcriteriaList[i];
                CriteriaImpl.Subcriteria actualSubcriteria = (CriteriaImpl.Subcriteria)actual.SubcriteriaList[i];
                Assert.AreEqual(expectedSubcriteria.Alias, actualSubcriteria.Alias);
                Assert.AreEqual(expectedSubcriteria.Path, actualSubcriteria.Path);
                Assert.AreEqual(expectedSubcriteria.JoinType, actualSubcriteria.JoinType);
                Assert.AreEqual(expectedSubcriteria.ToString(), actualSubcriteria.ToString());
            }
        }

    }

}
