using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(new[]
        {
            "John",
            "Alice",
            "Bob"
        });
    }
}