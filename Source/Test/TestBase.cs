
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using NHibernate.Criterion; 
using NHibernate.Impl;

using NUnit.Framework;

using Rhino.Mocks;

namespace NHibernate.LambdaExtensions.Test
{

    public class TestBase
    {

        private Hashtable _visitedObjects = new Hashtable();
        private Stack<string> _fieldPath = new Stack<string>();

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
                .Call(session.CreateCriteria(typeof(object)))
                .IgnoreArguments().Repeat.Any()
                .Do((Func<System.Type, ICriteria>)CreateCriteriaStub);

            Expect
                .Call(session.CreateCriteria(typeof(object), ""))
                .IgnoreArguments().Repeat.Any()
                .Do((Func<System.Type, string, ICriteria>)CreateCriteriaStub);

            mocks.ReplayAll();
            return session;
        }

        private void AssertDictionariesAreEqual(IDictionary expected, IDictionary actual)
        {
            Assert.AreEqual(expected.Keys.Count, actual.Keys.Count, _fieldPath.Peek() + ".Count");
            foreach (object key in expected.Keys)
            {
                if (!actual.Contains(key))
                    Assert.AreEqual(key, null, _fieldPath.Peek() + "[" + key.ToString() + "]");

                AssertObjectsAreEqual(expected[key], actual[key], "[" + key.ToString() + "]");
            }
        }

        private void AssertListsAreEqual(IList expected, IList actual)
        {
            Assert.AreEqual(expected.Count, actual.Count, _fieldPath.Peek() + ".Count");
            for (int i=0; i<expected.Count; i++)
            {
                AssertObjectsAreEqual(expected[i], actual[i], "[" + i.ToString() + "]");
            }
        }

        private void PushName(string name)
        {
            if (_fieldPath.Count == 0)
            {
                _fieldPath.Push(name);
            }
            else
            {
                _fieldPath.Push(_fieldPath.Peek() + name);
            }
        }

        private void AssertObjectsAreEqual(object expected, object actual, string name)
        {
            PushName(name);
            string fieldPath = _fieldPath.Peek();

            if (expected == null)
            {
                Assert.AreEqual(expected, actual, fieldPath);
                _fieldPath.Pop();
                return;
            }

            System.Type expectedType = expected.GetType();
            Assert.AreEqual(expectedType, actual.GetType(), fieldPath);

            if ((expectedType.IsValueType)
                || (expected is System.Type)
                || (expected is string))
            {
                Assert.AreEqual(expected, actual, fieldPath);
                _fieldPath.Pop();
                return;
            }

            if (_visitedObjects.Contains(expected))
            {
                _fieldPath.Pop();
                return;
            }

            _visitedObjects.Add(expected, null);

            if (expected is IDictionary)
            {
                AssertDictionariesAreEqual((IDictionary)expected, (IDictionary)actual);
                _fieldPath.Pop();
                return;
            }

            if (expected is IList)
            {
                AssertListsAreEqual((IList)expected, (IList)actual);
                _fieldPath.Pop();
                return;
            }

            while (expectedType != null)
            {
                foreach (FieldInfo fieldInfo in expectedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    AssertObjectsAreEqual(fieldInfo.GetValue(expected), fieldInfo.GetValue(actual), "." + fieldInfo.Name);
                }
                expectedType = expectedType.BaseType;
            }

            _fieldPath.Pop();
        }

        private void AssertObjectsAreEqual(object expected, object actual)
        {
            _visitedObjects.Clear();
            _fieldPath.Clear();
            AssertObjectsAreEqual(expected, actual, expected.GetType().Name);
        }

        protected void AssertCriteriaAreEqual(ICriteria expected, ICriteria actual)
        {
            AssertObjectsAreEqual(expected, actual);
        }

        protected void AssertCriteriaAreEqual(DetachedCriteria expected, DetachedCriteria actual)
        {
            AssertObjectsAreEqual(expected, actual);
        }

    }

}
