using System;

public class GratitudeActivity : Activity
{
    public GratitudeActivity()
    {
        _name = "Gratitude";
        _description = "This activity helps you focus on gratitude and positivity.";
    }

    public void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine();
        Console.WriteLine("Think about 3 things you are grateful for.");

        for (int i = 1; i <= 3; i++)
        {
            Console.Write($"Thing {i}: ");
            Console.ReadLine();
        }

        Console.WriteLine();
        Console.WriteLine("Take a moment to appreciate these things.");
        ShowSpinner(5);

        DisplayEndingMessage();
    }
}