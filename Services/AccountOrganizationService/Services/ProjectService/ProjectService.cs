using AccountOrganizationService.Dtos.Project;

namespace AccountOrganizationService.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ProjectService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetProjectDto>> GetProject(int id)
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            try
            {
                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (project is null)
                    throw new Exception($"Project with id '{id}' not found");

                var projectResponse = _mapper.Map<GetProjectDto>(project);
                serviceResponse.Data = projectResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProjectDto>>> GetAllProjects()
        {
            var serviceResponse = new ServiceResponse<List<GetProjectDto>>();
            try
            {
                var project = await _context.Projects.ToListAsync();

                var projectResponse = _mapper.Map<List<GetProjectDto>>(project);
                serviceResponse.Data = projectResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProjectDto>>> CreateProject(CreateProjectRequestDto newProject)
        {
            var serviceResponse = new ServiceResponse<List<GetProjectDto>>();
            try
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Id == newProject.DepartmentId);

                if (department is null)
                    throw new Exception($"Department with Id '{newProject.DepartmentId}' not found.");

                var project = _mapper.Map<Project>(newProject);
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                var projectsList = await _context.Projects
                    .ToListAsync();

                var projectResponse = _mapper.Map<List<GetProjectDto>>(projectsList);
                serviceResponse.Data = projectResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProjectDto>> UpdateProject(UpdateProjectRequestDto updatedProject)
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            try
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Id == updatedProject.DepartmentId);
                if (department is null)
                    throw new Exception($"Department with Id '{updatedProject.DepartmentId}' not found.");

                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == updatedProject.Id);

                if (project is null)
                    throw new Exception($"Project with Id '{updatedProject.Id}' not found.");

                project.Code = updatedProject.Code;
                project.Name = updatedProject.Name;
                project.Description = updatedProject.Description;
                project.DepartmentId = updatedProject.DepartmentId;
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetProjectDto>(project);
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProjectDto>>> DeleteProject(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetProjectDto>>();
            try
            {
                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (project is null)
                    throw new Exception($"Project with Id '{id}' not found.");

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<List<GetProjectDto>>(await _context.Projects.ToListAsync());
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