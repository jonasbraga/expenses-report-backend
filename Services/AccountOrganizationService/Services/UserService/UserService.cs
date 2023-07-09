using AccountOrganizationService.Dtos.User;

namespace AccountOrganizationService.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<GetUserDto>> GetUser(string id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var User = await _context.Users
                    .Include(u => u.Departments)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (User is null)
                    throw new Exception($"User with id '{id}' not found");

                var UserResponse = _mapper.Map<GetUserDto>(User);
                serviceResponse.Data = UserResponse;
            }
            catch (System.Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var User = await _context.Users
                    .Include(u => u.Departments)
                    .ToListAsync();

                var UserResponse = _mapper.Map<List<GetUserDto>>(User);
                serviceResponse.Data = UserResponse;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> CreateUser(CreateUserRequestDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                // Where to put this validation?
                // Is there a better way to do this?
                if (newUser.Role != Role.Manager && newUser.DepartmentsId is not null)
                {
                    throw new Exception($"User with role '{newUser.Role}' cannot have departments");
                }

                var supervisor = _context.Users.FirstOrDefault(u => u.Id == newUser.SupervisorId);

                if (supervisor is null)
                {
                    throw new Exception($"Supervisor with id '{newUser.SupervisorId}' not found");
                }

                // TODO: Managers can only create field staff users
                if (supervisor.Role != Role.Manager)
                {
                    throw new Exception($"Supervisor with id '{newUser.SupervisorId}' is not a manager");
                }

                if (newUser.DepartmentsId is not null)
                {
                    // Checking if given departments exist
                    newUser.DepartmentsId?.Select(id =>
                    {
                        var deparment = _context.Departments.FirstOrDefault(d => d.Id == id);
                        if (deparment is null)
                            throw new Exception($"Department with id '{id}' not found");
                        return id;
                    });
                }

                var user = _mapper.Map<User>(newUser);
                user.Departments = newUser.DepartmentsId?.Select(id => _context.Departments.First(d => d.Id == id)).ToList();
                user.Supervisor = supervisor;
                user.Password = AuthRepository.CreatePasswordHash(newUser.Password);

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                // Question: Send email to user with password?
                var userResponse = _mapper.Map<List<GetUserDto>>(await _context.Users
                    .Include(u => u.Departments)
                    .ToListAsync());
                serviceResponse.Data = userResponse;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserRequestDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                // Question: Where to put this validation?
                // Question: Is there a better way to do this?
                var user = await _context.Users
                    .Include(u => u.Departments)
                    .FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

                if (user is null)
                    throw new Exception($"User with id '{updatedUser.Id}' not found");

                if (updatedUser.Role != Role.Manager && updatedUser.DepartmentsId is not null)
                {
                    throw new Exception($"User with role '{updatedUser.Role}' cannot have departments");
                }

                var supervisor = _context.Users.FirstOrDefault(u => u.Id == updatedUser.SupervisorId);

                if (supervisor is null)
                {
                    throw new Exception($"Supervisor with id '{updatedUser.SupervisorId}' not found");
                }

                if (supervisor.Role != Role.Manager)
                {
                    throw new Exception($"Supervisor with id '{updatedUser.SupervisorId}' is not a manager");
                }

                if (updatedUser.DepartmentsId is not null)
                {
                    // Checking if given departments exist
                    updatedUser.DepartmentsId?.Select(id =>
                    {
                        var deparment = _context.Departments.FirstOrDefault(d => d.Id == id);
                        if (deparment is null)
                            throw new Exception($"Department with id '{id}' not found");
                        return id;
                    });
                }

                user.Departments = updatedUser.DepartmentsId?.Select(id => _context.Departments.First(d => d.Id == id)).ToList();
                user.Supervisor = supervisor;
                user.Password = AuthRepository.CreatePasswordHash(updatedUser.Password);
                await _context.SaveChangesAsync();

                var userResponse = _mapper.Map<GetUserDto>(user);
                serviceResponse.Data = userResponse;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(string id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = await _context.Users
                    .Include(u => u.Departments)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user is null)
                    throw new Exception($"User with id '{id}' not found");

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                var userResponse = _mapper.Map<List<GetUserDto>>(await _context.Users
                    .Include(u => u.Departments)
                    .ToListAsync());
                serviceResponse.Data = userResponse;
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}