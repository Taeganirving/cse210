using System;

public class NoteItem : ProjectItem
{
    // _date: DateTime
    private DateTime _date;

    // NoteItem(name: string, description: string, priority: string, date: DateTime)
    public NoteItem(string name, string description, string priority, DateTime date)
        : base(name, description, priority)
    {
        _date = date;
    }

    // Display(): void
    public override void Display()
    {
        Console.WriteLine($"{GetStatusSymbol()} [Note] {Name} | Date: {_date.ToShortDateString()} | Priority: {Priority}");
        Console.WriteLine($"   Note: {Description}");
    }

    // GetSaveString(): string
    public override string GetSaveString()
    {
        return $"NOTE|{Name}|{Description}|{Priority}|{IsComplete()}|{_date.ToShortDateString()}";
    }
}