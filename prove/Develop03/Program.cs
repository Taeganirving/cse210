using System;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);

        Scripture scripture = new Scripture(
            reference,
            "Trust in the Lord with all thine heart and lean not unto thine own understanding " +
            "in all thy ways acknowledge him and he shall direct thy paths"
        );

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press enter to continue or type 'quit' to finish: ");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine("Program ended.");
    }
}