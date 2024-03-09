namespace RegistrationApp.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;
        public long PhoneNumber { get; set; }
    }
}
