using System;
using System.Text.RegularExpressions;

public static class Validation
{
    private static readonly Regex Letters = new("[A-Za-z]");
    private static readonly Regex Digits  = new(@"\d");

    public static bool TryValidatePassword(string? input, out string error)
    {
        input = (input ?? string.Empty).Trim();

        if (input.Length < 8)
        {
            error = "Password must be at least 8 characters.";
            return false;
        }
        if (!Letters.IsMatch(input))
        {
            error = "Password must include at least one letter (A–Z).";
            return false;
        }
        if (!Digits.IsMatch(input))
        {
            error = "Password must include at least one digit (0–9).";
            return false;
        }
        error = string.Empty;
        return true;
    }

    public static string ReadTrimmed() => (Console.ReadLine() ?? string.Empty).Trim();
}

