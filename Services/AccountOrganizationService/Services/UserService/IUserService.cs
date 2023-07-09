using AccountOrganizationService.Dtos.User;

namespace AccountOrganizationService.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> GetUser(string id);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<GetUserDto>>> CreateUser(CreateUserRequestDto newUser);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserRequestDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(string id);
    }
}