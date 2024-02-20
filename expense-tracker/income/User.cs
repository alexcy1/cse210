using System;
using System.Collections.Generic;
using System.Data.SQLite;

class User : Entity
{
    // Private fields to store user information
    private string _name;
    private float _budget;
    private List<Transaction> _transactions = new List<Transaction>();

    // Constant string for the connection string to the database
    private const string ConnectionString = "Data Source=ExpensesTracker.db;Version=3;";


    // Constructor for the User class
    public User()
    {
        CreateTables();  // Call the CreateTables method when a User object is instantiated
    }


    // Private method to create necessary tables in the database
    private void CreateTables()
    {
        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Create Users Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Users (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_name TEXT, " +
                                      "_budget REAL)";
                // Execute the SQL query to create the Users table
                command.ExecuteNonQuery();

                // Create Income Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Income (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_amount REAL, " +
                                      "_date DATETIME)";
                // Execute the SQL query to create the Income table
                command.ExecuteNonQuery();

                // Create Expenses Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Expenses (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_amount REAL, " +
                                      "_date DATETIME, " +
                                      "_name TEXT, " +
                                      "_category TEXT)";
                // Execute the SQL query to create the Expenses table
                command.ExecuteNonQuery();
            }
        }
    }


    
    // Method to add income to the database
    public void AddIncome(float amount, DateTime date)
    {
         // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // SQL query to insert income into the Income table
                command.CommandText = "INSERT INTO Income (_amount, _date) VALUES (@amount, @date)";
                // Add parameters to the SQL query to prevent SQL injection
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }
        }
    }



    // Method to add an expense to the database
    public void AddExpense(string name, float amount, DateTime date, string category)
    {
         // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // SQL query to insert an expense into the Expenses table
                command.CommandText = "INSERT INTO Expenses (_name, _amount, _date, _category) " +
                                      "VALUES (@name, @amount, @date, @category)";

                // Add parameters to the SQL query to prevent SQL injection
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@category", category);
                command.ExecuteNonQuery();
            }
        }
    }



    // Method to get the balance by calculating the sum of income minus the sum of expenses
    public float GetBalance()
    {
         // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // SQL query to get the sum of income and expenses
                command.CommandText = "SELECT IFNULL(SUM(_amount), 0) FROM Income;" +
                                      "SELECT IFNULL(SUM(_amount), 0) FROM Expenses;";
                SQLiteDataReader reader = command.ExecuteReader();

                float income = reader.Read() ? Convert.ToSingle(reader[0]) : 0;
                // Move to the next result set for the sum of expenses
                reader.NextResult();
                float expenses = reader.Read() ? Convert.ToSingle(reader[0]) : 0;

                // Calculate and return the balance (income - expenses)
                return income - expenses;
            }
        }
    }



// Method to get the total expenses by calculating the sum of amounts from the Expenses table
public float GetTotalExpenses()
{
     // Open a connection to the SQLite database
    using (SQLiteConnection connection = OpenConnection())
    {
        // Create a SQLiteCommand to execute SQL queries
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            command.CommandText = "SELECT IFNULL(SUM(_amount), 0) FROM Expenses;";
            // Execute the SQL query and read the result
            SQLiteDataReader reader = command.ExecuteReader();

            // Return the sum of amounts (if available) or default to 0
            return reader.Read() ? Convert.ToSingle(reader[0]) : 0;
        }
    }
}



// Method to get the income history by selecting all records from the Income table
public List<Income> GetIncomeHistory()
{
    // Open a connection to the SQLite database
    using (SQLiteConnection connection = OpenConnection())
    {
        // Create a SQLiteCommand to execute SQL queries
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            command.CommandText = "SELECT * FROM Income";
            // Execute the SQL query and read the results
            SQLiteDataReader reader = command.ExecuteReader();

            // Create a list to store the income history
            List<Income> incomeHistory = new List<Income>();
            // Iterate through the results and create Income objects
            while (reader.Read())
            {
                // Extract values from the reader
                int id = Convert.ToInt32(reader["_id"]);
                float amount = Convert.ToSingle(reader["_amount"]);
                DateTime date = Convert.ToDateTime(reader["_date"]);

                // Create an Income object and add it to the list
                Income income = new Income(id, amount, date);
                incomeHistory.Add(income);
            }
            // Return the list of Income objects representing the income history
            return incomeHistory;
        }
    }
}



// Method to get the expense history by selecting all records from the Expenses table
public List<Expense> GetExpenseHistory()
{
    // Open a connection to the SQLite database
    using (SQLiteConnection connection = OpenConnection())
    {
        // Create a SQLiteCommand to execute SQL queries
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            // SQL query to select all records from the Expenses table
            command.CommandText = "SELECT * FROM Expenses";
            SQLiteDataReader reader = command.ExecuteReader();

            // Create a list to store the expense history
            List<Expense> expenseHistory = new List<Expense>();

            // Iterate through the results and create Expense objects
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["_id"]);
                float amount = Convert.ToSingle(reader["_amount"]);
                DateTime date = Convert.ToDateTime(reader["_date"]);
                string name = Convert.ToString(reader["_name"]);
                string category = Convert.ToString(reader["_category"]);

                // Create an Expense object and add it to the list
                Expense expense = new Expense(id, amount, date, name, category);
                expenseHistory.Add(expense);
            }
            // Return the list of Expense objects representing the expense history
            return expenseHistory;
        }
    }
}



