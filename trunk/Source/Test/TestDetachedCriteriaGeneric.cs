
using System;

using NHibernate.Criterion;

using NUnit.Framework;

namespace NHibernate.LambdaExtensions.Test
{

    [TestFixture]
    public class TestDetachedCriteriaGeneric : TestBase
    {

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

    }

}
