namespace RegistrationApp.Server.Dto
{
    public class UserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public Int64 PhoneNumber { get; set; }
    }
}
