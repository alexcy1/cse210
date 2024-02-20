using System;

// Definition of the Income class, inheriting from Transaction
class Income : Transaction
{
    // No additional attributes/methods for now

    // Parameterized constructor using the base keyword to call the constructor of the base class (Transaction)
    public Income(int id, float amount, DateTime date)
        : base(id, amount, date)
    {
        // No additional initialization specific to Income...
    }

    // Method to display transaction history specific to Income
    public void DisplayTransactionHistory()
    {
        Console.WriteLine($"Income ID: {GetId()}, Amount: {GetAmount()}, Date: {GetDate()}");
    }
}
