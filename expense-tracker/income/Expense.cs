using System;

// Definition of the Expense class, inheriting from Transaction
class Expense : Transaction
{
    // Private fields to store name and category
    private string _name;
    private string _category;

    // Parameterized constructor using the base keyword to call the constructor of the base class (Transaction)
    public Expense(int id, float amount, DateTime date, string name, string category)
        : base(id, amount, date)
    {
        // Initializing the private fields in the derived class constructor
        _name = name;
        _category = category;
    }

    // Getter methods for name and category
    public string GetName() => _name;
    public string GetCategory() => _category;

    // Setter methods for name and category
    public void SetName(string name) => _name = name;
    public void SetCategory(string category) => _category = category;
}
