using MediatR;

namespace Muvids.Application.Features.Movies.Commands;

public class CreateMovieCommand : IRequest<CreateMovieCommandResponse>
{
    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string Rating { get; set; } = null!;

    public bool IsPublic { get; set; }
}
