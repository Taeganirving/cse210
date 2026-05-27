using System;

public abstract class ProjectItem
{
    private string _name;
    private string _description;
    private bool _isComplete;
    private string _priority;

    public ProjectItem(string name, string description, string priority)
    {
        _name = name;
        _description = description;
        _priority = priority;
        _isComplete = false;
    }

    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public string GetPriority() { return _priority; }
    public bool IsComplete() { return _isComplete; }

    public void SetName(string name) { _name = name; }
    public void SetDescription(string description) { _description = description; }
    public void SetPriority(string priority) { _priority = priority; }

    public void MarkComplete() { _isComplete = true; }
    public void MarkIncomplete() { _isComplete = false; }

    public string GetStatusSymbol()
    {
        return _isComplete ? "[X]" : "[ ]";
    }

    public abstract void Display();
    public abstract string GetSaveString();
}