using System;

public class TaskItem : ProjectItem
{
    // _dueDate: DateTime
    private DateTime _dueDate;

    // TaskItem(name: string, description: string, priority: string, dueDate: DateTime)
    public TaskItem(string name, string description, string priority, DateTime dueDate)
        : base(name, description, priority)
    {
        _dueDate = dueDate;
    }

    // IsOverdue(): bool
    private bool IsOverdue()
    {
        return !IsComplete() && _dueDate.Date < DateTime.Today;
    }

    // Display(): void
    public override void Display()
    {
        string overdueText = IsOverdue() ? " !! OVERDUE" : "";
        Console.WriteLine($"{GetStatusSymbol()} [Task] {Name} | Priority: {Priority} | Due: {_dueDate.ToShortDateString()}{overdueText}");
        Console.WriteLine($"   Description: {Description}");
    }

    // GetSaveString(): string
    public override string GetSaveString()
    {
        return $"TASK|{Name}|{Description}|{Priority}|{IsComplete()}|{_dueDate.ToShortDateString()}";
    }
}