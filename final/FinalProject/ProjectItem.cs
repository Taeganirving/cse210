using System;

public abstract class ProjectItem
{
    // _name: string
    private string _name;
    // _description: string
    private string _description;
    // _isComplete: bool
    private bool _isComplete;
    // _priority: string
    private string _priority;

    // ProjectItem(name: string, description: string, priority: string)
    public ProjectItem(string name, string description, string priority)
    {
        _name = name;
        _description = description;
        _priority = priority;
        _isComplete = false;
    }

    // GetStatusSymbol(): string
    protected string GetStatusSymbol()
    {
        return _isComplete ? "[X]" : "[ ]";
    }

    // IsComplete(): bool
    public bool IsComplete() { return _isComplete; }

    // MarkComplete(): void
    public void MarkComplete() { _isComplete = true; }

    // MarkIncomplete(): void
    public void MarkIncomplete() { _isComplete = false; }

    // Display(): void
    public abstract void Display();

    // GetSaveString(): string  -- used by ProjectManager.Save()
    public abstract string GetSaveString();

    // Preserve completion state when item is replaced via editing
    // CopyStatusTo(target: ProjectItem): void
    public void CopyStatusTo(ProjectItem target)
    {
        if (_isComplete) target.MarkComplete();
    }

    // --- Minimal read access needed for save/display in subclasses ---
    protected string Name        { get { return _name; } }
    protected string Description { get { return _description; } }
    protected string Priority    { get { return _priority; } }
}