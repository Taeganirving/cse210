using System;

public class MilestoneItem : ProjectItem
{
    private DateTime _targetDate;

    public MilestoneItem(string name, string description, string priority, DateTime targetDate)
        : base(name, description, priority)
    {
        _targetDate = targetDate;
    }

    public DateTime GetTargetDate() { return _targetDate; }
    public void SetTargetDate(DateTime targetDate) { _targetDate = targetDate; }

    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [Milestone] {GetName()} | Priority: {GetPriority()} | Target: {_targetDate.ToShortDateString()}");
        Console.WriteLine($"Description: {GetDescription()}");
    }

    public override string GetSaveString()
    {
        return $"MILESTONE|{GetName()}|{GetDescription()}|{GetPriority()}|{IsComplete()}|{_targetDate.ToShortDateString()}";
    }
}