using AccountOrganizationService.Dtos.Department;

namespace AccountOrganizationService.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<List<GetDepartmentDto>>> GetAllDepartments();
        Task<ServiceResponse<GetDepartmentDto>> GetDepartment(int id);
        Task<ServiceResponse<List<GetDepartmentDto>>> CreateDepartment(CreateDepartmentRequestDto newDepartment);
        Task<ServiceResponse<GetDepartmentDto>> UpdateDepartment(UpdateDepartmentRequestDto updatedDepartment);
        Task<ServiceResponse<List<GetDepartmentDto>>> DeleteDepartment(int id);
    }
}
