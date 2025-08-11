using System;

namespace ShipItApp
{
    public static class Menu
    {
        public static void DisplayLoggedOutMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("              MAIN MENU                ");
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [1] Create User");
            Console.WriteLine(" [2] Login");
            Console.WriteLine(" [3] About");
            Console.WriteLine();
            Console.WriteLine(" [0] Exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write(" Select a Menu Option: ");
            Console.ResetColor();
        }

        public static void DisplayLoggedInMenu(User currentUser)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========================================");
            Console.WriteLine($"      Welcome {currentUser.Name}      ");
            Console.WriteLine("========================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(" [1] About");
            Console.WriteLine(" [2] Show Profile");
            Console.WriteLine(" [3] Users");
            Console.WriteLine(" [4] Logout");
            Console.WriteLine(" [5] Search Users");
            Console.WriteLine(" [0] Exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.Write(" Select a Menu Option: ");
            Console.ResetColor();
        }
    }
}

public static void SearchUsers()
{
    Console.Clear();
    Console.WriteLine("🔎 Search Users");
    Console.Write("Enter search text: ");
    var q = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(q))
    {
        Console.WriteLine("\nEmpty query. Press ENTER to return.");
        Console.ReadLine();
        return;
    }

    var path = "users.csv";
    if (!File.Exists(path))
    {
        Console.WriteLine($"\nFile not found: {path}");
        Console.ReadLine();
        return;
    }

    var results = File.ReadAllLines(path)
                      .Where(line => !string.IsNullOrWhiteSpace(line))
                      .Where(line => line.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                      .ToList();

    Console.WriteLine();
    if (results.Count == 0)
    {
        Console.WriteLine("No results.");
    }
    else
    {
        foreach (var line in results) Console.WriteLine(line);
    }

    Console.WriteLine("\nPress ENTER to return to the menu.");
    Console.ReadLine();
}
