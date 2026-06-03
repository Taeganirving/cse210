using System;

public class ExpenseItem : ProjectItem
{
    // _amount: double
    private double _amount;
    // _category: string
    private string _category;

    // ExpenseItem(name: string, description: string, priority: string, amount: double, category: string)
    public ExpenseItem(string name, string description, string priority, double amount, string category)
        : base(name, description, priority)
    {
        _amount = amount;
        _category = category;
    }

    // GetAmount(): double  -- needed by Project to total expenses
    public double GetAmount() { return _amount; }

    // Display(): void
    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [$] {Name} | ${_amount:F2} | Category: {_category} | Priority: {Priority}");
        Console.WriteLine($"   Description: {Description}");
    }

    // GetSaveString(): string
    public override string GetSaveString()
    {
        return $"EXPENSE|{Name}|{Description}|{Priority}|{IsComplete()}|{_amount}|{_category}";
    }
}