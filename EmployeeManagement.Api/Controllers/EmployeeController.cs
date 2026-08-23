using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [Authorize(Roles = "Employee")]
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(new[]
        {
            "Ansh",
            "Alice",
            "Bob"
        });
    }
}