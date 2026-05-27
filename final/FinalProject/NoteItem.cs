using System;

public class NoteItem : ProjectItem
{
    private DateTime _date;

    public NoteItem(string name, string description, string priority, DateTime date)
        : base(name, description, priority)
    {
        _date = date;
    }

    public DateTime GetDate() { return _date; }
    public void SetDate(DateTime date) { _date = date; }

    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [Note] {GetName()} | Date: {_date.ToShortDateString()} | Priority: {GetPriority()}");
        Console.WriteLine($"Note: {GetDescription()}");
    }

    public override string GetSaveString()
    {
        return $"NOTE|{GetName()}|{GetDescription()}|{GetPriority()}|{IsComplete()}|{_date.ToShortDateString()}";
    }
}