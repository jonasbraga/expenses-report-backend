using AccountOrganizationService.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace AccountOrganizationService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Accountant, Manager")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(string id)
    {
        var result = await _userService.GetUser(id);
        if (result.Data is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();
        if (result.Success == false)
        {
            // Where's internal server error?
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> CreateUser([FromBody] CreateUserRequestDto request)
    {
        var result = await _userService.CreateUser(request);
        // TODO: Improve error handling
        // Maybe creating a custom exception class so we can define the status code properly
        // How can we improve the default validation error? It validates the request body against the model and return a 400
        // with very little information about what went wrong
        if (result.Data is null)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser([FromBody] UpdateUserRequestDto request)
    {
        var result = await _userService.UpdateUser(request);
        if (result.Data is null)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    // TODO: consider permissions and authorization, only managers and accountant should be able to delete users
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(string id)
    {
        var result = await _userService.DeleteUser(id);
        if (result.Data is null)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}