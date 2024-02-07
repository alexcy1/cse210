using System;

public class Circle : Shape
{
    private double _radius;

    // Constructor for Circle
    public Circle(string color, double radius) : base(color)
    {
        SetColor(color);
        _radius = radius;
    }

    // Method to calculate the area of the circle
    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}