// Method to show a consolidated chart based on expense history and total income
 public void ShowConsolidatedChart()
 {
    // Retrieve expense history from the database
    List<Expense> expenses = GetExpenseHistory();

    // Calculate the total income
    float totalIncome = CalculateTotalIncome();
    ChartGenerator.GenerateConsolidatedChart(expenses, totalIncome);
}



// Method to calculate the total income by summing up amounts from the Income table
public float CalculateTotalIncome()
{
    // Open a connection to the SQLite database
    using (SQLiteConnection connection = OpenConnection())
    {
        // Create a SQLiteCommand to execute SQL queries
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            command.CommandText = "SELECT IFNULL(SUM(_amount), 0) FROM Income";
             // Execute the SQL query and get the result as an object
            object result = command.ExecuteScalar();

            return result != DBNull.Value ? Convert.ToSingle(result) : 0;
        }
    }
}


    
// Method to open a connection to the SQLite database
private SQLiteConnection OpenConnection()
{
    SQLiteConnection connection = new SQLiteConnection(ConnectionString);
    // Open the connection to the database
    connection.Open();
    return connection;
}



// Method to delete all income records and return the total income before deletion
public float DeleteAllIncome()
    {
        // Initialize the variable to store the total income
        float totalIncome = 0;

        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Retrieve the total income before deleting all records
                command.CommandText = "SELECT SUM(_amount) FROM Income";
                object result = command.ExecuteScalar();
                
                // Check if the result is not DBNull.Value and convert to float
                if (result != DBNull.Value)
                {
                    totalIncome = Convert.ToSingle(result);
                }

                // Delete all records from the Income table
                command.CommandText = "DELETE FROM Income";
                command.ExecuteNonQuery();
            }
        }

        //Console.WriteLine($"All income totaling ${totalIncome} deleted successfully.");
        return totalIncome;
    }



// Method to check if an expense with the given ID exists in the Expenses table
public bool ExpenseExists(int expenseId)
    {
        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Check if the expense with the given ID exists
                command.CommandText = "SELECT COUNT(*) FROM Expenses WHERE _id = @id";
                command.Parameters.AddWithValue("@id", expenseId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                // Return true if the count is greater than 0 (expense exists), otherwise false
                return count > 0;
            }
        }
    }



// Method to check if an income with the given ID exists in the Income table
public bool IncomeExists(int incomeId)
    {
        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Check if the income with the given ID exists
                command.CommandText = "SELECT COUNT(*) FROM Income WHERE _id = @id";

                 // Add a parameter for the income ID
                command.Parameters.AddWithValue("@id", incomeId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                // Return true if the count is greater than 0 (income exists), otherwise false
                return count > 0;
            }
        }
    }



// Method to delete all expense records and return the total expenses before deletion
public float DeleteAllExpenses()
    {
        // Initialize the variable to store the total expenses
        float totalExpenses = 0;

        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Retrieve the total expenses before deleting all records
                command.CommandText = "SELECT SUM(_amount) FROM Expenses";
                object result = command.ExecuteScalar();

                // Check if the result is not DBNull.Value and convert to float
                if (result != DBNull.Value)
                {
                    totalExpenses = Convert.ToSingle(result);
                }

                // Delete all records from the Expenses table
                command.CommandText = "DELETE FROM Expenses";
                command.ExecuteNonQuery();
            }
        }

        //Console.WriteLine($"All expenses totaling ${totalExpenses} deleted successfully.");
        return totalExpenses;
    }



// Method to delete a specific income record by ID and return the deleted income amount
public float DeleteIncomeById(int incomeId)
    {
        // Initialize the variable to store the deleted income amount
        float deletedIncomeAmount = 0;

        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Retrieve the income amount before deleting
                command.CommandText = "SELECT _amount FROM Income WHERE _id = @id";
                // Add a parameter for the income ID
                command.Parameters.AddWithValue("@id", incomeId);

                object result = command.ExecuteScalar();

                // Check if the result is not DBNull.Value and convert to float
                if (result != DBNull.Value)
                {
                    deletedIncomeAmount = Convert.ToSingle(result);

                    // Now delete the specific income record
                    command.CommandText = "DELETE FROM Income WHERE _id = @id";
                    command.ExecuteNonQuery();
                }
            }
        }

        //Console.WriteLine($"Income totaling ${deletedIncomeAmount} deleted successfully.");
        return deletedIncomeAmount;
    }



// Method to delete a specific expense record by ID and return the deleted expense amount
public float DeleteExpenseById(int expenseId)
    {
        // Initialize the variable to store the deleted expense amount
        float deletedExpenseAmount = 0;

        // Open a connection to the SQLite database
        using (SQLiteConnection connection = OpenConnection())
        {
            // Create a SQLiteCommand to execute SQL queries
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Retrieve the expense amount before deleting
                command.CommandText = "SELECT _amount FROM Expenses WHERE _id = @id";
                 // Add a parameter for the expense ID
                command.Parameters.AddWithValue("@id", expenseId);

                object result = command.ExecuteScalar();

                // Check if the result is not DBNull.Value and convert to float
                if (result != DBNull.Value)
                {
                    deletedExpenseAmount = Convert.ToSingle(result);

                    // Now delete the specific expense record
                    command.CommandText = "DELETE FROM Expenses WHERE _id = @id";
                    command.ExecuteNonQuery();
                }
            }
        }

        //Console.WriteLine($"Expense totaling ${deletedExpenseAmount} deleted successfully.");
        return deletedExpenseAmount;
    }

}

