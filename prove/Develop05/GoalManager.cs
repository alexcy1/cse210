using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        while (true)
        {
            // Main menu Option
            Console.WriteLine($"\nYou have {_score} points");

            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Display Player Info");
            Console.WriteLine("2. List Goal Names");
            Console.WriteLine("3. List Goal Details");
            Console.WriteLine("4. Create New Goal");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Save Goals");
            Console.WriteLine("7. Load Goals");
            Console.WriteLine("8. Exit\n");

            Console.Write("Select a choice from the menu :::");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8)
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
            }

            switch (choice) // Switch case to display program based on choice selected
            {
                case 1:
                    DisplayPlayerInfo();
                    break;
                case 2:
                    ListGoalNames();
                    break;
                case 3:
                    ListGoalDetails();
                    break;
                case 4:
                    CreateGoal();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    SaveGoals();
                    break;
                case 7:
                    LoadGoals();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    break;
            }
        }
    }


    public void DisplayPlayerInfo() // Display Point method
    {
        Console.WriteLine($"Current Score: {_score}");
    }


    public void ListGoalNames() // Show goal names method
    {
        Console.WriteLine("\n-------------------------------");
        Console.WriteLine("List of Goal Names:");
        Console.WriteLine("-------------------------------");

        if (_goals.Count == 0)
        {
            Console.WriteLine("No goal names available.");
        }
        else
        {
            foreach (var goal in _goals)
            {
                Console.WriteLine(goal.ShortName);
            }
        }
    }



    public void ListGoalDetails() // Goal details method
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
        }
        else
        {
            Console.WriteLine("\n-------------------------------");
            Console.WriteLine("List of Goal Details:");
            Console.WriteLine("-------------------------------");

            for (int i = 0; i < _goals.Count; i++)
            {
                string completionStatus = _goals[i].IsComplete() ? "[X]" : "[ ]";

                // Remove prefixes if they exist
                string goalName = _goals[i].GetDetailsString();
                goalName = goalName.Replace("SimpleGoal:", "").Replace("EternalGoal:", "").Replace("ChecklistGoal:", "");

                Console.Write($"{i + 1}. {completionStatus} {goalName}");

                if (_goals[i] is ChecklistGoal checklistGoal)
                {
                    Console.Write($" -- Currently completed {checklistGoal.AmountCompleted}/{checklistGoal.Target}");
                }

                Console.WriteLine();
            }
        }
    }


    public void CreateGoal() // Create new goal method
    {
        Console.WriteLine("\nThe types of Goals are:"); // Menu option
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal\n");

        Console.Write("Which type of goal would you like to create? :::");
        int typeChoice = int.Parse(Console.ReadLine());

        Goal goal;
        switch (typeChoice) // Display condition with switch case
        {
            case 1:
                Console.Write("What is the name of your goal? :::");
                string simpleName = Console.ReadLine();
                Console.Write("What is a short description of it? ::: ");
                string simpleDescription = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? :::");
                int simplePoints = int.Parse(Console.ReadLine());
                goal = new SimpleGoal(simpleName, simpleDescription, simplePoints);
                break;
            case 2:
                Console.Write("What is the name of your goal? :::");
                string eternalName = Console.ReadLine();
                Console.Write("What is a short description of it? :::");
                string eternalDescription = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? :::");
                int eternalPoints = int.Parse(Console.ReadLine());
                goal = new EternalGoal(eternalName, eternalDescription, eternalPoints);
                break;
            case 3:
                Console.Write("What is the name of your goal? :::");
                string checklistName = Console.ReadLine();
                Console.Write("What is a short description of it? :::");
                string checklistDescription = Console.ReadLine();
                Console.Write("What is the amount of points associated with this goal? :::");
                int checklistPoints = int.Parse(Console.ReadLine());
                Console.Write("How many times does this goal need to be accomplished for a bonus? :::");
                int target = int.Parse(Console.ReadLine());
                Console.Write("what is the bonus for accomplishing it that many times? :::");
                int bonus = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(checklistName, checklistDescription, checklistPoints, target, bonus);
                break;
            default:
                Console.WriteLine("Invalid choice. Simple Goal created by default.");
                goal = new SimpleGoal("Default Goal", "Default Description", 0);
                break;
        }

        _goals.Add(goal);
        Console.WriteLine("\nGoal created successfully!");

        // Condition to check if badge should be awarded
        if (goal.IsComplete())
        {
            AwardBadge(goal.ShortName);
        }
    }



