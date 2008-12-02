
using System;
using System.Collections.Generic;

namespace NHibernate.LambdaExtensions.Test
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Father { get; set; }
        public IList<Child> Children { get; set; }
    }

    public class Child
    {
        public string Nickname { get; set; }
    }

}

