using System;

public abstract class Transaction : ITransaction
{
    private int _id;
    private float _amount;
    private DateTime _date;

    // Parameterized constructor
    protected Transaction(int id, float amount, DateTime date)
    {
        _id = id;
        _amount = amount;
        _date = date;
    }

    public int GetId() => _id; // The ID of the transaction.
    public float GetAmount() => _amount; // The amount of the transaction.
    public DateTime GetDate() => _date; // The date of the transaction.

    public void SetId(int id) => _id = id; // The new ID to set.
    public void SetAmount(float amount) => _amount = amount; // The new amount to set.
    public void SetDate(DateTime date) => _date = date; // The new date to set.
}