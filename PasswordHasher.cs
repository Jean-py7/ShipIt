using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    public static string CreateHash(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[16];
        rng.GetBytes(salt);

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Combine(salt, Encoding.UTF8.GetBytes(password)));

        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public static bool Verify(string password, string stored)
    {
        if (string.IsNullOrWhiteSpace(stored)) return false;
        var parts = stored.Split(':');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var expected = Convert.FromBase64String(parts[1]);

        using var sha = SHA256.Create();
        var actual = sha.ComputeHash(Combine(salt, Encoding.UTF8.GetBytes(password)));

        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }

    private static byte[] Combine(byte[] a, byte[] b)
    {
        var c = new byte[a.Length + b.Length];
        Buffer.BlockCopy(a, 0, c, 0, a.Length);
        Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
        return c;
    }
}
