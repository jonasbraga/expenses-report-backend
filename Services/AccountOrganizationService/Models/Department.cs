namespace AccountOrganizationService.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Acronym { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required List<User> Managers { get; set; }

        // Add createdAt, updatedAt and maybe deleteAt
    }
}


