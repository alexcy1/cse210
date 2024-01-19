// install SQLITE for macOS || navigate to the project directory and install
// dotnet add package System.Data.SQLite.Core -s https://api.nuget.org/v3/index.json

using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Develop02
{
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();
        private string connectionString = "Data Source=journal.db;";
        public int EntriesCount => _entries.Count;
        private string darkPurpleColor = "\u001b[35m";
        private string resetColor = "\u001b[0m";
        private string redColor = "\u001b[31m";


        // Add Entry -----------------------------------------------------------------
        public void AddEntry(Entry newEntry)
        {
            _entries.Add(newEntry);
        }

        
        #pragma warning disable
         // Display All Items In A Journal -----------------------------------------------------------------
        public void DisplayAll()
        {
            // Clear the existing entries before displaying
            _entries.Clear();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Retrieve all records from the journal table
                using (var command = new SQLiteCommand("SELECT daily_journal FROM journal", connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["daily_journal"] != null) // Check for null
                            {
                                string[] entryData = reader["daily_journal"].ToString().Split(new[] { " - Prompt: ", "\n" }, StringSplitOptions.None);

                                if (entryData.Length >= 3)
                                {
                                    Entry loadedEntry = new Entry(entryData[0], entryData[1], entryData[2]);
                                    _entries.Add(loadedEntry);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid data format in the database.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Unexpected null value in the database.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n{redColor}No items in your journal.{resetColor}");
                        return;
                    }
                }
            }

            foreach (var entry in _entries) // Loop through all journal entries and display ----------------------------
            {
                Console.WriteLine($"\n{darkPurpleColor}Date: {entry.Date} - Prompt: {entry.PromptText}\n{entry.EntryText}{resetColor}");
            }
        }
        #pragma warning restore




        // Save Prompts And Entries To SQLite-Database -----------------------------------------------------
        public void SaveToDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Get the count of entries already present in the database
                int existingEntriesCount;
                using (var countCommand = new SQLiteCommand("SELECT COUNT(*) FROM journal", connection))
                {
                    existingEntriesCount = Convert.ToInt32(countCommand.ExecuteScalar());
                }

                if (_entries.Count > 0)
                {
                    Entry newEntry = _entries.Last();

                    // Insert the new entry into the journal table
                    using (var command = new SQLiteCommand("INSERT INTO journal (daily_journal) VALUES (@journalPrompt)", connection))
                    {
                        command.Parameters.AddWithValue("@journalPrompt", $"{newEntry.Date} - Prompt: {newEntry.PromptText}\n{newEntry.EntryText}");
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    Console.WriteLine($"{redColor}No new entries to save.{resetColor}");
                }
            }
        }



        #pragma warning disable
        // Load Journal From database ------------------------------------------------
        public int LoadFromDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Retrieve all records from the journal table
                using (var command = new SQLiteCommand("SELECT daily_journal FROM journal", connection))
                using (var reader = command.ExecuteReader())
                {
                    int count = 0;

                    while (reader.Read())
                    {
                        string[] entryData = reader["daily_journal"].ToString().Split(new[] { " - Prompt: ", "\n" }, StringSplitOptions.None);

                        if (entryData.Length >= 3)
                        {
                            Entry loadedEntry = new Entry(entryData[0], entryData[1], entryData[2]);
                            _entries.Add(loadedEntry);
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        Console.WriteLine("----------------------------------------------------------------------------------------------");
                        foreach (var entry in _entries)
                        {
                            Console.WriteLine($"\nDate: {entry?.Date}\nPrompt: {entry?.PromptText}\nEntry: {entry?.EntryText}\n");
                        }
                        Console.WriteLine("----------------------------------------------------------------------------------------------");
                        Console.WriteLine($"{darkPurpleColor}You have {count} {(count > 1 ? "items" : "item")} in your journal:{resetColor}");
                        Console.WriteLine("---------------------------------");
                    }

                    return count;
                }
            }
        }
        #pragma warning restore


    }
}
