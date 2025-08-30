using System;
using System.IO;

namespace ShipItApp
{
    public static class Menu
    {
        // ===== Logged-out menu (entry point) =====
        public static void DisplayLoggedOutMenu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================");
                Console.WriteLine("            MAIN MENU           ");
                Console.WriteLine("================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine(" [1] Create User");
                Console.WriteLine(" [2] Login");
                Console.WriteLine(" [3] List Users");
                Console.WriteLine(" [4] About");
                Console.WriteLine(" [0] Exit");
                Console.WriteLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Select a Menu Option: ");
                Console.ResetColor();
                var choice = (Console.ReadLine() ?? string.Empty).Trim();

                switch (choice)
                {
                    case "1": CreateUser(); break;
                    case "2": Login(); break;
                    case "3": ListUsers(); break;
                    case "4": About(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid option. Please try 0–4.");
                        Pause();
                        break;
                }
            }
        }

        // ===== Optional: logged-in menu (light demo) =====
        public static void DisplayLoggedInMenu(User currentUser)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================");
                Console.WriteLine($"    Welcome, {currentUser?.Name ?? "User"}   ");
                Console.WriteLine("================================");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine(" [1] List Users");
                Console.WriteLine(" [0] Logout");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Select a Menu Option: ");
                Console.ResetColor();
                var choice = (Console.ReadLine() ?? string.Empty).Trim();

                switch (choice)
                {
                    case "1": ListUsers(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid option. Please try 0 or 1.");
                        Pause();
                        break;
                }
            }
        }

        // ===== Create User (validate + hash + save) =====
        private static void CreateUser()
        {
            Console.Write("Name: ");  var name  = Validation.ReadTrimmed();
            Console.Write("Email: "); var email = Validation.ReadTrimmed();
            Console.Write("City: ");  var city  = Validation.ReadTrimmed();
            Console.Write("State: "); var state = Validation.ReadTrimmed();

            Console.Write("Password: ");
            var pwd = Validation.ReadTrimmed();
            while (!Validation.TryValidatePassword(pwd, out var msg))
            {
                Console.WriteLine(msg);
                Console.Write("Password: ");
                pwd = Validation.ReadTrimmed();
            }

            // 🔒 hash it (salt:hash)
            var pwdHash = PasswordHasher.CreateHash(pwd);

            // Save as: Name,Email,PasswordHash,City,State
            var line = $"{name},{email},{pwdHash},{city},{state}";
            try
            {
                File.AppendAllText("users.csv", line + Environment.NewLine);
                Console.WriteLine("User saved.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not save user (is users.csv open?): {ex.Message}");
            }
            Pause();
        }

        // ===== Login (verify against stored hash) =====
        private static void Login()
        {
            Console.Write("Email: ");    var emailInput = Validation.ReadTrimmed();
            Console.Write("Password: "); var pwdInput   = Validation.ReadTrimmed();

            if (!File.Exists("users.csv"))
            {
                Console.WriteLine("No users have been created yet.");
                Pause(); return;
            }

            User? logged = null;
            try
            {
                foreach (var line in File.ReadLines("users.csv"))
                {
                    var cols = line.Split(',');
                    if (cols.Length < 5) continue;

                    var name  = cols[0];
                    var email = cols[1];
                    var hash  = cols[2]; // salt:hash

                    if (email.Equals(emailInput, StringComparison.OrdinalIgnoreCase) &&
                        PasswordHasher.Verify(pwdInput, hash))
                    {
                        logged = new User { Name = name, Email = email, City = cols[3], State = cols[4] };
                        break;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not read users: {ex.Message}");
                Pause(); return;
            }

            if (logged is not null)
            {
                Console.WriteLine("Login successful.");
                Pause();
                DisplayLoggedInMenu(logged);
            }
            else
            {
                Console.WriteLine("Invalid email or password.");
                Pause();
            }
        }

        // ===== List Users (hide password/hash) =====
        private static void ListUsers()
        {
            try
            {
                if (!File.Exists("users.csv"))
                {
                    Console.WriteLine("No users yet.");
                    Pause();
                    return;
                }

                Console.WriteLine("Name | Email | (password hidden)");
                foreach (var line in File.ReadLines("users.csv"))
                {
                    var cols = line.Split(',');
                    if (cols.Length < 5) continue;
                    var name  = cols[0];
                    var email = cols[1];
                    Console.WriteLine($"{name} | {email} | (password hidden)");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Could not read users: {ex.Message}");
            }
            Pause();
        }

        private static void About()
        {
            Console.WriteLine("ShipItApp – Final Milestone demo.");
            Console.WriteLine("- Passwords validated (>=8 chars, letter + digit)");
            Console.WriteLine("- Stored as salted hashes (salt:hash) in users.csv");
            Console.WriteLine("- User list hides passwords");
            Pause();
        }

        private static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Press Enter to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
