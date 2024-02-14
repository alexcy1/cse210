using System;

class Expense : Transaction
{
    private string _name;
    private string _category;

    // Parameterized constructor in the derived class using base keyword
    public Expense(int id, float amount, DateTime date, string name, string category)
        : base(id, amount, date)
    {
        _name = name;
        _category = category;
    }

    public string GetName() => _name;
    public string GetCategory() => _category;

    public void SetName(string name) => _name = name;
    public void SetCategory(string category) => _category = category;
}
