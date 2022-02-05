using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Muvids.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(ILogger<MoviesController> logger)
    {
        this._logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllMovies()
    {
        _logger.LogInformation("jajaja");
        return Ok(new List<string>() { "a", "b" });
    }
}
