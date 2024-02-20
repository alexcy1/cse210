using System;

// Definition of the Transaction abstract class implementing the ITransaction interface
public abstract class Transaction : ITransaction
{
    // Private fields for the transaction ID, amount, and date
    private int _id;
    private float _amount;
    private DateTime _date;

    // Parameterized constructor to initialize the fields
    protected Transaction(int id, float amount, DateTime date)
    {
        _id = id;
        _amount = amount;
        _date = date;
    }

    // Implementation of the GetId method from the ITransaction interface
    public int GetId() => _id;

    // Implementation of the GetAmount method from the ITransaction interface
    public float GetAmount() => _amount;

    // Implementation of the GetDate method from the ITransaction interface
    public DateTime GetDate() => _date;

    // Method to set the transaction ID
    public void SetId(int id) => _id = id;

    // Method to set the transaction amount
    public void SetAmount(float amount) => _amount = amount;

    // Method to set the transaction date
    public void SetDate(DateTime date) => _date = date;
}
