using AccountOrganizationService.Dtos.Project;

namespace AccountOrganizationService.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ServiceResponse<GetProjectDto>> GetProject(int id);
        Task<ServiceResponse<List<GetProjectDto>>> GetAllProjects();
        Task<ServiceResponse<List<GetProjectDto>>> CreateProject(CreateProjectRequestDto newProject);
        Task<ServiceResponse<GetProjectDto>> UpdateProject(UpdateProjectRequestDto updatedProject);
        Task<ServiceResponse<List<GetProjectDto>>> DeleteProject(int id);
    }
}