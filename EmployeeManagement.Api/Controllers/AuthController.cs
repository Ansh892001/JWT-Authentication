using EmployeeManagement.Api.Models.Requests;
using EmployeeManagement.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<RegisterRequest> _registerValidator;

    public AuthController(IAuthService authService, IValidator<RegisterRequest> registerValidator)
    {
        _authService = authService;
        _registerValidator = registerValidator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
    RefreshRequest request)
    {
        var response = await _authService.RefreshAsync(request);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
    RegisterRequest request)
    {
        var validationResult =
            await _registerValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var response =
            await _authService.RegisterAsync(request);

        return Created(string.Empty, response);
    }
}