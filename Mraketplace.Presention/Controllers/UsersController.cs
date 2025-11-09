using Microsoft.AspNetCore.Mvc;
using Mraketplace.Presention.DTOs.RequestModels;
using Mraketplace.Presention.DTOs.ResponseModels;
using Marketplace.Application;

[ApiController]
[Route("user-apis")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("get-user-summary")]
    public async Task<string> GetUserFunction()
    {
        return await _userService.GetAllUsersAsync();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterRequestModel registerRequest)
    {
        if (await _userService.RegisterUserAsync(registerRequest.username, registerRequest.password, 
            registerRequest.age, registerRequest.phoneNumber, registerRequest.email))
        {
            return Ok("Registration Successful");
        }
        else
        {
            return BadRequest("Registration Failed - User might already exist");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequestModel loginRequest)
    {
        if (await _userService.LoginUserAsync(loginRequest.username, loginRequest.password))
        {
            return Ok("Login Successful");
        }
        else
        {
            return Unauthorized("Login Failed - Invalid credentials");
        }
    }

    [HttpGet("get-user/{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _userService.GetUserByNameAsync(username);
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var response = new UserSummaryResponseModel(user.Username, user.Age);
        return Ok(response);
    }

    [HttpPut("add-balance")]
    public async Task<IActionResult> AddBalance([FromQuery] string username, [FromQuery] int amount)
    {
        if (await _userService.AddBalanceAsync(username, amount))
        {
            return Ok($"Balance added successfully");
        }
        return NotFound("User not found");
    }
}