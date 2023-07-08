using AccountOrganizationService.Dtos.Department;
using AccountOrganizationService.Dtos.Project;
using AccountOrganizationService.Dtos.User;

namespace AccountOrganizationService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Department
            CreateMap<Department, GetDepartmentDto>()
                .ForMember(dest => dest.ManagersId, opt => opt.MapFrom(src => src.Managers.Select(m => m.Id)));
            CreateMap<CreateDepartmentRequestDto, Department>();
            // User
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.SupervisorId, opt => opt.MapFrom(src => src.Supervisor.Id))
                .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));

            // Project
            CreateMap<Project, GetProjectDto>();
            CreateMap<CreateProjectRequestDto, Project>();
            CreateMap<UpdateProjectRequestDto, Project>();
        }
    }
}