namespace AccountOrganizationService.Dtos.Department
{
    public class UpdateDepartmentRequestDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Acronym { get; set; }
        public required string Description { get; set; }
        public required List<string> ManagersId { get; set; }
    }
}