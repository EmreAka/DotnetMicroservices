using Microsoft.AspNetCore.Mvc;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PlatformsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetPlatforms()
    {
        return Ok("Platforms from PlatformService");
    }
}
