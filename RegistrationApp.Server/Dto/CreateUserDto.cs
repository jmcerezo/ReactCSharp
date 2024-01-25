namespace RegistrationApp.Server.Dto
{
    public class CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public required long PhoneNumber { get; set; }
    }
}
