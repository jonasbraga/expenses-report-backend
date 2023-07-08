using AccountOrganizationService.Dtos.Department;
using Microsoft.AspNetCore.Mvc;

namespace AccountOrganizationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly IDepartmentService _departmentService;

    public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
    {
        _departmentService = departmentService;
        _logger = logger;
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<List<Department>>> Get(int id)
    {
        var result = await _departmentService.GetDepartment(id);
        if (result.Data is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<List<Department>>> GetAll()
    {
        return Ok(await _departmentService.GetAllDepartments());
    }

    // Create post method
    [HttpPost("Create")]
    public async Task<ActionResult<ServiceResponse<List<GetDepartmentDto>>>> CreateDepartment([FromBody] CreateDepartmentRequestDto request)
    {

        // TODO add validation, search for middleware or library to make this easier
        // Iterate Manager ID list to check if they exist

        var result = await _departmentService.CreateDepartment(request);
        if (result.Success == false)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetDepartmentDto>>>> DeleteDepartment(int id)
    {
        var result = await _departmentService.DeleteDepartment(id);
        if (result.Data is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<ServiceResponse<List<GetDepartmentDto>>>> UpdateDepartment(UpdateDepartmentRequestDto updatedDepartment)
    {
        var result = await _departmentService.UpdateDepartment(updatedDepartment);
        if (result.Data is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}

