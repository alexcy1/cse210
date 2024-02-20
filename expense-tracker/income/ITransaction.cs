using System;

// Definition of the ITransaction interface
public interface ITransaction
{
    // Method to get the ID of the transaction
    int GetId();

    // Method to get the amount of the transaction
    float GetAmount();

    // Method to get the date of the transaction
    DateTime GetDate();
}
