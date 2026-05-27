using System;
using System.Collections.Generic;

public class Project
{
    private string _name;
    private string _description;
    private string _category;
    private string _status;
    private string _priority;
    private double _budgetGoal;

    private List<ProjectItem> _items = new List<ProjectItem>();

    public Project(string name, string description, string category, string status, string priority, double budgetGoal)
    {
        _name = name;
        _description = description;
        _category = category;
        _status = status;
        _priority = priority;
        _budgetGoal = budgetGoal;
    }

    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public string GetCategory() { return _category; }
    public string GetStatus() { return _status; }
    public string GetPriority() { return _priority; }
    public double GetBudgetGoal() { return _budgetGoal; }

    public void SetName(string name) { _name = name; }
    public void SetDescription(string description) { _description = description; }
    public void SetCategory(string category) { _category = category; }
    public void SetStatus(string status) { _status = status; }
    public void SetPriority(string priority) { _priority = priority; }
    public void SetBudgetGoal(double budgetGoal) { _budgetGoal = budgetGoal; }

    public bool HasBudgetGoal()
    {
        return _budgetGoal > 0;
    }

    public double GetBudgetDifference()
    {
        return _budgetGoal - GetTotalExpenses();
    }

    public int GetBudgetUsedPercentage()
    {
        if (!HasBudgetGoal())
        {
            return 0;
        }

        return (int)((GetTotalExpenses() / _budgetGoal) * 100);
    }

    public void AddItem(ProjectItem item)
    {
        _items.Add(item);
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items.RemoveAt(index);
        }
    }

    public List<ProjectItem> GetItems()
    {
        return _items;
    }

    public int GetCompletedItemCount()
    {
        int count = 0;

        foreach (ProjectItem item in _items)
        {
            if (item.IsComplete())
            {
                count++;
            }
        }

        return count;
    }

    public int GetIncompleteItemCount()
    {
        return _items.Count - GetCompletedItemCount();
    }

    public double GetTotalExpenses()
    {
        double total = 0;

        foreach (ProjectItem item in _items)
        {
            if (item is ExpenseItem expense)
            {
                total += expense.GetAmount();
            }
        }

        return total;
    }

    public int GetProgressPercentage()
    {
        if (_items.Count == 0)
        {
            return 0;
        }

        return (GetCompletedItemCount() * 100) / _items.Count;
    }

    public void DisplayProject()
    {
        Console.WriteLine();
        Console.WriteLine($"=== {_name} ===");
        Console.WriteLine($"Category: {_category}");
        Console.WriteLine($"Status: {_status}");
        Console.WriteLine($"Priority: {_priority}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine();

        Console.WriteLine("Dashboard:");
        Console.WriteLine($"Progress: {GetProgressPercentage()}%");
        Console.WriteLine($"Completed Items: {GetCompletedItemCount()}");
        Console.WriteLine($"Remaining Items: {GetIncompleteItemCount()}");
        Console.WriteLine($"Total Expenses: ${GetTotalExpenses():F2}");

        if (HasBudgetGoal())
        {
            Console.WriteLine($"Budget Goal: ${_budgetGoal:F2}");
            Console.WriteLine($"Budget Used: {GetBudgetUsedPercentage()}%");

            double difference = GetBudgetDifference();

            if (difference > 0)
            {
                Console.WriteLine($"Under Budget By: ${difference:F2}");
            }
            else if (difference < 0)
            {
                Console.WriteLine($"Over Budget By: ${Math.Abs(difference):F2}");
            }
            else
            {
                Console.WriteLine("Budget Met Exactly.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Project Items:");

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
}