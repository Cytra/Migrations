using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController() : ControllerBase
{
    [Authorize(Policy = "ReadAccess")]
    [HttpGet("secure-data")]
    public IActionResult GetSecureData()
    {
        return Ok("You have read access!");
    }
}
