
using System;
using System.Collections.Generic;

namespace NHibernate.LambdaExtensions.Test
{

    public enum PersonGender
    {
        Male = 1,
        Female = 2,
    }

    public class Person
    {
        public static string StaticName;
        public string Name { get; set; }
        public int Age { get; set; }
        public Person Father { get; set; }
        public PersonGender Gender { get; set; }
        public IList<Child> Children { get; set; }
        public IList<Person> PersonList { get; set; }
        public bool IsParent { get; set; }
        public int? NullableAge { get; set; }
        public PersonGender? NullableGender { get; set; }
        public bool? NullableIsParent { get; set; }
        public byte NumberOfFingers { get; set; }
    }

    public class Child
    {
        public string Nickname { get; set; }
    }

}

