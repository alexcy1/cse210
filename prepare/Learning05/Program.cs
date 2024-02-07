using System;
using System.Collections.Generic;

namespace Learning05
{
    class Program
    {
        static void Main()
        {
            // Create a list to hold shapes
            List<Shape> shapes = new List<Shape>();

            // Add a square, rectangle, and circle to the list
            shapes.Add(new Square("red", 5)); // Pass color "red" and side length 5
            shapes.Add(new Rectangle("blue", 4, 6)); // Pass color "blue", length 4, and width 6
            shapes.Add(new Circle("green", 3)); // Pass color "green" and radius 3

            // Iterate through the list of shapes
            foreach (Shape shape in shapes)
            {
                // Call and display the GetColor() and GetArea() methods for each shape
                Console.WriteLine("Color of the shape: " + shape.GetColor());
                Console.WriteLine("Area of the shape: " + shape.GetArea());
                Console.WriteLine(); // Add a blank line for separation
            }
        }
    }
}
