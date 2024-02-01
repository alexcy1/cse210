
using System;
using System.Collections.Generic;
using System.Threading;

abstract class Activity // Common attributes for all derived classes
{
    protected string _name;
    protected string _description;
    protected int _duration;
    protected int _CountdownDuration;


    public Activity(string name, string description) // Constructor for initializing Activity attributes
    {
        _name = name;
        _description = description;
        _duration = 0;
        _CountdownDuration = 0;
    }


    protected int GetSpinnerDuration() // Spinner duration Method
    {
        return 5;
    }

    protected int GetCountDownDuration() // Count down duration Method
    {
        return 6; 
    }


    public virtual void DisplayStartingMessage() // Method to display the starting message and get user input for session duration for all activities
    {
        Console.Clear();
        Console.WriteLine($"\nWelcome to the {_name} Activity.\n");
        Console.WriteLine(_description);
        Console.Write("How long, in seconds would you like your session? :::");
        _duration = Convert.ToInt32(Console.ReadLine());
    }


    public void DisplayEndingMessage() // Method to display ending message for all activities
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(GetSpinnerDuration());
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity");
        ShowSpinner(GetSpinnerDuration());
    }


    public void ShowSpinner(int seconds) // Method to display a spinner animation for all activities
    {
        int originalLeft = Console.CursorLeft;

        for (int i = 0; i < seconds; i++)
        {
            Console.Write("/");
            Thread.Sleep(250); // Set duration
            Console.SetCursorPosition(originalLeft, Console.CursorTop);

            Console.Write("-");
            Thread.Sleep(250); // Set duration
            Console.SetCursorPosition(originalLeft, Console.CursorTop);

            Console.Write("\\");
            Thread.Sleep(250); // Set duration
            Console.SetCursorPosition(originalLeft, Console.CursorTop);

            Console.Write("|");
            Thread.Sleep(250); // Set duration
            Console.SetCursorPosition(originalLeft, Console.CursorTop);
        }

        // Clear the last character
        Console.SetCursorPosition(originalLeft, Console.CursorTop);
        Console.Write(" ");
        Console.WriteLine();
    }


    public void Countdown(int seconds, string message = "") // Method for countdown in seconds for all activities
    {
        Console.Write($"You may begin in: {message}");

        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); // Set duration
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);

            if (i == 1)
            {
                Console.Write("  "); // Attempt to replace the last number with two spaces
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            }
        }

        Console.WriteLine();
    }
    public abstract void Run(); // Declaring an abstract method
}

