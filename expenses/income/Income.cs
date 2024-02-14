using System;

class Income : Transaction
{
    // No additional attributes/methods for now

    // Parameterized constructor in the derived class using base keyword
    public Income(int id, float amount, DateTime date)
        : base(id, amount, date)
    {
        // No additional initialization specific to Income
    }

    public void DisplayTransactionHistory()
    {
        Console.WriteLine("Transation history goes here");
    }
}
