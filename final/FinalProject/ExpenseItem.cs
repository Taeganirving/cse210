using System;

public class ExpenseItem : ProjectItem
{
    private double _amount;
    private string _category;

    public ExpenseItem(string name, string description, string priority, double amount, string category)
        : base(name, description, priority)
    {
        _amount = amount;
        _category = category;
    }

    public double GetAmount() { return _amount; }
    public string GetCategory() { return _category; }

    public void SetAmount(double amount) { _amount = amount; }
    public void SetCategory(string category) { _category = category; }

    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [$] {GetName()} | ${_amount:F2} | Category: {_category} | Priority: {GetPriority()}");
        Console.WriteLine($"Description: {GetDescription()}");
    }

    public override string GetSaveString()
    {
        return $"EXPENSE|{GetName()}|{GetDescription()}|{GetPriority()}|{IsComplete()}|{_amount}|{_category}";
    }
}