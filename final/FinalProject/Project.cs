using System;
using System.Collections.Generic;
using System.IO;

public class Project
{
    // _name: string
    private string _name;
    // _description: string
    private string _description;
    // _category: string
    private string _category;
    // _status: string
    private string _status;
    // _priority: string
    private string _priority;
    // _budgetGoal: double
    private double _budgetGoal;
    // _items: List<ProjectItem>
    private List<ProjectItem> _items = new List<ProjectItem>();

    // Project(name: string, description: string, category: string, status: string, priority: string, budgetGoal: double)
    public Project(string name, string description, string category, string status, string priority, double budgetGoal)
    {
        _name = name;
        _description = description;
        _category = category;
        _status = status;
        _priority = priority;
        _budgetGoal = budgetGoal;
    }

    // --- Read access needed by ProjectManager for listing/filtering ---
    public string GetName()     { return _name; }
    public string GetCategory() { return _category; }
    public string GetStatus()   { return _status; }
    public string GetPriority() { return _priority; }

    // Update(name: string, description: string, category: string, status: string, priority: string, budgetGoal: double): void
    // Replaces all project info at once rather than individual setters
    public void Update(string name, string description, string category, string status, string priority, double budgetGoal)
    {
        _name        = name;
        _description = description;
        _category    = category;
        _status      = status;
        _priority    = priority;
        _budgetGoal  = budgetGoal;
    }

    // AddItem(item: ProjectItem): void
    public void AddItem(ProjectItem item)
    {
        _items.Add(item);
    }

    // ReplaceItem(index: int, newItem: ProjectItem): void
    // Editing creates a new item to replace the old one, preserving completion state
    public void ReplaceItem(int index, ProjectItem newItem)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items[index].CopyStatusTo(newItem);
            _items[index] = newItem;
        }
    }

    // RemoveItem(index: int): void
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items.RemoveAt(index);
        }
    }

    // MarkItemComplete(index: int): bool
    public bool MarkItemComplete(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items[index].MarkComplete();
            return true;
        }
        return false;
    }

    // MarkItemIncomplete(index: int): bool
    public bool MarkItemIncomplete(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items[index].MarkIncomplete();
            return true;
        }
        return false;
    }

    // GetItemCount(): int
    public int GetItemCount() { return _items.Count; }

    // GetItemType(index: int): string
    public string GetItemType(int index)
    {
        if (index < 0 || index >= _items.Count) return "";
        ProjectItem item = _items[index];
        if (item is TaskItem)      return "task";
        if (item is MilestoneItem) return "milestone";
        if (item is ExpenseItem)   return "expense";
        if (item is NoteItem)      return "note";
        return "";
    }

    // --- Private helpers ---

    // TotalExpenses(): double
    private double TotalExpenses()
    {
        double total = 0;
        foreach (ProjectItem item in _items)
        {
            if (item is ExpenseItem expense)
                total += expense.GetAmount();
        }
        return total;
    }

    // CompletedCount(): int
    private int CompletedCount()
    {
        int count = 0;
        foreach (ProjectItem item in _items)
            if (item.IsComplete()) count++;
        return count;
    }

    // ProgressPercentage(): int
    private int ProgressPercentage()
    {
        if (_items.Count == 0) return 0;
        return (CompletedCount() * 100) / _items.Count;
    }

    // BudgetUsedPercentage(): int
    private int BudgetUsedPercentage()
    {
        if (_budgetGoal <= 0) return 0;
        return (int)((TotalExpenses() / _budgetGoal) * 100);
    }

    // GetProgressPercentage(): int  -- needed by ProjectManager for the list view
    public int GetProgressPercentage() { return ProgressPercentage(); }

    // GetTotalExpenses(): double  -- needed by ProjectManager for the list view
    public double GetTotalExpenses() { return TotalExpenses(); }

    // Display(): void
    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine($"=== {_name} ===");
        Console.WriteLine($"Category: {_category}  |  Status: {_status}  |  Priority: {_priority}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine();

        Console.WriteLine("--- Dashboard ---");
        Console.WriteLine($"Progress:          {ProgressPercentage()}%");
        Console.WriteLine($"Completed Items:   {CompletedCount()}");
        Console.WriteLine($"Remaining Items:   {_items.Count - CompletedCount()}");
        Console.WriteLine($"Total Expenses:    ${TotalExpenses():F2}");

        if (_budgetGoal > 0)
        {
            double diff = _budgetGoal - TotalExpenses();
            Console.WriteLine($"Budget Goal:       ${_budgetGoal:F2}");
            Console.WriteLine($"Budget Used:       {BudgetUsedPercentage()}%");

            if (diff > 0)
                Console.WriteLine($"Under Budget By:   ${diff:F2}");
            else if (diff < 0)
                Console.WriteLine($"Over Budget By:    ${Math.Abs(diff):F2}");
            else
                Console.WriteLine("Budget Met Exactly.");
        }

        Console.WriteLine();
        Console.WriteLine("--- Items ---");

        if (_items.Count == 0)
        {
            Console.WriteLine("No items added yet.");
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            _items[i].Display();
            Console.WriteLine();
        }
    }

    // Save(writer: StreamWriter): void
    // FileManager responsibility moved here -- Project knows its own data
    public void Save(StreamWriter writer)
    {
        writer.WriteLine($"PROJECT|{_name}|{_description}|{_category}|{_status}|{_priority}|{_budgetGoal}");
        foreach (ProjectItem item in _items)
            writer.WriteLine(item.GetSaveString());
        writer.WriteLine("ENDPROJECT");
    }
}