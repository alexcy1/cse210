using System;
using System.Collections.Generic;
using System.Data.SQLite;

class User : Entity
{
    private string _name;
    private float _budget;
    private List<Transaction> _transactions = new List<Transaction>();

    private const string ConnectionString = "Data Source=ExpensesTracker.db;Version=3;";

    public User()
    {
        CreateTables();
    }

    private void CreateTables()
    {
        using (SQLiteConnection connection = OpenConnection())
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // Users Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Users (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_name TEXT, " +
                                      "_budget REAL)";
                command.ExecuteNonQuery();

                // Income Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Income (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_amount REAL, " +
                                      "_date DATETIME)";
                command.ExecuteNonQuery();

                // Expenses Table
                command.CommandText = "CREATE TABLE IF NOT EXISTS Expenses (" +
                                      "_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                      "_amount REAL, " +
                                      "_date DATETIME, " +
                                      "_name TEXT, " +
                                      "_category TEXT)";
                command.ExecuteNonQuery();
            }
        }
    }
    


    // Adds an Income to the Income table in the database    
    public void AddIncome(float amount, DateTime date)
    {
        // Add income logic, e.g., insert into the Income table
        using (SQLiteConnection connection = OpenConnection())
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Income (_amount, _date) VALUES (@amount, @date)";
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }
        }
    }



    // Adds an expense to the Expenses table in the database
    public void AddExpense(string name, float amount, DateTime date, string category)
    {
        // Add expense logic, e.g., insert into the Expenses table
        using (SQLiteConnection connection = OpenConnection())
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO Expenses (_name, _amount, _date, _category) " +
                                      "VALUES (@name, @amount, @date, @category)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@category", category);
                command.ExecuteNonQuery();
            }
        }
    }


    public float GetBalance()
    {
        // Get balance logic, e.g., sum of income - sum of expenses
        using (SQLiteConnection connection = OpenConnection())
        {
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT IFNULL(SUM(_amount), 0) FROM Income;" +
                                      "SELECT IFNULL(SUM(_amount), 0) FROM Expenses;";
                SQLiteDataReader reader = command.ExecuteReader();
                return 0;
            }
        }
    }



public float GetTotalExpenses() // Total expenses method
{
    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            return 0;
        }
    }
}





public List<Income> GetIncomeHistory() // income History Method
{
    // Get income history logic, e.g., select from Income table
    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            command.CommandText = "SELECT * FROM Income";
            SQLiteDataReader reader = command.ExecuteReader();

            List<Income> incomeHistory = new List<Income>();
            
            return incomeHistory;
        }
    }
}




public List<Expense> GetExpenseHistory() // Expense History Method
{
    // Get expense history logic, e.g., select from Expenses table
    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            command.CommandText = "SELECT * FROM Expenses";
            SQLiteDataReader reader = command.ExecuteReader();

            List<Expense> expenseHistory = new List<Expense>();
            
            return expenseHistory;
        }
    }
}



// Retrieves the expense history, calculates total income, and generates a consolidated chart.
 public void ShowConsolidatedChart()
 {
    List<Expense> expenses = GetExpenseHistory();
    float totalIncome = CalculateTotalIncome();
    ChartGenerator.GenerateConsolidatedChart(expenses, totalIncome);
}



public float CalculateTotalIncome() // Total Income calcuation goes here in the method
{
    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            return 0;
        }
    }
}


// Opens a connection to the SQLite database using the specified connection string.
private SQLiteConnection OpenConnection()
{
    SQLiteConnection connection = new SQLiteConnection(ConnectionString);
    connection.Open();
    return connection;
}



public float DeleteAllIncome() // Delete All method
{
    float totalIncome = 0;

    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            // Retrieve the total income before deleting
            command.CommandText = "SELECT SUM(_amount) FROM Income";
            object result = command.ExecuteScalar();
            
            if (result != DBNull.Value)
            {
                totalIncome = Convert.ToSingle(result);
            }

            // Now delete all income records
            command.CommandText = "DELETE FROM Income";
            command.ExecuteNonQuery();
        }
    }

    //Console.WriteLine($"All income totaling ${totalIncome} deleted successfully.");
    return totalIncome;
}



public float DeleteAllExpenses() // Delete Expenses method
{
    float totalExpenses = 0;

    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            // Retrieve the total expenses before deleting
            command.CommandText = "SELECT SUM(_amount) FROM Expenses";
            object result = command.ExecuteScalar();

            if (result != DBNull.Value)
            {
                totalExpenses = Convert.ToSingle(result);
            }

            // Now delete all expenses records
            command.CommandText = "DELETE FROM Expenses";
            command.ExecuteNonQuery();
        }
    }

    //Console.WriteLine($"All expenses totaling ${totalExpenses} deleted successfully.");
    return totalExpenses;
}




public float DeleteIncomeById(int incomeId) // Delete Icome by ID method
{
    float deletedIncomeAmount = 0;

    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            // Retrieve the income amount before deleting
            command.CommandText = "SELECT _amount FROM Income WHERE _id = @id";
            command.Parameters.AddWithValue("@id", incomeId);

            object result = command.ExecuteScalar();
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



public float DeleteExpenseById(int expenseId) // Delete Expense by ID method
{
    float deletedExpenseAmount = 0;

    using (SQLiteConnection connection = OpenConnection())
    {
        using (SQLiteCommand command = new SQLiteCommand(connection))
        {
            // Retrieve the expense amount before deleting
            command.CommandText = "SELECT _amount FROM Expenses WHERE _id = @id";
            command.Parameters.AddWithValue("@id", expenseId);

            object result = command.ExecuteScalar();
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