public void RecordEvent()  // Record events method
{
    Console.WriteLine("\nThe goals are:");
    for (int i = 0; i < _goals.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {_goals[i].ShortName}");
    }

    int choice;
    Console.Write("\nWhich goal did you accomplish? :::");
    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > _goals.Count)
    {
        Console.WriteLine("\nInvalid input. Please enter a valid choice.");
    }
    choice--;

    _goals[choice].RecordEvent();
    _score += _goals[choice].Points;
    Console.WriteLine("\nEvent recorded successfully!");
    Console.WriteLine($"Congratulations! You have earned {_score} points!");
    Console.WriteLine($"You now have {_score} points!");

    // Condition to check if badges should be awarded after recording event
    foreach (var goal in _goals)
    {
        if (goal.IsComplete())
        {
            AwardBadge(goal.ShortName);
        }
    }
}



    public void SaveGoals()  // Save goals to file method
    {
        using (StreamWriter writer = new StreamWriter(GetFileName()))
        {
            // Write the total score
            writer.WriteLine(_score);

            foreach (var goal in _goals)
            {
                // Write the type of goal
                writer.Write(goal.GetType().Name);
                writer.Write(":");

                // Write the goal details
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved to file!");
    }




    public void LoadGoals()  // Load goals from file method
    {
        _goals.Clear();
        using (StreamReader reader = new StreamReader(GetFileName()))
        {
            string line;
            // Read the total score first
            if ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int totalScore))
                {
                    _score = totalScore; // Update the score
                }
                else
                {
                    Console.WriteLine($"Error: Invalid score format in the file.");
                }
            }

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');

                // Making sure the array has the expected length before accessing its elements
                if (parts.Length >= 3)
                {
                    string shortName = parts[0];
                    string description = parts[1];
                    int points;

                    // Attempt to parse points, handle invalid format
                    if (int.TryParse(parts[2], out points))
                    {
                        Goal goal;
                        if (parts.Length == 4)
                        {
                            bool isComplete;
                            if (bool.TryParse(parts[3], out isComplete))
                            {
                                goal = new SimpleGoal(shortName, description, points) { _isComplete = isComplete };
                            }
                            else
                            {
                                Console.WriteLine($"Error: Invalid completion status format in line '{line}'. Skipping this line.");
                                continue;
                            }
                        }
                        else if (parts.Length == 6)
                        {
                            int amountCompleted, target, bonus;
                            if (int.TryParse(parts[3], out amountCompleted) &&
                                int.TryParse(parts[4], out target) &&
                                int.TryParse(parts[5], out bonus))
                            {
                                goal = new ChecklistGoal(shortName, description, points, target, bonus) { _amountCompleted = amountCompleted };
                            }
                            else
                            {
                                Console.WriteLine($"Error: Invalid format for checklist goal in line '{line}'. Skipping this line.");
                                continue;
                            }
                        }
                        else
                        {
                            goal = new EternalGoal(shortName, description, points);
                        }

                        _goals.Add(goal);

                        // Condition to check if the goal is complete and if so, award the badge
                        if (goal.IsComplete())
                        {
                            AwardBadge(goal.ShortName);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error: Invalid points format in line '{line}'. Skipping this line.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Invalid data format in line '{line}'. Skipping this line.");
                }
            }
        }
        Console.WriteLine("Goals loaded from file!");
    }



    public string GetFileName()  // Get file name method
    {
        Console.Write("Enter the file name :::");
        string fileName = Console.ReadLine();
        Console.WriteLine("");

        // Checking if the file name has .txt extension
        if (!fileName.EndsWith(".txt"))
        {
            return fileName + ".txt"; // if not auto add .txt
        }

        // Return the original file name if it already ends with .txt
        return fileName;
    }



private void AwardBadge(string goalName)
{
    Console.WriteLine($"Congratulations! You earned a badge for completing {goalName} goal!");
    // No time to implement complex badge functionality here
}


}
