namespace AccountOrganizationService.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var id = Guid.NewGuid().ToString();
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = id,
                FirstName = "Admin",
                LastName = "Braga",
                SupervisorId = id,
                Supervisor = null,
                Role = Role.Accountant,
                Email = "admin@expense.report",
                Password = AuthRepository.CreatePasswordHash("123123123"),
                Address = "123 Admin St",
                City = "Admin City",
                State = "Admin State",
                Zip = "12345",
                Country = "Admin Country"
            }
            );
        }
    }
}