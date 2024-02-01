using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class ReflectingActivity : Activity // Derived class inheriting from Activity with unique attributes
{
    private List<string> _prompts;
    private List<string> _questions;
    private string promptsFilePath = "prompts.txt";
    private string questionsFilePath = "questions.txt";


/*
    Constructor for ReflectingActivity class, which initializes the base class
    with a specific name and description. Additionally, it loads prompts and questions
    from files during object creation.
*/
    public ReflectingActivity() : base("Reflecting", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.\n")
    {
        // Load prompts and questions from files
        LoadPrompts();
        LoadQuestions();
    }


    private void LoadPrompts() // Method to load prompts from file as stretch challenge
    {
        if (File.Exists(promptsFilePath))
        {
            _prompts = File.ReadAllLines(promptsFilePath).ToList();
        }
        else
        {
            Console.WriteLine("File does not exisit. Now loading default prompts...");
            // Or I can use default prompts here if the file doesn't exist
            _prompts = new List<string> { "Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless." };
        }
    }


    private void LoadQuestions() // Method to load questions from file as stretch challenge
    {
        if (File.Exists(questionsFilePath))
        {
            _questions = File.ReadAllLines(questionsFilePath).ToList();
        }
        else
        {
            Console.WriteLine("File does not exisit. Now loading default questions...");
            // Or I can use default questions here if the file doesn't exist
            _questions = new List<string> { "Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?" };
        }
    }


    private void SavePrompts() // Write default prompts to a file
    {
        File.WriteAllLines(promptsFilePath, _prompts);
    }

    private void SaveQuestions() // Write default questions to a file
    {
        File.WriteAllLines(questionsFilePath, _questions);
    }


    public override void Run() // Method that also calls methods from the base class
    {
        int spinnerDuration = GetSpinnerDuration();
        int countdownDuration = GetCountDownDuration();
        DisplayStartingMessage();

        Console.Clear(); // Clear the console before displaying the activity
        Console.WriteLine("Get ready...");
        ShowSpinner(spinnerDuration); // Set duration

        // Display a random prompt
        string prompt = GetRandomPrompt();
        Console.WriteLine($"\nConsider the following prompt:\n\n--- {prompt} ---\n");

        Console.Write("When you have something in mind, press enter to continue :::");
        Console.ReadLine(); // Wait for user input

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience:");
        Countdown(countdownDuration); // Countdown to 1
        Console.Clear(); // Clear the console before displaying questions

        // Display prompts based on the user specified session duration
        for (int i = 0; i < Math.Min(_duration, _questions.Count); i++)
        {
            Console.Write($"> {_questions[i]} ");

            // Show spinner for each question
            ShowSpinner(spinnerDuration);
        }

        // Save prompts and questions after the activity
        SavePrompts();
        SaveQuestions();

        DisplayEndingMessage();
    }


    private string GetRandomPrompt() // Method for selecting a random prompt
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}
