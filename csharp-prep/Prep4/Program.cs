using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int quitNumber = 0; 
        List<int> numbers = new List<int>(); // Empty List variables to hold numbers
        int sum = 0; // set sum to zero at intial stage
        int count = 0;  // Keeping track of numbers
        int maxNum = int.MinValue; // initial maxNum variable 

        while(true) 
        {
            // Prompt user for number continously untill 0 is entered
            Console.Write("\nEnter a number (type 0 to quit) :::");
            int number = int.Parse(Console.ReadLine()); 

            if (number == quitNumber) // break out of the loop and proceed to calculate numbers
            {
                break; 
            }

            // Add numbers to the list and calculate sum
            numbers.Add(number);
            sum += number;
            count++;

            // Check for higher number
            if (number > maxNum)
            {
                maxNum = number;
            }
        }

        if (count > 0) // Check if there are numbers in the list for calculation
        {
            double average = (double)sum / count; // Calculate average
            Console.WriteLine($"\nSum: {sum}\nAverage: {average}\nMax Number: {maxNum}\n");
        }
        else
        {
            Console.WriteLine("\nNo numbers were entered for computation.");
        }
    }
}
