
using System;

using NHibernate.Criterion; 

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    public class TestBase
    {

        protected void AssertCriteriaAreEqual(ICriteria expected, ICriteria actual)
        {
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

    }

}
