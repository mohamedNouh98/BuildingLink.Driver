namespace BuildingLink.Core.Drivers.Commands
{
    public class UpdateDriverCommand
    {
        public required string Id { get; init; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}