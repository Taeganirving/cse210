using System;

class Program
{
    static void Main(string[] args)
    {
        ProjectManager manager = new ProjectManager();
        FileManager fileManager = new FileManager();

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

            if (choice == 1)
            {
                CreateProject(manager);
            }
            else if (choice == 2)
            {
                manager.DisplayAllProjects();
                Pause();
            }
            else if (choice == 3)
            {
                SelectProjectMenu(manager);
            }
            else if (choice == 4)
            {
                FilterByCategory(manager);
            }
            else if (choice == 5)
            {
                DeleteProject(manager);
            }
            else if (choice == 6)
            {
                fileManager.SaveProjects(manager.GetProjects(), "lifeBuildSave.txt");
                Console.WriteLine("Projects saved.");
                Pause();
            }
            else if (choice == 7)
            {
                fileManager.LoadProjects(manager, "lifeBuildSave.txt");
                Console.WriteLine("Projects loaded.");
                Pause();
            }
            else if (choice == 8)
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Invalid option.");
                Pause();
            }
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

        Console.Write("Budget Goal, enter 0 for no budget: ");
        double budgetGoal = GetDoubleInput("");

        Project project = new Project(
            name,
            description,
            category,
            status,
            priority,
            budgetGoal
        );

        manager.AddProject(project);

