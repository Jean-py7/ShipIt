public static List<User> LoadUsers() =>
    File.ReadAllLines(DataFilePath)
        .Select(line => line.Split(','))
        .Where(parts => parts.Length >= 5)
        .Select(parts => new User
        {
            Name     = parts[0],
            Email    = parts[1],
            Password = parts[2],
            City     = parts[3],
            State    = parts[4]
        })
        .ToList();

public static bool EmailTaken(string email) =>
    LoadUsers().Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

public static User FindByEmail(string email) =>
    LoadUsers().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

private static void CreateUser()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== Create New User ===");
    Console.ResetColor();

    // Name
    Console.Write("Enter your name: ");
    var name = Console.ReadLine().Trim();
    while (string.IsNullOrWhiteSpace(name))
    {
        Console.Write("Name cannot be empty. Enter your name: ");
        name = Console.ReadLine().Trim();
    }

    // Email
    Console.Write("Enter your email: ");
    var email = Console.ReadLine().Trim();
    while (!Validation.ValidateEmail(email) || EmailTaken(email))
    {
        if (EmailTaken(email))
        {
            Console.Write("This email is already registered. Enter a different email: ");
        }
        else
        {
            Console.Write("Invalid email format. Enter a valid email: ");
        }
        email = Console.ReadLine().Trim();
    }

    // Password
    Console.Write("Enter your password (min 6 chars, at least 1 letter and 1 digit): ");
    var password = Console.ReadLine();
    while (!Validation.ValidatePassword(password))
    {
        Console.Write("Password too short or lacks required characters. Enter a valid password: ");
        password = Console.ReadLine();
    }

    // City
    Console.Write("Enter your city: ");
    var city = Console.ReadLine().Trim();
    while (string.IsNullOrWhiteSpace(city))
    {
        Console.Write("City cannot be empty. Enter your city: ");
        city = Console.ReadLine().Trim();
    }

    // State
    Console.Write("Enter your state (2-letter code): ");
    var state = Console.ReadLine().Trim();
    while (!Validation.ValidateStateAbbreviation(state))
    {
        Console.Write("Invalid state. Enter a valid 2-letter state code: ");
        state = Console.ReadLine().Trim();
    }

    var user = new User
    {
        Name     = name,
        Email    = email,
        Password = password,
        City     = city,
        State    = state.ToUpper()
    };
    File.AppendAllLines(DataFilePath, new[] { $"{user.Name},{user.Email},{user.Password},{user.City},{user.State}" });

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("User created successfully.");
    Console.ResetColor();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

private static void Login()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== Login ===");
    Console.ResetColor();

    Console.Write("Enter your email: ");
    var emailInput = Console.ReadLine().Trim();
    var found = FindByEmail(emailInput);

    if (found != null)
    {
        Console.Write("Enter your password: ");
        var passwordInput = Console.ReadLine();
        if (found.Password == passwordInput)
        {
            _currentUser = found;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Login successful. Welcome, {_currentUser.Name}!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid password.");
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("User not found. Please create an account first.");
    }

    Console.ResetColor();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}
