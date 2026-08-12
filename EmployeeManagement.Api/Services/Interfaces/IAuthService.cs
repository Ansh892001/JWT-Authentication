using EmployeeManagement.Api.Models.Requests;
using EmployeeManagement.Api.Models.Responses;

namespace EmployeeManagement.Api.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshAsync(RefreshRequest request);
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}