using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Features.Movies.Commands;
using Muvids.Application.Features.Movies.Queries.GetMoviesList;

namespace Muvids.Web.API.Controllers;
[Authorize]
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

    [HttpGet("all", Name = "GetAllMovies")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MovieListVm>))]
    public async Task<IActionResult> GetAllMovies()
    {
        var dtos = await _mediator.Send(new GetMovieListQuery());
        return Ok(dtos);
    }

    
    [HttpPost("createmovie", Name = "Create")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateMovieDto))]
    public async Task<IActionResult> Create([FromBody] CreateMovieCommand data)
    {
        var dtos = await _mediator.Send(data);
        return Ok(dtos);
    }
}
