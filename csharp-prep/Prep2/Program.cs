using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter Grade Percentage :::");
        int gradPercent = Convert.ToInt32(Console.ReadLine());

        string letter;
        string passed = "Congratulations! You have passed the course.\n";
        string failed = "Keep working hard! Better luck next time!.\n";

        if(gradPercent >= 90)
        {
            letter = "A";
        }
        else if(gradPercent >= 80)
        {
            letter = "B";
        }
        else if(gradPercent >= 70)
        {
            letter = "C";
        }
        else if(gradPercent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        Console.WriteLine("\nYour grade is: " + letter);

        if (gradPercent >= 70)
        {
            Console.WriteLine(passed);
        }
        else
        {
            Console.WriteLine(failed);
        }
    }
}