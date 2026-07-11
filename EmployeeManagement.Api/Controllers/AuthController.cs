using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        if (request.Email != "ansh@gmail.com" ||
            request.Password != "password123")
        {
            return Unauthorized();
        }

        var user = new User
        {
            Id = 1,
            Email = request.Email,
            Role = "Admin"
        };

        var token = _tokenService.GenerateToken(user);

        return Ok(new LoginResponse
        {
            AccessToken = token
        });
    }
}