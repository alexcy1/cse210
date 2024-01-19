using System;
using System.Data.SQLite;

namespace Develop02
{
    public class Delete
    {
        private string connectionString;
        private string redColor = "\u001b[31m";
        private string resetColor = "\u001b[0m";


        public Delete(string connectionString)
        {
            this.connectionString = connectionString;
        }


        // Delete all items in the journal ------------------------------------------
        public void DeleteAllItems()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Delete all rows from the journal
                using (var command = new SQLiteCommand("DELETE FROM journal", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        // Delete single journal in the Database ----------------------------------------
        public void DeleteSpecificItems(int entryNumber)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Get the total count of items in the journal
                int totalItems;
                using (var countCommand = new SQLiteCommand("SELECT COUNT(*) FROM journal", connection))
                {
                    totalItems = Convert.ToInt32(countCommand.ExecuteScalar());
                }

                if (totalItems == 0)
                {
                    Console.WriteLine($"\n{redColor}No items in the journal to delete.{resetColor}");
                    return;
                }

                // Validate the entryNumber
                if (entryNumber < 1 || entryNumber > totalItems)
                {
                    Console.WriteLine($"\n{redColor}Invalid entry number. Please enter a number between 1 and {0}.{resetColor}", totalItems);
                    return;
                }

                // Delete the row with a specific entry number from the journal table
                using (var command = new SQLiteCommand("DELETE FROM journal WHERE rowid IN (SELECT rowid FROM journal LIMIT 1 OFFSET @entryNumber - 1)", connection))
                {
                    command.Parameters.AddWithValue("@entryNumber", entryNumber);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine($"\n{redColor}Entry {entryNumber} deleted successfully!{resetColor}");
            }
        }
    }
}
