using System.IO;

namespace ShipItApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EnsureDataFileExists();
            Menu.DisplayLoggedOutMenu();
        }

        private static void EnsureDataFileExists()
        {
            if (!File.Exists("users.csv"))
            {
                File.WriteAllText("users.csv", string.Empty);
            }
        }
    }
}

