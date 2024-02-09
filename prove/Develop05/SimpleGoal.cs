using System;


public class SimpleGoal : Goal
{
    public bool _isComplete; 


    // Constructor
    public SimpleGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
        _isComplete = false; // Initialize _isComplete to false by default
    }


    public override void RecordEvent()
    {
        _isComplete = true; // Mark the simple goal as complete
    }


    // Override IsComplete method
    public override bool IsComplete()
    {
        return _isComplete; 
    }


    // Override GetStringRepresentation method
    public override string GetStringRepresentation()
    {
        return $"{_shortName},{_description},{_points},{_isComplete}";
    }


    // String representation of goal details including completion status.
    public override string GetDetailsString()
    {
        string completionStatus = IsComplete() ? " (Completed)" : "";
        return $"{_shortName}: {_description}{completionStatus}";
    }

}
