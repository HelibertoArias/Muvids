using MediatR;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Features.Movies.Queries.GetMoviesList;

namespace Muvids.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMediator mediator, 
                            ILogger<MoviesController> logger)
    {
        this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMovies()
    {
        var dtos = await _mediator.Send(new GetMovieListQuery());
        return Ok(dtos);
    }
}
