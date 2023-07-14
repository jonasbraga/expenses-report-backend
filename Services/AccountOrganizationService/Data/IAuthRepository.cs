namespace AccountOrganizationService.Data
{
    public interface IAuthRepository
    {
        // We don't need the register method because the users will be created by the admin
        // And the admin will be created by the seed data

        //Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}