
using System;


public class ChecklistGoal : Goal
{
    public int _amountCompleted;
    public int _target;
    public int _bonus;

    public ChecklistGoal(string shortName, string description, int points, int target, int bonus) : base(shortName, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }


    public override void RecordEvent()
    {
        _amountCompleted++; // Increment the amount completed for the checklist goal
    }


    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }


    // String representation of goal details including completion status.
    public override string GetDetailsString()
    {
        string completionStatus = IsComplete() ? " (Completed)" : "";
        return $"{_shortName}: {_description}{completionStatus}";
    }


    // String representation of goal data for saving purposes.
    public override string GetStringRepresentation()
    {
        return $"{_shortName},{_description},{_points},{_amountCompleted},{_target},{_bonus}";
    }


    // Adding properties for AmountCompleted, Target, and Bonus
    public int AmountCompleted
    {
        get { return _amountCompleted; }
    }

    public int Target
    {
        get { return _target; }
    }

    public int Bonus
    {
        get { return _bonus; }
    }
}
