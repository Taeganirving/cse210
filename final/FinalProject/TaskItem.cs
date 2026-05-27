using System;

public class TaskItem : ProjectItem
{
    private DateTime _dueDate;

    public TaskItem(string name, string description, string priority, DateTime dueDate)
        : base(name, description, priority)
    {
        _dueDate = dueDate;
    }

    public DateTime GetDueDate() { return _dueDate; }
    public void SetDueDate(DateTime dueDate) { _dueDate = dueDate; }

    public bool IsOverdue()
    {
        return !IsComplete() && _dueDate.Date < DateTime.Today;
    }

    public override void Display()
    {
        string overdueText = IsOverdue() ? " OVERDUE" : "";

        Console.WriteLine($"{GetStatusSymbol()} [Task] {GetName()} | Priority: {GetPriority()} | Due: {_dueDate.ToShortDateString()}{overdueText}");
        Console.WriteLine($"Description: {GetDescription()}");
    }

    public override string GetSaveString()
    {
        return $"TASK|{GetName()}|{GetDescription()}|{GetPriority()}|{IsComplete()}|{_dueDate.ToShortDateString()}";
    }
}