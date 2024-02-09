using System;

public abstract class Goal
{
    protected string _shortName;
    protected int _points;
    protected string _description;

    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();

    public virtual string GetDetailsString()
    {
        return $"{_shortName} ({_description})";
    }

    public abstract string GetStringRepresentation();

    // Properties
    public string ShortName
    {
        get { return _shortName; }
    }

    public int Points
    {
        get { return _points; }
    }
}
