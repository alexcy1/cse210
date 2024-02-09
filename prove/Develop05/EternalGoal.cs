using System;

public class EternalGoal : Goal
{
    public EternalGoal(string shortName, string description, int points) : base(shortName, description, points)
    {
    }

    public override void RecordEvent()
    {
        // No further action needed for Eternal Goal
    }

    public override bool IsComplete()
    {
        return false; // Eternal Goal is never complete
    }


    public override string GetDetailsString()
    {
        string completionStatus = IsComplete() ? " (Completed)" : "";
        return $"{_shortName}: {_description}{completionStatus}";
    }


    public override string GetStringRepresentation()
    {
        //return $"{_shortName},{_description},{_points},false";
        return $"{_shortName},{_description},{_points}";
    }
}


