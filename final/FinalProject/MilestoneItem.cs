using System;

public class MilestoneItem : ProjectItem
{
    // _targetDate: DateTime
    private DateTime _targetDate;

    // MilestoneItem(name: string, description: string, priority: string, targetDate: DateTime)
    public MilestoneItem(string name, string description, string priority, DateTime targetDate)
        : base(name, description, priority)
    {
        _targetDate = targetDate;
    }

    // Display(): void
    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [Milestone] {Name} | Priority: {Priority} | Target: {_targetDate.ToShortDateString()}");
        Console.WriteLine($"   Description: {Description}");
    }

    // GetSaveString(): string
    public override string GetSaveString()
    {
        return $"MILESTONE|{Name}|{Description}|{Priority}|{IsComplete()}|{_targetDate.ToShortDateString()}";
    }
}