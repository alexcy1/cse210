
using System;
using System.Collections.Generic;
using System.IO;

class ListingActivity : Activity // Derived class inheriting from Activity with unique attributes
{
    private int _count;
    private List<string> _prompts;


    // Constructor for ListingActivity, inheriting from Activity and initializing unique attributes
    public ListingActivity() : base("Listing", "This activity will help you reflect on positive aspects of your life by listing items.\n")
    {
        _count = 0;
        _prompts = LoadPromptsFromFile("list-prompts.txt");
    }


    public override void Run() // Method that also calls methods from the base class
    {
        DisplayStartingMessage(); // Call from the base class

        Console.Clear();
        int _SpinnerDuration = GetSpinnerDuration(); // Call from the base class and assign to variable _SpinnerDuration
        int _CountdownDuration = GetCountDownDuration(); // Call from the base class and assign to variable _CountdownDuration

        Console.WriteLine("Get ready...");
        ShowSpinner(_SpinnerDuration); // Call ShowSpinner() method from the base class

        // Display a random question
        string randomQuestion = GetRandomPrompt();
        Console.WriteLine($"List as many responses as you can to the following prompt:\n--- {randomQuestion} ---");

        Countdown(_CountdownDuration); // Call Countdown method from the base class
        Console.WriteLine("");

        // Display prompts based on the user specified session duration
        for (int i = 0; i < _duration; i++)
        {
            Console.Write("> ");
            GetListFromUser();
            _count++;
        }

        Console.WriteLine($"\nYou listed {_count} items."); // Displaying the specified session here

        DisplayEndingMessage(); // A call from Activity or base class
    }


    // Method to load prompts from a file
    private List<string> LoadPromptsFromFile(string fileName)
    {
        List<string> prompts = new List<string>();

        try
        {
            // Read all lines from the file and add them to the prompts list
            prompts.AddRange(File.ReadAllLines(fileName));
        }
        catch (IOException e)
        {
            Console.WriteLine($"Error reading prompts from file: {e.Message}");
        }

        return prompts;
    }


    public string GetRandomPrompt() // Method for selecting a random prompt
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }


    public void GetListFromUser() // Method to get a list of items from the user
    {
        string item = Console.ReadLine();
        // Reserved to do something with the entered item maybe in the future
        // I could also do something here for stretch challeng if time permit
    }
}


