namespace AccountOrganizationService.Models
{
    public class User
    {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Role Role { get; set; }
        public required User Supervisor { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string Country { get; set; }
        public required List<Department> Departments { get; set; }
    }
}