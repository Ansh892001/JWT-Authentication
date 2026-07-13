using EmployeeManagement.Api.Models.Entities;
using EmployeeManagement.Api.Repositories.Interfaces;

namespace EmployeeManagement.Api.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users =
    [
        new User
        {
            Id = 1,
            Email = "ansh@gmail.com",
            Password = "password123",
            Role = "Admin"
        },
        new User
        {
            Id = 2,
            Email = "john@gmail.com",
            Password = "password123",
            Role = "Employee"
        }
    ];

    public Task<User?> GetByEmailAsync(string email)
    {
        var user = Users.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(user);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        var user = Users.FirstOrDefault(u => u.Id == id);

        return Task.FromResult(user);
    }
}