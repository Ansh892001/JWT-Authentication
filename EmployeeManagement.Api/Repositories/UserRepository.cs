using EmployeeManagement.Api.Contexts;
using EmployeeManagement.Api.Models.Entities;
using EmployeeManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
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

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
        .FirstOrDefaultAsync(x => x.Id == id);

    }
}