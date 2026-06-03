using System;
using System.Collections.Generic;
using System.IO;

public class ProjectManager
{
    // _projects: List<Project>
    private List<Project> _projects = new List<Project>();

    // AddProject(project: Project): void
    public void AddProject(Project project)
    {
        _projects.Add(project);
    }

    // RemoveProject(index: int): void
    public void RemoveProject(int index)
    {
        if (index >= 0 && index < _projects.Count)
            _projects.RemoveAt(index);
    }

    // SelectProject(index: int): Project
    public Project SelectProject(int index)
    {
        if (index >= 0 && index < _projects.Count)
            return _projects[index];
        return null;
    }

    // HasProjects(): bool
    public bool HasProjects() { return _projects.Count > 0; }

    // DisplayAll(): void
    public void DisplayAll()
    {
        Console.WriteLine();
        Console.WriteLine("Projects:");

        if (_projects.Count == 0)
        {
            Console.WriteLine("No projects available.");
            return;
        }

        for (int i = 0; i < _projects.Count; i++)
        {
            Project p = _projects[i];
            Console.WriteLine(
                $"{i + 1}. {p.GetName()} | {p.GetCategory()} | {p.GetStatus()} | " +
                $"{p.GetPriority()} | {p.GetProgressPercentage()}% Complete | ${p.GetTotalExpenses():F2}"
            );
        }
    }

    // DisplayByCategory(category: string): void
    public void DisplayByCategory(string category)
    {
        Console.WriteLine();
        Console.WriteLine($"Projects in category '{category}':");

        bool found = false;
        for (int i = 0; i < _projects.Count; i++)
        {
            if (_projects[i].GetCategory().Equals(category, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{i + 1}. {_projects[i].GetName()}");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No projects found in that category.");
    }

    // Save(fileName: string): void
    // Consolidated from FileManager -- belongs here alongside the data it manages
    public void Save(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Project project in _projects)
                project.Save(writer);
        }
    }

    // Load(fileName: string): void
    // Consolidated from FileManager
    public void Load(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        _projects.Clear();

        string[] lines = File.ReadAllLines(fileName);
        Project current = null;

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            if (parts[0] == "PROJECT")
            {
                current = new Project(parts[1], parts[2], parts[3], parts[4], parts[5], double.Parse(parts[6]));
                _projects.Add(current);
            }
            else if (parts[0] == "TASK" && current != null)
            {
                TaskItem task = new TaskItem(parts[1], parts[2], parts[3], DateTime.Parse(parts[5]));
                if (parts[4] == "True") task.MarkComplete();
                current.AddItem(task);
            }
            else if (parts[0] == "MILESTONE" && current != null)
            {
                MilestoneItem milestone = new MilestoneItem(parts[1], parts[2], parts[3], DateTime.Parse(parts[5]));
                if (parts[4] == "True") milestone.MarkComplete();
                current.AddItem(milestone);
            }
            else if (parts[0] == "EXPENSE" && current != null)
            {
                ExpenseItem expense = new ExpenseItem(parts[1], parts[2], parts[3], double.Parse(parts[5]), parts[6]);
                if (parts[4] == "True") expense.MarkComplete();
                current.AddItem(expense);
            }
            else if (parts[0] == "NOTE" && current != null)
            {
                NoteItem note = new NoteItem(parts[1], parts[2], parts[3], DateTime.Parse(parts[5]));
                if (parts[4] == "True") note.MarkComplete();
                current.AddItem(note);
            }
        }
    }
}