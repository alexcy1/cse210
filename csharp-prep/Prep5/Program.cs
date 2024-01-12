using System;

class Program
{
    static void Main(string[] args)
    {
        // Call All Methods
        DisplayWelcome();
        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(userNumber);
        DisplayResult(userName, squaredNumber);
    }

    // Welcome Message
    static void DisplayWelcome()
    {
        Console.WriteLine("\nWelcome to the program!");
    }

    // User Name
    static string PromptUserName()
    {
        Console.Write("Please enter your name :::");
        string userName = Console.ReadLine();
        return userName;
    }

    // User Favourite Number
     static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());

        return number;
    }

    // Square Number
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // Display Result
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"\n{name}, the square of your number is {square}\n");
    }


}