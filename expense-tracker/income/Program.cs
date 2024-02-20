using System;
using System.Collections.Generic;

namespace hello
{
    class Program
    {
        static void Main()
        {
            ColorUtility colorUtility = new ColorUtility();
            // Clear the console and display the welcome message
            Console.Clear();
            DisplayWelcomeMessage();
            User user = new User();
            
            while (true)
            {
                // Display the main menu and prompt for user input
                DisplayMenu();
                Console.Write(colorUtility.ApplyDarkPurple("\nEnter your option :::"));
                string choice = Console.ReadLine()!;

                // Handle the input based on the user's selected option
                HandleUserInput(choice, user);
            }
        }


        // Display a welcome message
        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("\n=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("Welcome to the Expenses Tracking App!");
            Console.WriteLine("=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=\n");
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


        // Method to handle user inputs and choice
        private static void HandleUserInput(string choice, User user)
        {
            ColorUtility colorUtility = new ColorUtility();
            switch (choice.ToUpper())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Income Entry Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.Write(colorUtility.ApplyDarkPurple("Enter income amount. (q) to quit :::"));
                    string incomeAmountInput = Console.ReadLine()!;

                    if (!string.IsNullOrWhiteSpace(incomeAmountInput) && float.TryParse(incomeAmountInput, out float incomeAmount))
                    {
                        user.AddIncome(incomeAmount, DateTime.Now);
                        Console.WriteLine(colorUtility.ApplyGreen("\nIncome added successfully!\n"));
                    }
                    
                    else if(incomeAmountInput.ToLower() == "q")
                    {
                        Console.WriteLine(colorUtility.ApplyRed("You quit adding Income."));
                        break;
                    }
                    else
                    {
                        Console.WriteLine(colorUtility.ApplyRed("Invalid input for income amount."));
                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Expense Entry Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.Write(colorUtility.ApplyDarkPurple("Enter expense name :::"));
                    string expenseName = Console.ReadLine()!;

                    Console.Write(colorUtility.ApplyDarkPurple("Enter expense amount :::"));
                    string expenseAmountInput = Console.ReadLine()!;
                    float expenseAmount;

                    if (float.TryParse(expenseAmountInput, out expenseAmount))
                    {
                        Console.Write(colorUtility.ApplyDarkPurple("Enter expense category :::"));
                        string expenseCategory = Console.ReadLine()!;

                        user.AddExpense(expenseName, expenseAmount, DateTime.Now, expenseCategory);
                        
                        Console.WriteLine(colorUtility.ApplyGreen("\nExpense added successfully!\n"));
                    }
                    else
                    {
                        Console.WriteLine(colorUtility.ApplyRed("Invalid expense amount. Please enter a valid number."));
                    }

                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Income Balance Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    Console.WriteLine(colorUtility.ApplyGreen($"Total Income:\t ${user.CalculateTotalIncome():N0}"));
                    Console.WriteLine(colorUtility.ApplyRed($"Total Expenses:\t ${user.GetTotalExpenses():N0}"));
                    Console.WriteLine(colorUtility.ApplyDarkPurple($"Income Balance:\t ${user.GetBalance():N0}\n"));
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Compare Expenses Page".ToUpper());
                    Console.WriteLine("----------------------------------------");

                    //user.ShowExpensesChart();
                    user.ShowConsolidatedChart();
                    Console.WriteLine("");
                    break;

                case "5":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine(colorUtility.ApplyDarkBlue("Income History Page".ToUpper()));
                    Console.WriteLine("----------------------------------------");

                    List<ITransaction> incomeTransactions = user.GetIncomeHistory().Cast<ITransaction>().ToList();
                    DisplayTransactionHistory("Income History", incomeTransactions);
                    Console.WriteLine("");
                    break;

                case "6":
                    Console.Clear();
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine(colorUtility.ApplyRed("Expense History Page".ToUpper()));
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
                    Console.WriteLine(colorUtility.ApplyRed("You Exited Expenses Tracker. Goodbye!\n"));
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine(colorUtility.ApplyRed("Invalid choice. Please try again."));
                    break;
            }
        }



    // Display delete Menu Method
    private static void DisplayDeleteMenu(User user) 
    {
        while (true)
        {
            ColorUtility colorUtility = new ColorUtility();

            Console.WriteLine("(1) Delete All Income");
            Console.WriteLine("(2) Delete All Expenses");
            Console.WriteLine("(3) Delete Specific Income");
            Console.WriteLine("(4) Delete Specific Expense");
            Console.WriteLine("(5) Quit delete menu");

            Console.Write(colorUtility.ApplyDarkPurple("\nEnter your option ::: "));
            string deleteChoice = Console.ReadLine()!;

            switch (deleteChoice)
            {
                case "1":
                    float totalIncome = user.DeleteAllIncome();
                    Console.WriteLine(colorUtility.ApplyRed($"\nAll income totaling ${totalIncome:N0} deleted successfully.\n"));
                    break;

                case "2":
                    float totalExpenses = user.DeleteAllExpenses();
                    Console.WriteLine(colorUtility.ApplyRed($"\nAll expenses totaling ${totalExpenses:N0} deleted successfully.\n"));
                    break;

                case "3":
                    int incomeIdToDelete = -1; // Initialize with default value
                    try
                    {
                        Console.Write(colorUtility.ApplyDarkPurple("Enter the ID of the income to delete ::: "));
                        incomeIdToDelete = int.Parse(Console.ReadLine()!);

                        // Check if the income ID exists in the database
                        bool incomeExists = user.IncomeExists(incomeIdToDelete);

                        if (incomeExists)
                        {
                            float deletedIncomeAmount = user.DeleteIncomeById(incomeIdToDelete);
                            Console.WriteLine(colorUtility.ApplyRed($"\nIncome totaling ${deletedIncomeAmount:N0} deleted successfully.\n"));
                        }
                        else
                        {
                            Console.WriteLine(colorUtility.ApplyRed($"No income found with ID {incomeIdToDelete}.\n"));
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(colorUtility.ApplyRed($"Error occurred while deleting the income with ID {incomeIdToDelete}.\n"));
                    }
                    break;


                case "4":
                    int expenseIdToDelete = -1; // Initialize with default value
                    try
                    {
                        Console.Write(colorUtility.ApplyDarkPurple("Enter the ID of the expense to delete ::: "));
                        expenseIdToDelete = int.Parse(Console.ReadLine()!);

                        // Check if the expense ID exists in the database
                        bool expenseExists = user.ExpenseExists(expenseIdToDelete); // Assuming there's a method to check existence

                        if (expenseExists)
                        {
                            float deletedExpenseAmount = user.DeleteExpenseById(expenseIdToDelete);
                            Console.WriteLine(colorUtility.ApplyRed($"\nExpense totaling ${deletedExpenseAmount:N0} deleted successfully.\n"));
                        }
                        else
                        {
                            Console.WriteLine(colorUtility.ApplyRed($"No expense found with ID {expenseIdToDelete}.\n"));
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(colorUtility.ApplyRed($"Error occurred while deleting the expense with ID {expenseIdToDelete}.\n"));
                    }
                    break;


                case "5":
                    Console.WriteLine(colorUtility.ApplyRed("Quitting delete menu..."));
                    Console.Clear();
                    return; // Exit the method or break the loop

                default:
                    Console.Clear();
                    Console.WriteLine(colorUtility.ApplyRed("Invalid choice. Please try again.\n"));
                    break;
            }
        }
    }



    // Transaction history method
    private static void DisplayTransactionHistory(string title, List<ITransaction> transactions)
        {
            ColorUtility colorUtility = new ColorUtility();
            foreach (var transaction in transactions)
            {
                // Check if it's an expense to access category and name
                if (transaction is Expense expense)
                {
                    Console.WriteLine(colorUtility.ApplyRed($"ID: {transaction.GetId()},\t\t{expense.GetCategory()}\t\t${transaction.GetAmount():N0}\t\t{transaction.GetDate()}"));
                }
                else
                {
                    Console.WriteLine(colorUtility.ApplyDarkBlue($"ID: {transaction.GetId()},\t\t${transaction.GetAmount():N0}\t\t{transaction.GetDate()}"));
                }
            }
        }


    }
}
