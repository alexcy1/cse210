using System;

class Program
{
    static void Main(string[] args)
    {
        // Get Frist Name
        Console.Write("What is your first name? :::");
        string fristName = Console.ReadLine();

         // Get last Name
        Console.Write("What is your last name? :::");
        string lastName = Console.ReadLine();

         // Display Result
        Console.WriteLine($"\nYour name is {lastName}, {fristName} {lastName}.\n");
    }
}