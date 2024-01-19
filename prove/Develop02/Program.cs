/*
Exceeding Requirements:

1. Database Persistence:
I implemented seamless integration with SQLite database to enable persistent storage of journal entries.
Users can now save their entries to the database for long-term preservation and accessibility.
Entries can be easily retrieved and viewed from the database, ensuring data integrity and availability.

2. Enhanced Content Management:
I created a Delete class to provide a user-friendly interface for removing individual or group entries from the journal.
This feature empowers users with greater control over their journal content and organization.
It facilitates keeping the journal focused and relevant by allowing for selective removal of entries.
*/

using System;

namespace Develop02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("Welcome To The Journal Program");
            Console.WriteLine("=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");

            string darkGreenColor = "\u001b[32m"; // Colors
            string darkPurpleColor = "\u001b[35m";
            string redColor = "\u001b[31m";
            string resetColor = "\u001b[0m";

            // OBJECT INSTANCES ---------------------------------------------------------------
            PromptGenerator promptGenerator = new PromptGenerator(); // Create PromptGenerator Instance
            Journal journal = new Journal(); // Create Journal Instance
            Delete delete = new Delete("Data Source=journal.db;"); // Create Delete Instance

            while (true)
            {
                // Display Menu --------------------------------------------------------
                Console.WriteLine("\nPlease select one of the following choices: ");
                Console.WriteLine("1. Write to Journal");
                Console.WriteLine("2. Display Journal");
                Console.WriteLine("3. Load Journal");
                Console.WriteLine("4. Save Entry");
                Console.WriteLine("5. Quit Journal");
                Console.WriteLine("6. Delete Journal");

                Console.Write("What would you like to do? :::");
                string userChoice = Console.ReadLine()!;

                if (userChoice == "5") // Quit the program
                {
                    Console.WriteLine($"\n{redColor}Exiting the Journal Program. Goodbye!{resetColor}\n");
                    break;
                }

                switch (userChoice)
                {
                    case "1":
                        // WRITE ----------------------------------------------------------
                        string randomPrompt = promptGenerator.GetRandomPrompt();
                        Console.WriteLine(randomPrompt);

                        Console.Write("::: ");
                        string userJournal = Console.ReadLine()!;

                        Entry newEntry = new Entry(DateTime.Now.ToString("dd/MM/yyyy"), randomPrompt, userJournal);
                        journal.AddEntry(newEntry);
                        break;

                    case "2":
                        // DISPLAY --------------------------------------------------------
                        journal.DisplayAll();
                        break;

                    case "6":
                        // DELETE ---------------------------------------------------------
                        Console.WriteLine("\n1. Delete entire journal");
                        Console.WriteLine("2. Delete individual entry");
                        Console.Write("\nChoose an option :::");
                        string deleteOption = Console.ReadLine()!;
                        
                        if (deleteOption.ToLower() == "q") // Quit deleteing items in the journal and return to the main menu
                        {
                            Console.WriteLine($"{redColor}You quit deleting items in the journal{resetColor}");
                            break;
                        }
                        else if (deleteOption == "1")
                        {
                            // Delete entire entry in the journal
                            delete.DeleteAllItems();
                            Console.WriteLine($"\n{redColor}Entire journal deleted successfully!{resetColor}");
                        }
                        else if(deleteOption == "2")
                        {
                            // Delete individual entry in the journal
                            Console.Write("Enter the entry number to delete: ");
                            if (int.TryParse(Console.ReadLine(), out int entryNumber))
                            {
                                // Call the method to delete the specific entry by entryNumber
                                delete.DeleteSpecificItems(entryNumber);
                            }
                            else
                            {
                                Console.WriteLine($"{redColor}Invalid entry number.{resetColor}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{redColor}Invalid option for delete.{resetColor}");
                        }
                        break;

                    case "4":
                        // SAVE ----------------------------------------------------------
                        journal.SaveToDatabase();
                        Console.WriteLine($"\n{darkGreenColor}Journal entries saved successfully!{resetColor}");
                        break;
                    
                    case "3":
                         // LOAD ----------------------------------------------------------
                            int EntriesCount = journal.LoadFromDatabase();
                            Console.WriteLine($"\n{darkPurpleColor}Entries loaded from the database.{resetColor}");

                            // Check if there are loaded entries
                            if (journal.EntriesCount > 0)
                            {
                                Console.WriteLine($"{darkPurpleColor}Displaying loaded entries:{resetColor}");
                                journal.DisplayAll();
                                Console.WriteLine("----------------------------------------------------------------------------------------------");

                            }
                            else
                            {
                                Console.WriteLine($"{redColor}No items in your journal.{resetColor}");
                            }
                            break;

                    default:
                        Console.WriteLine($"\n{redColor}Invalid option. Please choose a valid option.{resetColor}\n");
                        break;
                }
            }
        }
    }
}
