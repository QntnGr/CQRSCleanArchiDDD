using Application.Common.Interfaces.Services;
using Application.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CQRSCleanArchiDDD.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserManagementController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtTokenService _jwtTokenService;

    public UserManagementController(IUserService userService, JwtTokenService jwtTokenService)
    {
        _userService = userService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto userLogin)
    {
        var user = await _userService.AuthenticateUser(userLogin.Username, userLogin.Password);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _jwtTokenService.GenerateToken(user.Id.ToString());
        return Ok(new { Token = token });
    }
}
