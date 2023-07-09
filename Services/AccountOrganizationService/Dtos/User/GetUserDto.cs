using AccountOrganizationService.Dtos.Department;

namespace AccountOrganizationService.Dtos.User
{
    public class GetUserDto
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Role Role { get; set; }
        public required string SupervisorId { get; set; }
        //public required GetUserDto Supervisor { get; set; }
        public required string Email { get; set; }
        //public required string Password { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string Country { get; set; }
        public required List<int> DepartmentsId { get; set; }
        //public required List<GetDepartmentDto> Departments { get; set; }
    }
}