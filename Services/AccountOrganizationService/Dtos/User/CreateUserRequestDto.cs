namespace AccountOrganizationService.Dtos.User
{
    public class CreateUserRequestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Role Role { get; set; }
        public required string SupervisorId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string Country { get; set; }
        // Only manager can be assigned to a department
        public List<int>? DepartmentsId { get; set; }
    }
}