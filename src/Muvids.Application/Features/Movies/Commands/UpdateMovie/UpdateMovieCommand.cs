using MediatR;

namespace Muvids.Application.Features.Movies.Commands.UpdateMovie;

public class UpdateMovieCommand : IRequest
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string Language { get; set; } = null!;

    public bool IsPublic { get; set; }
}
