/*

    To address the stretch challenge, which encouraged incorporating additional 
    features, I opted to enable the program to load prompts and questions from files 
    instead of requiring the modification of the class file each time a user wants 
    to add more data. 
    
    This approach simplifies the process of structuring additional prompts and questions, 
    making it more user-friendly. Furthermore, it reduces the time involved and mitigates 
    potential errors associated with directly loading data into the class file.

    If the file does not exist, the program will automatically load default prompts,
     and questions, save them to a file, and use the file for loading prompts in subsequent program runs.

*/


using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
            {
                Console.Clear(); // Clear the console before displaying the menu

                // Menu Option -------------------------------------------------
                Console.WriteLine("Menu Option:");
                Console.WriteLine(" 1. Start breathing Activity");
                Console.WriteLine(" 2. Start reflection Activity");
                Console.WriteLine(" 3. Start listing Activity");
                Console.WriteLine(" 4. Quit");

                Console.Write("Select a choice from the menu :::");
                string input = Console.ReadLine(); // Read user entry 

                if (string.IsNullOrWhiteSpace(input)) // Validate entry
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                    Thread.Sleep(3000);
                    continue;
                }

                if (int.TryParse(input, out int choice)) // Convert string to an integer
                {
                    Activity selectedActivity = null; // Initializes the selectedActivity variable to null

                    switch (choice) // Converted integer hold a true value for variable choice
                    {
                        case 1:
                            selectedActivity = new BreathingActivity(); // Assign the class instance to 1
                            break;

                        case 2:
                            selectedActivity = new ReflectingActivity(); // Assign the class instance to 2
                            break;

                        case 3:
                            selectedActivity = new ListingActivity(); // Assign the class instance to 3
                            break;

                        case 4:
                            Console.WriteLine("\nExiting the program. Goodbye!\n"); // Exit the program
                            Thread.Sleep(2000); // 2 seconds delay
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please choose again."); // Out of range option 
                            Thread.Sleep(3000); // 3 seconds delay
                            break;
                    }

                    if (selectedActivity != null)
                    {
                        selectedActivity.Run(); // Execute if selectedActivity is not null
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice."); // Invalid choice alert
                }
            }
    }
}