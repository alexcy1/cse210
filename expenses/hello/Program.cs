using System;
using System.Collections.Generic;

namespace hello
{
    class Program
    {
        static void Main()
        {
            // Clear the console and display the welcome message
            Console.Clear();
            DisplayWelcomeMessage();
            User user = new User();

            while (true)
            {
                // Display the main menu and prompt for user input
                DisplayMenu();
                Console.Write("\nEnter your option :::");
                string choice = Console.ReadLine()!;

                // Handle the input based on the user's selected option
                HandleUserInput(choice, user);
            }
        }

        // Method to display a welcome message
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("\nWelcome to the Expenses Tracking App!\n");
        }

        // Method to display the main menu options
        private static void DisplayMenu()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Expenses Tracking Menu:");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("(1) Add Income");
            Console.WriteLine("(2) Add Expenses");
            Console.WriteLine("(3) View Balance");
            Console.WriteLine("(4) Compare Expenses");
            Console.WriteLine("(5) Show Income History");
            Console.WriteLine("(6) Show Expenses History");
            Console.WriteLine("(7) Delete Records");
            Console.WriteLine("(8) Quit");
        }

        private static void HandleUserInput(string choice, User user)
        {
            switch (choice.ToUpper())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Income Entry Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.Write("Enter income amount. (q) to quit :::");
                    string incomeAmountInput = Console.ReadLine()!;

                    if (!string.IsNullOrWhiteSpace(incomeAmountInput) && float.TryParse(incomeAmountInput, out float incomeAmount))
                    {
                        user.AddIncome(incomeAmount, DateTime.Now);
                        Console.WriteLine("\nIncome added successfully!\n");
                    }
                    
                    else if(incomeAmountInput.ToLower() == "q")
                    {
                        Console.WriteLine("You quit adding Income.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for income amount.");
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Expense Entry Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Income Balance Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.WriteLine($"Total Income:\t ${user.CalculateTotalIncome():N0}");
                    Console.WriteLine($"Total Expenses:\t ${user.GetTotalExpenses():N0}");
                    Console.WriteLine($"Income Balance:\t ${user.GetBalance():N0}\n");
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Compare Expenses Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.WriteLine("");
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Income History Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    List<ITransaction> incomeTransactions = user.GetIncomeHistory().Cast<ITransaction>().ToList();
                    DisplayTransactionHistory("Income History", incomeTransactions);
                    Console.WriteLine("");
                    break;

                case "6":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Expense History Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    List<ITransaction> expenseTransactions = user.GetExpenseHistory().Cast<ITransaction>().ToList();
                    DisplayTransactionHistory("", expenseTransactions);
                    Console.WriteLine("");
                    break;

                case "7":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Delete Record Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    DisplayDeleteMenu(user);
                    Console.WriteLine("");
                    break;

                case "8":
                    Console.WriteLine("\nYou Exited Expenses Tracker. Goodbye!\n");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        

// Delete menu method
private static void DisplayDeleteMenu(User user)
{
    while (true)
    {
        //Console.WriteLine("Delete Menu:\n");
        Console.WriteLine("(1) Delete All Income");
        Console.WriteLine("(2) Delete All Expenses");
        Console.WriteLine("(3) Delete Specific Income");
        Console.WriteLine("(4) Delete Specific Expense");
        Console.WriteLine("(5) Quit delete menu");

        Console.Write("\nEnter your option ::: ");
        string deleteChoice = Console.ReadLine()!;

        switch (deleteChoice)
        {
            case "1":
                float totalIncome = user.DeleteAllIncome();
                Console.WriteLine($"\nAll income totaling ${totalIncome:N0} deleted successfully.\n");
                break;

            case "2":
                float totalExpenses = user.DeleteAllExpenses();
                Console.WriteLine($"\nAll expenses totaling ${totalExpenses:N0} deleted successfully.\n");
                break;

            case "3":
                while (true)
                {
                    Console.Write("Enter the ID of the income to delete (or 'q' to quit) ::: ");
                    string userInput = Console.ReadLine()!;

                    if (userInput.ToLower() == "q")
                    {
                        // User chose to quit
                        break;
                    }

                    if (int.TryParse(userInput, out int incomeIdToDelete))
                    {
                        float deletedIncomeAmount = user.DeleteIncomeById(incomeIdToDelete);
                        if (deletedIncomeAmount > 0)
                        {
                            Console.WriteLine($"\nIncome totaling ${deletedIncomeAmount:N0} deleted successfully.\n");
                        }
                        else
                        {
                            Console.WriteLine($"No income found with ID {incomeIdToDelete}.\n");
                        }
                        break; // Exit the loop after attempting deletion
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID. Please enter a valid ID or 'q' to quit.");
                    }
                }
                break;

            case "4":
                while (true)
                {
                    Console.Write("Enter the ID of the expense to delete (or 'q' to quit) ::: ");
                    string userInput = Console.ReadLine()!;

                    if (userInput.ToLower() == "q")
                    {
                        // User chose to quit
                        break;
                    }

                    if (int.TryParse(userInput, out int expenseIdToDelete))
                    {
                        float deletedExpenseAmount = user.DeleteExpenseById(expenseIdToDelete);
                        if (deletedExpenseAmount > 0)
                        {
                            Console.WriteLine($"\nExpense totaling ${deletedExpenseAmount:N0} deleted successfully.\n");
                        }
                        else
                        {
                            Console.WriteLine($"No expense found with ID {expenseIdToDelete}.\n");
                        }
                        break; // Exit the loop after attempting deletion
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID. Please enter a valid ID or 'q' to quit.");
                    }
                }
                break;


            case "5":
                Console.WriteLine("Quitting delete menu...");
                Console.Clear();
                return; // Exit the method or break the loop

            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Please try again.\n");
                break;
        }
    }
}



        private static void DisplayTransactionHistory(string title, List<ITransaction> transactions)
        {
            //Console.WriteLine($"{title}");
            foreach (var transaction in transactions)
            {
                // Check if it's an expense to access category and name
                if (transaction is Expense expense)
                {
                    Console.WriteLine($"ID: {transaction.GetId()},\t\t{expense.GetCategory()}\t\t${transaction.GetAmount():N0}\t\t{transaction.GetDate()}");
                }
                else
                {
                    Console.WriteLine($"ID: {transaction.GetId()},\t\t${transaction.GetAmount():N0}\t\t{transaction.GetDate()}");
                }
            }
        }




    }
}
