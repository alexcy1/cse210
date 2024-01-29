using System;

namespace Learning04
{
    public class Student : Person
    {
        private string _number;

        // calling the parent constructor using "base"!
        public Student(string name, string number) : base(name)
        {
        _number = number;
        }

        public string GetNumber()
        {
            return _number;
        }
    }
}
