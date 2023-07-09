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
                .ForMember(dest => dest.DepartmentsId, opt => opt.MapFrom(src => src.Departments.Select(d => d.Id)));
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();
            // TODO: figure out how to map role to readable string
            //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => MapRole(src.Role)));

            // Project
            CreateMap<Project, GetProjectDto>();
            CreateMap<CreateProjectRequestDto, Project>();
            CreateMap<UpdateProjectRequestDto, Project>();
        }

        private string MapRole(Role role)
        {
            switch (role)
            {
                case Role.Accountant:
                    return "Accountant";
                case Role.FieldStaff:
                    return "Field Staff";
                case Role.Manager:
                    return "Manager";
                default:
                    return "Unknown";
            }
        }
    }
}
