using System;
using System.Threading;

class BreathingActivity : Activity
{

    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.\n")
    {
        // Additional initialization for BreathingActivity
    }
    

    public override void Run()
    {
        int _SpinnerDuration = GetSpinnerDuration();
        int _CountdownDuration = GetCountDownDuration();

        DisplayStartingMessage();
        //GetDurationFromUser();
        Console.Clear(); // Clear the console before displaying the breathing activity

        Console.WriteLine("Get ready...");
        ShowSpinner(_SpinnerDuration); // Adjust the duration as needed
        
        for (int i = 0; i < _duration; i++)
        {
            Console.Write("\nBreath in...");
            Countdown(_CountdownDuration); // Countdown from 3 to 1
            Console.WriteLine();

            Console.Write("Now breath out...");
            Countdown(_CountdownDuration); // Countdown from 3 to 1
            Console.WriteLine();
        }
        
        DisplayEndingMessage();
    }

    private void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000); // Adjust the duration as needed
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);

            if (i == 1)
            {
                Console.Write("  "); // Replace the last number with two spaces
                Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
            }
        }
    }
}

