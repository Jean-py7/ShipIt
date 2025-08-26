namespace ShipItApp
{
    public class User
    {
        public string Name  { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // was password
        public string City  { get; set; }
        public string State { get; set; }
    }
}
