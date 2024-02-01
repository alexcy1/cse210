using System;
using System.Threading;

class BreathingActivity : Activity // Derived class inheriting from Activity 
{

// Constructor for BreathingActivity, inheriting from Activity
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.\n")
    {
        // This could be used for more initialization for BreathingActivity
    }
    

    public override void Run() // Method that also calls methods from the base class
    {
        int _SpinnerDuration = GetSpinnerDuration(); // Call from the base class and assign to variable _SpinnerDuration
        int _CountdownDuration = GetCountDownDuration(); // Call from the base class and assign to variable _CountdownDuration

        DisplayStartingMessage();
        Console.Clear(); // Clear the console before displaying the breathing activity

        Console.WriteLine("Get ready...");
        ShowSpinner(_SpinnerDuration); // Set duration
        
        for (int i = 0; i < _duration; i++)
        {
            Console.Write("\nBreath in...");
            Countdown(_CountdownDuration); // Countdown
            Console.WriteLine();

            Console.Write("Now breath out...");
            Countdown(_CountdownDuration); // Countdown
            Console.WriteLine();
        }
        
        DisplayEndingMessage();
    }


    private void Countdown(int seconds) // Method for 
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); // Set duration
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);

            if (i == 1)
            {
                Console.Write("  "); // Replace the last number with two spaces
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            }
        }
    }
}

