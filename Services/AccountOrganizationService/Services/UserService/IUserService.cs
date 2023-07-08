using AccountOrganizationService.Dtos.User;

namespace AccountOrganizationService.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAllUsers();
        Task<ServiceResponse<User>> GetUser(string id);
        Task<ServiceResponse<List<GetUserDto>>> CreateUser(User newUser);
        Task<ServiceResponse<User>> UpdateUser(string id);
        Task<ServiceResponse<User>> DeleteUser(string id);


    }
}