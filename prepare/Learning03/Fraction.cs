using System;

    public class Fraction
    {
        private int _top;
        private int _bottom;

        // Default Constructor
        public Fraction()
        {
            _top = 1;
            _bottom = 1;
        }

        // Constructor with one parameter for the top
        public Fraction(int top)
        {
            _top = top;
            _bottom = 1;
        }

        // Constructor with two parameters for the top and bottom
        public Fraction(int top, int bottom)
        {
            _top = top;
            _bottom = bottom;
        }

        // Top ---------------------------------------------------
        public int GetTop()
        {
            return _top;
        }

        public void SetTop(int top) // Set top
        {
            _top = top;
        }

        // Bottom ---------------------------------------------------
        public int GetBottom()
        {
            return _bottom;
        }

        public void SetBottom(int bottom) // Set bottom
        {
            _bottom = bottom;
        }

        // GetFractionString method
        public string GetFractionString()
        {
            return $"{_top}/{_bottom}";
        }

        // GetDecimalValue method
        public double GetDecimalValue()
        {
            return (double)_top / _bottom;
        }


        // Override ToString() to display the fraction
        public override string ToString()
        {
            return $"{_top}/{_bottom}";
        }


    }