        Console.WriteLine();
        Console.WriteLine("Project created.");
        Pause();
    }

    static void SelectProjectMenu(ProjectManager manager)
    {
        Console.Clear();
        manager.DisplayAllProjects();

        Console.WriteLine();

        int index = GetIntInput("Select project number: ") - 1;
        Project selectedProject = manager.SelectProject(index);

        if (selectedProject == null)
        {
            Console.WriteLine("Invalid project.");
            Pause();
            return;
        }

        bool inProjectMenu = true;

        while (inProjectMenu)
        {
            Console.Clear();
            selectedProject.DisplayProject();

            Console.WriteLine();
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Add Milestone");
            Console.WriteLine("4. Add Note");
            Console.WriteLine("5. Mark Item Complete");
            Console.WriteLine("6. Mark Item Incomplete");
            Console.WriteLine("7. Edit Item");
            Console.WriteLine("8. Delete Item");
            Console.WriteLine("9. Edit Project Info");
            Console.WriteLine("10. Back");

            int choice = GetIntInput("Choose an option: ");

            if (choice == 1)
            {
                AddTask(selectedProject);
            }
            else if (choice == 2)
            {
                AddExpense(selectedProject);
            }
            else if (choice == 3)
            {
                AddMilestone(selectedProject);
            }
            else if (choice == 4)
            {
                AddNote(selectedProject);
            }
            else if (choice == 5)
            {
                MarkItemComplete(selectedProject);
            }
            else if (choice == 6)
            {
                MarkItemIncomplete(selectedProject);
            }
            else if (choice == 7)
            {
                EditItem(selectedProject);
            }
            else if (choice == 8)
            {
                DeleteItem(selectedProject);
            }
            else if (choice == 9)
            {
                EditProjectInfo(selectedProject);
            }
            else if (choice == 10)
            {
                inProjectMenu = false;
            }
            else
            {
                Console.WriteLine("Invalid option.");
                Pause();
            }
        }
    }

    static void AddTask(Project project)
    {
        Console.Clear();

        Console.Write("Task Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Priority: ");
        string priority = Console.ReadLine();

        DateTime dueDate = GetDateInput("Due Date: ");

        TaskItem task = new TaskItem(name, description, priority, dueDate);
        project.AddItem(task);

        Console.WriteLine("Task added.");
        Pause();
    }

    static void AddExpense(Project project)
    {
        Console.Clear();

        Console.Write("Expense Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Priority: ");
        string priority = Console.ReadLine();

        double amount = GetDoubleInput("Amount: ");

        Console.Write("Category: ");
        string category = Console.ReadLine();

        ExpenseItem expense = new ExpenseItem(
            name,
            description,
            priority,
            amount,
            category
        );

        project.AddItem(expense);

        Console.WriteLine("Expense added.");
        Pause();
    }

    static void AddMilestone(Project project)
    {
        Console.Clear();

        Console.Write("Milestone Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Priority: ");
        string priority = Console.ReadLine();

        DateTime targetDate = GetDateInput("Target Date: ");

        MilestoneItem milestone = new MilestoneItem(
            name,
            description,
            priority,
            targetDate
        );

        project.AddItem(milestone);

        Console.WriteLine("Milestone added.");
        Pause();
    }

    static void AddNote(Project project)
    {
        Console.Clear();

        Console.Write("Note Title: ");
        string name = Console.ReadLine();

        Console.Write("Note: ");
        string description = Console.ReadLine();

        Console.Write("Priority: ");
        string priority = Console.ReadLine();

        DateTime date = GetDateInput("Date: ");

        NoteItem note = new NoteItem(
            name,
            description,
            priority,
            date
        );

        project.AddItem(note);

        Console.WriteLine("Note added.");
        Pause();
    }

    static void MarkItemComplete(Project project)
    {
        Console.Clear();
        project.DisplayProject();

        if (project.GetItems().Count == 0)
        {
            Console.WriteLine("There are no items to mark complete.");
            Pause();
            return;
        }

        int index = GetIntInput("Select item number to mark complete: ") - 1;

        if (index >= 0 && index < project.GetItems().Count)
        {
            project.GetItems()[index].MarkComplete();
            Console.WriteLine("Item marked complete.");
        }
        else
        {
            Console.WriteLine("Invalid item.");
        }

        Pause();
    }

    static void MarkItemIncomplete(Project project)
    {
        Console.Clear();
        project.DisplayProject();

        if (project.GetItems().Count == 0)
        {
            Console.WriteLine("There are no items to mark incomplete.");
            Pause();
            return;
        }

        int index = GetIntInput("Select item number to mark incomplete: ") - 1;

        if (index >= 0 && index < project.GetItems().Count)
        {
            project.GetItems()[index].MarkIncomplete();
            Console.WriteLine("Item marked incomplete.");
        }
        else
        {
            Console.WriteLine("Invalid item.");
        }

        Pause();
    }

    static void EditItem(Project project)
    {
        Console.Clear();
        project.DisplayProject();

        if (project.GetItems().Count == 0)
        {
            Console.WriteLine("There are no items to edit.");
            Pause();
            return;
        }

        int index = GetIntInput("Select item number to edit: ") - 1;

        if (index < 0 || index >= project.GetItems().Count)
        {
            Console.WriteLine("Invalid item.");
            Pause();
            return;
        }

        ProjectItem item = project.GetItems()[index];

        Console.Write("New Name: ");
        item.SetName(Console.ReadLine());

        Console.Write("New Description: ");
        item.SetDescription(Console.ReadLine());

        Console.Write("New Priority: ");
        item.SetPriority(Console.ReadLine());

        if (item is TaskItem task)
        {
            task.SetDueDate(GetDateInput("New Due Date: "));
        }
        else if (item is MilestoneItem milestone)
        {
            milestone.SetTargetDate(GetDateInput("New Target Date: "));
        }
        else if (item is ExpenseItem expense)
        {
            expense.SetAmount(GetDoubleInput("New Amount: "));

            Console.Write("New Expense Category: ");
            expense.SetCategory(Console.ReadLine());
        }
        else if (item is NoteItem note)
        {
            note.SetDate(GetDateInput("New Note Date: "));
        }

        Console.WriteLine("Item updated.");
        Pause();
    }

    static void DeleteItem(Project project)
    {
        Console.Clear();
        project.DisplayProject();

        if (project.GetItems().Count == 0)
        {
            Console.WriteLine("There are no items to delete.");
            Pause();
            return;
        }

        int index = GetIntInput("Select item number to delete: ") - 1;

        if (index >= 0 && index < project.GetItems().Count)
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

        Console.Write("New Project Name: ");
        project.SetName(Console.ReadLine());

        Console.Write("New Description: ");
        project.SetDescription(Console.ReadLine());

        Console.Write("New Category: ");
        project.SetCategory(Console.ReadLine());

        Console.Write("New Status: ");
        project.SetStatus(Console.ReadLine());

        Console.Write("New Priority: ");
        project.SetPriority(Console.ReadLine());

        double budgetGoal = GetDoubleInput("New Budget Goal, enter 0 for no budget: ");
        project.SetBudgetGoal(budgetGoal);

        Console.WriteLine("Project updated.");
        Pause();
    }

    static void FilterByCategory(ProjectManager manager)
    {
        Console.Clear();

        Console.Write("Enter category to filter by: ");
        string category = Console.ReadLine();

        manager.DisplayProjectsByCategory(category);
        Pause();
    }

    static void DeleteProject(ProjectManager manager)
    {
        Console.Clear();

        manager.DisplayAllProjects();

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

    static int GetIntInput(string prompt)
    {
        int number;
        bool validInput = false;

        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            validInput = int.TryParse(input, out number);

            if (!validInput)
            {
                Console.WriteLine("Please enter a valid number.");
            }

        } while (!validInput);

        return number;
    }

    static double GetDoubleInput(string prompt)
    {
        double number;
        bool validInput = false;

        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            validInput = double.TryParse(input, out number);

            if (!validInput)
            {
                Console.WriteLine("Please enter a valid amount.");
            }

        } while (!validInput);

        return number;
    }

    static DateTime GetDateInput(string prompt)
    {
        DateTime date;
        bool validInput = false;

        do
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            validInput = DateTime.TryParse(input, out date);

            if (!validInput)
            {
                Console.WriteLine("Please enter a valid date, like 06/15/2026.");
            }

        } while (!validInput);

        return date;
    }

    static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
    }
}
