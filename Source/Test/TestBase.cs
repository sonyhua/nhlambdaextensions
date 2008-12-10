
using System;
using System.Collections;

using NHibernate.Criterion; 

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    public class TestBase
    {

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

        protected void AssertCriteriaAreEqual(ICriteria expected, ICriteria actual)
        {
            expected = expected.GetCriteriaByAlias(expected.RootAlias);
            actual = actual.GetCriteriaByAlias(actual.RootAlias);
            Assert.AreEqual(expected.Alias, actual.Alias);
            AssertFetchModesAreEqual(expected.FetchModes, actual.FetchModes);
            AssertLockModesAreEqual(expected.LockModes, actual.LockModes);
            AssertProjectionsAreEqual(expected.Projection, actual.Projection);
            Assert.AreEqual(expected.CriteriaClass, actual.CriteriaClass);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count, "Subcriteria count mismatch");
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

        protected void AssertCriteriaAreEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            expected = expected.GetCriteriaByAlias(expected.RootAlias);
            actual = actual.GetCriteriaByAlias(actual.RootAlias);
            Assert.AreEqual(expected.Alias, actual.Alias);
            Assert.AreEqual(expected.CriteriaClass, actual.CriteriaClass);
            AssertFetchModesAreEqual(expected.FetchModes, actual.FetchModes);
            AssertProjectionsAreEqual(expected.Projection, actual.Projection);
            Assert.AreEqual(expected.SubcriteriaList.Count, actual.SubcriteriaList.Count, "Subcriteria count mismatch");
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

    }

}
