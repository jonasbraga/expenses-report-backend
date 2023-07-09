using AccountOrganizationService.Dtos.Project;
using Microsoft.AspNetCore.Mvc;

namespace AccountOrganizationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // TODO: Use Role enum instead of string
    [Authorize(Roles = "Accountant, Manager, FieldStaff")]
    public class ProjectController : ControllerBase
    {

        IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;

        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<GetProjectDto>> Get(int id)
        {
            var result = await _projectService.GetProject(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<GetProjectDto>> GetAll()
        {
            var result = await _projectService.GetAllProjects();
            if (result.Success == false)
            {
                // Where's internal server error?
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<GetProjectDto>> CreateProject([FromBody] CreateProjectRequestDto request)
        {
            var result = await _projectService.CreateProject(request);
            if (result.Success == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<GetProjectDto>> UpdateProject([FromBody] UpdateProjectRequestDto request)
        {
            var result = await _projectService.UpdateProject(request);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<GetProjectDto>> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProject(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}