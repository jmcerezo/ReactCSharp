namespace RegistrationApp.Server.Dto
{
    public class UpdateUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public long? PhoneNumber { get; set; }
    }
}
