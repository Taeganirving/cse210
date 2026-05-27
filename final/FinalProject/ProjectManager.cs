using System;
using System.Collections.Generic;

public class ProjectManager
{
    private List<Project> _projects = new List<Project>();

    public void AddProject(Project project)
    {
        _projects.Add(project);
    }

    public List<Project> GetProjects()
    {
        return _projects;
    }

    public void ClearProjects()
    {
        _projects.Clear();
    }

    public void DisplayAllProjects()
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
            Project project = _projects[i];

            Console.WriteLine(
                $"{i + 1}. {project.GetName()} | " +
                $"{project.GetCategory()} | " +
                $"{project.GetStatus()} | " +
                $"{project.GetPriority()} | " +
                $"{project.GetProgressPercentage()}% Complete | " +
                $"${project.GetTotalExpenses():F2}"
            );
        }
    }

    public void DisplayProjectsByCategory(string category)
    {
        Console.WriteLine();
        Console.WriteLine($"Projects in category: {category}");

        bool found = false;

        for (int i = 0; i < _projects.Count; i++)
        {
            if (_projects[i].GetCategory().ToLower() == category.ToLower())
            {
                Console.WriteLine($"{i + 1}. {_projects[i].GetName()}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No projects found in that category.");
        }
    }

    public Project SelectProject(int index)
    {
        if (index >= 0 && index < _projects.Count)
        {
            return _projects[index];
        }

        return null;
    }

    public void RemoveProject(int index)
    {
        if (index >= 0 && index < _projects.Count)
        {
            _projects.RemoveAt(index);
        }
    }
}