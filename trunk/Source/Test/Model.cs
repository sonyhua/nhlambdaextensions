
using System;
using System.Collections.Generic;

namespace NHibernate.LambdaExtensions.Test
{

    public class Person
    {
        public string Name { get; protected set; }
        public int Age { get; protected set; }
        public IList<Child> Children { get; protected set; }
    }

    public class Child
    {
        public string Nickname { get; protected set; }
    }

}

