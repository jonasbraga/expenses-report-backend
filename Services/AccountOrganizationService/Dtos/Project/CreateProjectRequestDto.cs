namespace AccountOrganizationService.Services.ProjectService
{
    public class CreateProjectRequestDto
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required int DepartmentId { get; set; }
    }
}