using System;
using System.Collections.Generic;
using System.IO;

public class FileManager
{
    public void SaveProjects(List<Project> projects, string fileName)
    {
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            foreach (Project project in projects)
            {
                outputFile.WriteLine(
                    $"PROJECT|{project.GetName()}|" +
                    $"{project.GetDescription()}|" +
                    $"{project.GetCategory()}|" +
                    $"{project.GetStatus()}|" +
                    $"{project.GetPriority()}|" +
                    $"{project.GetBudgetGoal()}"
                );

                foreach (ProjectItem item in project.GetItems())
                {
                    outputFile.WriteLine(item.GetSaveString());
                }

                outputFile.WriteLine("ENDPROJECT");
            }
        }
    }

    public void LoadProjects(ProjectManager manager, string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        manager.ClearProjects();

        string[] lines = File.ReadAllLines(fileName);
        Project currentProject = null;

        foreach (string line in lines)
        {
            string[] parts = line.Split("|");

            if (parts[0] == "PROJECT")
            {
                currentProject = new Project(
                    parts[1],
                    parts[2],
                    parts[3],
                    parts[4],
                    parts[5],
                    double.Parse(parts[6])
                );

                manager.AddProject(currentProject);
            }
            else if (parts[0] == "TASK")
            {
                TaskItem task = new TaskItem(
                    parts[1],
                    parts[2],
                    parts[3],
                    DateTime.Parse(parts[5])
                );

                if (parts[4] == "True")
                {
                    task.MarkComplete();
                }

                currentProject.AddItem(task);
            }
            else if (parts[0] == "MILESTONE")
            {
                MilestoneItem milestone = new MilestoneItem(
                    parts[1],
                    parts[2],
                    parts[3],
                    DateTime.Parse(parts[5])
                );

                if (parts[4] == "True")
                {
                    milestone.MarkComplete();
                }

                currentProject.AddItem(milestone);
            }
            else if (parts[0] == "EXPENSE")
            {
                ExpenseItem expense = new ExpenseItem(
                    parts[1],
                    parts[2],
                    parts[3],
                    double.Parse(parts[5]),
                    parts[6]
                );

                if (parts[4] == "True")
                {
                    expense.MarkComplete();
                }

                currentProject.AddItem(expense);
            }
            else if (parts[0] == "NOTE")
            {
                NoteItem note = new NoteItem(
                    parts[1],
                    parts[2],
                    parts[3],
                    DateTime.Parse(parts[5])
                );

                if (parts[4] == "True")
                {
                    note.MarkComplete();
                }

                currentProject.AddItem(note);
            }
        }
    }
}