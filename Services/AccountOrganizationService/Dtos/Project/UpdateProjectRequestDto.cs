namespace AccountOrganizationService.Dtos.Project
{
    public class UpdateProjectRequestDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required int DepartmentId { get; set; }
    }
}