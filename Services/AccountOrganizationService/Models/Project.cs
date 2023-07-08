namespace AccountOrganizationService.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required int DepartmentId { get; set; }
        public required Department Department { get; set; }
    }
}