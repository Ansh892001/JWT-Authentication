using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}