using System;

namespace Learning04
{
    public class WrittenAssignment : Assignment
    {
        private string _title;

        public WrittenAssignment(string studentName, string topic, string title) : base(studentName, topic)
        {
            _title = title;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetWritingInformation()
        {
            // Notice that we are calling the getter here because _studentName is private in the base class
            string studentName = GetStudentName();
            return $"{_title} by {studentName}\n";
        }
    }
}
