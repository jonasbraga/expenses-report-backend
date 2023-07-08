using AccountOrganizationService.Dtos.Department;

namespace AccountOrganizationService.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentService(IMapper mapper, AppDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetDepartmentDto>>> CreateDepartment(CreateDepartmentRequestDto newDepartment)
        {
            var serviceResponse = new ServiceResponse<List<GetDepartmentDto>>();

            try
            {
                newDepartment.ManagersId.Select(id => _context.Users.First(u => u.Id == id)).ToList();
            }
            catch (System.Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                serviceResponse.Message = "Invalid Manager Id";
                return serviceResponse;
            }

            try
            {
                var department = new Department
                {
                    Name = newDepartment.Name,
                    Acronym = newDepartment.Acronym,
                    Description = newDepartment.Description,
                    Managers = newDepartment.ManagersId.Select(id => _context.Users.First(u => u.Id == id)).ToList()
                };
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                var dbDepartments = await _context.Departments
                        .Include(d => d.Managers)
                        .ToListAsync();
                var departmentResponse = _mapper.Map<List<GetDepartmentDto>>(dbDepartments);
                serviceResponse.Data = departmentResponse;
            }
            catch (System.Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Unexpected error occurred";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDepartmentDto>>> DeleteDepartment(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetDepartmentDto>>();

            try
            {
                var department = await _context.Departments.FirstAsync(d => d.Id == id);
                if (department is null)
                    throw new Exception($"Department with Id '{id}' not found.");

                _context.Departments.Remove(department);

                await _context.SaveChangesAsync();

                var dbDepartments = await _context.Departments
                    .Include(d => d.Managers)
                    .ToListAsync();
                var departmentResponse = _mapper.Map<List<GetDepartmentDto>>(dbDepartments);
                serviceResponse.Data = departmentResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDepartmentDto>>> GetAllDepartments()
        {
            var serviceResponse = new ServiceResponse<List<GetDepartmentDto>>();
            try
            {
                var dbDepartments = await _context.Departments
                        .Include(d => d.Managers)
                        .ToListAsync();
                var departmentResponse = _mapper.Map<List<GetDepartmentDto>>(dbDepartments);
                serviceResponse.Data = departmentResponse;
            }
            catch (System.Exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Unexpected error occurred";
                throw;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<GetDepartmentDto>> GetDepartment(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetDepartmentDto>> UpdateDepartment(UpdateDepartmentRequestDto updatedDepartment)
        {
            var serviceResponse = new ServiceResponse<GetDepartmentDto>();

            try
            {
                var department =
                    await _context.Departments
                        .Include(d => d.Managers)
                        .FirstOrDefaultAsync(d => d.Id == updatedDepartment.Id);
                if (department is null)
                    throw new Exception($"Department with Id '{updatedDepartment.Id}' not found.");

                // TODO: validate managers id before updating

                department.Name = updatedDepartment.Name;
                department.Acronym = updatedDepartment.Acronym;
                department.Description = updatedDepartment.Description;
                department.Managers = updatedDepartment.ManagersId.Select(id => _context.Users.First(u => u.Id == id)).ToList();

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetDepartmentDto>(department);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

