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
    public UserSummaryResponseModel GetUserFunction()
    {
        return new UserSummaryResponseModel();
    }

    [HttpPost("register")]
    public IActionResult RegisterUser(RegisterRequestModel registerRequest)
    {
        if (_userService.RegisterUser(registerRequest.username, registerRequest.password, 
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
    public IActionResult LoginUser(LoginRequestModel loginRequest)
    {
        if (_userService.LoginUser(loginRequest.username, loginRequest.password))
        {
            return Ok("Login Successful");
        }
        else
        {
            return Unauthorized("Login Failed - Invalid credentials");
        }
    }

    [HttpGet("get-user/{username}")]
    public IActionResult GetUser(string username)
    {
        var user = _userService.GetUserByName(username);
        if (user != null)
        {
            return Ok(new { 
                Username = user.Username, 
                Age = user.Age, 
                PhoneNumber = user.PhoneNumber, 
                Email = user.Email,
                Balance = user.Balance 
            });
        }
        return NotFound("User not found");
    }

    [HttpPut("add-balance")]
    public IActionResult AddBalance([FromQuery] string username, [FromQuery] int amount)
    {
        if (_userService.AddBalance(username, amount))
        {
            return Ok($"Balance added successfully");
        }
        return NotFound("User not found");
    }
}