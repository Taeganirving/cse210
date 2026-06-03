using System;

class Program
{
    static void Main(string[] args)
    {
        ProjectManager manager = new ProjectManager();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== LifeBuild ===");
            Console.WriteLine("1. Create Project");
            Console.WriteLine("2. View Projects");
            Console.WriteLine("3. Select Project");
            Console.WriteLine("4. Filter Projects by Category");
            Console.WriteLine("5. Delete Project");
            Console.WriteLine("6. Save Projects");
            Console.WriteLine("7. Load Projects");
            Console.WriteLine("8. Quit");

            int choice = GetIntInput("Choose an option: ");

            if      (choice == 1) CreateProject(manager);
            else if (choice == 2) { manager.DisplayAll(); Pause(); }
            else if (choice == 3) SelectProjectMenu(manager);
            else if (choice == 4) FilterByCategory(manager);
            else if (choice == 5) DeleteProject(manager);
            else if (choice == 6) { manager.Save("lifeBuildSave.txt"); Console.WriteLine("Projects saved."); Pause(); }
            else if (choice == 7) { manager.Load("lifeBuildSave.txt"); Console.WriteLine("Projects loaded."); Pause(); }
            else if (choice == 8) running = false;
            else { Console.WriteLine("Invalid option."); Pause(); }
        }
    }

    static void CreateProject(ProjectManager manager)
    {
        Console.Clear();
        Console.Write("Project Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.WriteLine("Suggested Categories: Vehicle, Software, Fitness, Outdoor, Business, Home, School");
        Console.Write("Category: ");
        string category = Console.ReadLine();

        Console.Write("Status: ");
        string status = Console.ReadLine();

        Console.Write("Priority: ");
        string priority = Console.ReadLine();

        double budgetGoal = GetDoubleInput("Budget Goal (enter 0 for none): ");

        manager.AddProject(new Project(name, description, category, status, priority, budgetGoal));
        Console.WriteLine("Project created.");
        Pause();
    }

    static void SelectProjectMenu(ProjectManager manager)
    {
        Console.Clear();
        manager.DisplayAll();
        Console.WriteLine();

        int index = GetIntInput("Select project number: ") - 1;
        Project project = manager.SelectProject(index);

        if (project == null) { Console.WriteLine("Invalid project."); Pause(); return; }

        bool inMenu = true;
        while (inMenu)
        {
            Console.Clear();
            project.Display();
            Console.WriteLine();
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Add Milestone");
            Console.WriteLine("4. Add Note");
            Console.WriteLine("5. Mark Item Complete");
            Console.WriteLine("6. Mark Item Incomplete");
            Console.WriteLine("7. Edit Item (replaces with new)");
            Console.WriteLine("8. Delete Item");
            Console.WriteLine("9. Edit Project Info");
            Console.WriteLine("10. Back");

            int choice = GetIntInput("Choose an option: ");

            if      (choice == 1)  AddTask(project);
            else if (choice == 2)  AddExpense(project);
            else if (choice == 3)  AddMilestone(project);
            else if (choice == 4)  AddNote(project);
            else if (choice == 5)  ToggleItem(project, complete: true);
            else if (choice == 6)  ToggleItem(project, complete: false);
            else if (choice == 7)  EditItem(project);
            else if (choice == 8)  DeleteItem(project);
            else if (choice == 9)  EditProjectInfo(project);
            else if (choice == 10) inMenu = false;
            else { Console.WriteLine("Invalid option."); Pause(); }
        }
    }

    static void AddTask(Project project)
    {
        Console.Clear();
        Console.Write("Task Name: ");        string name = Console.ReadLine();
        Console.Write("Description: ");     string desc = Console.ReadLine();
        Console.Write("Priority: ");        string pri  = Console.ReadLine();
        DateTime due = GetDateInput("Due Date: ");

        project.AddItem(new TaskItem(name, desc, pri, due));
        Console.WriteLine("Task added."); Pause();
    }

    static void AddExpense(Project project)
    {
        Console.Clear();
        Console.Write("Expense Name: ");    string name = Console.ReadLine();
        Console.Write("Description: ");    string desc = Console.ReadLine();
        Console.Write("Priority: ");       string pri  = Console.ReadLine();
        double amount = GetDoubleInput("Amount: ");
        Console.Write("Category: ");       string cat  = Console.ReadLine();

        project.AddItem(new ExpenseItem(name, desc, pri, amount, cat));
        Console.WriteLine("Expense added."); Pause();
    }

    static void AddMilestone(Project project)
    {
        Console.Clear();
        Console.Write("Milestone Name: ");  string name = Console.ReadLine();
        Console.Write("Description: ");    string desc = Console.ReadLine();
        Console.Write("Priority: ");       string pri  = Console.ReadLine();
        DateTime target = GetDateInput("Target Date: ");

        project.AddItem(new MilestoneItem(name, desc, pri, target));
        Console.WriteLine("Milestone added."); Pause();
    }

    static void AddNote(Project project)
    {
        Console.Clear();
        Console.Write("Note Title: ");     string name = Console.ReadLine();
        Console.Write("Note: ");           string desc = Console.ReadLine();
        Console.Write("Priority: ");      string pri  = Console.ReadLine();
        DateTime date = GetDateInput("Date: ");

        project.AddItem(new NoteItem(name, desc, pri, date));
        Console.WriteLine("Note added."); Pause();
    }

    static void ToggleItem(Project project, bool complete)
    {
        Console.Clear();
        project.Display();

        if (project.GetItemCount() == 0)
        {
            Console.WriteLine($"No items to mark {(complete ? "complete" : "incomplete")}.");
            Pause(); return;
        }

        int index = GetIntInput($"Select item number to mark {(complete ? "complete" : "incomplete")}: ") - 1;
        bool ok = complete ? project.MarkItemComplete(index) : project.MarkItemIncomplete(index);

        Console.WriteLine(ok ? $"Item marked {(complete ? "complete" : "incomplete")}." : "Invalid item.");
        Pause();
    }

    // EditItem: collects new data and asks Project to replace the item
    static void EditItem(Project project)
    {
        Console.Clear();
        project.Display();

        if (project.GetItemCount() == 0)
        {
            Console.WriteLine("No items to edit."); Pause(); return;
        }

        int index = GetIntInput("Select item number to edit: ") - 1;
        string type = project.GetItemType(index);

        if (type == "")
        {
            Console.WriteLine("Invalid item."); Pause(); return;
        }

        Console.Write("New Name: ");        string name = Console.ReadLine();
        Console.Write("New Description: "); string desc = Console.ReadLine();
        Console.Write("New Priority: ");    string pri  = Console.ReadLine();

        ProjectItem replacement = null;

        if (type == "task")
        {
            DateTime due = GetDateInput("New Due Date: ");
            replacement = new TaskItem(name, desc, pri, due);
        }
        else if (type == "milestone")
        {
            DateTime target = GetDateInput("New Target Date: ");
            replacement = new MilestoneItem(name, desc, pri, target);
        }
        else if (type == "expense")
        {
            double amount = GetDoubleInput("New Amount: ");
            Console.Write("New Category: "); string cat = Console.ReadLine();
            replacement = new ExpenseItem(name, desc, pri, amount, cat);
        }
        else if (type == "note")
        {
            DateTime date = GetDateInput("New Date: ");
            replacement = new NoteItem(name, desc, pri, date);
        }

        project.ReplaceItem(index, replacement);
        Console.WriteLine("Item updated."); Pause();
    }

    static void DeleteItem(Project project)
    {
        Console.Clear();
        project.Display();

        if (project.GetItemCount() == 0)
        {
            Console.WriteLine("No items to delete."); Pause(); return;
        }

        int index = GetIntInput("Select item number to delete: ") - 1;

        if (index >= 0 && index < project.GetItemCount())
        {
            project.RemoveItem(index);
            Console.WriteLine("Item deleted.");
        }
        else
        {
            Console.WriteLine("Invalid item.");
        }
        Pause();
    }

    static void EditProjectInfo(Project project)
    {
        Console.Clear();
        Console.Write("New Project Name: ");  string name = Console.ReadLine();
        Console.Write("New Description: ");   string desc = Console.ReadLine();
        Console.Write("New Category: ");      string cat  = Console.ReadLine();
        Console.Write("New Status: ");        string stat = Console.ReadLine();
        Console.Write("New Priority: ");      string pri  = Console.ReadLine();
        double budget = GetDoubleInput("New Budget Goal (0 for none): ");

        project.Update(name, desc, cat, stat, pri, budget);
        Console.WriteLine("Project updated."); Pause();
    }

    static void FilterByCategory(ProjectManager manager)
    {
        Console.Clear();
        Console.Write("Enter category to filter by: ");
        string category = Console.ReadLine();
        manager.DisplayByCategory(category);
        Pause();
    }

    static void DeleteProject(ProjectManager manager)
    {
        Console.Clear();
        manager.DisplayAll();

        int index = GetIntInput("Select project number to delete: ") - 1;

        if (manager.SelectProject(index) != null)
        {
            manager.RemoveProject(index);
            Console.WriteLine("Project deleted.");
        }
        else
        {
            Console.WriteLine("Invalid project.");
        }
        Pause();
    }

    // --- Input helpers ---

    static int GetIntInput(string prompt)
    {
        int number;
        do
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out number)) return number;
            Console.WriteLine("Please enter a valid number.");
        } while (true);
    }

    static double GetDoubleInput(string prompt)
    {
        double number;
        do
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out number)) return number;
            Console.WriteLine("Please enter a valid amount.");
        } while (true);
    }

    static DateTime GetDateInput(string prompt)
    {
        DateTime date;
        do
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out date)) return date;
            Console.WriteLine("Please enter a valid date, e.g. 06/15/2026.");
        } while (true);
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
    }
}