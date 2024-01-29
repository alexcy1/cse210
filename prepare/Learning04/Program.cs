using System;

namespace Learning04
{
class Program
    {
        static void Main()
        {
            Student student = new Student("Brigham", "234");
            string name = student.GetName();
            string number = student.GetNumber();
            
            Console.WriteLine("");
            Console.WriteLine(name);
            Console.WriteLine(number);

            // Create a base "Assignment" object
            Assignment a1 = new Assignment("Samuel Bennett", "Multiplication");
            Console.WriteLine(a1.GetSummary());

            // Now create the derived class Assignments for Math Assignment and Writing Assignments
            
            // Math Assignment
            MathAssignment math = new MathAssignment("Roberto Rodriguez", "Fraction", "7.3", "8-19");
            string assignment = math.GetSummary();
            string mathAssignment = math.GetHomeworkList();
            Console.WriteLine(assignment);
            Console.WriteLine(mathAssignment);

            // Writing Assignments
            WrittenAssignment writtenAssignment = new WrittenAssignment("Mary Waters", "European History", "The Causes of World War II by Mary Waters");
            string written = writtenAssignment.GetSummary();
            string writtenTitle = writtenAssignment.GetWritingInformation();
            Console.WriteLine(written);
            Console.WriteLine(writtenTitle);

        }
    }
}