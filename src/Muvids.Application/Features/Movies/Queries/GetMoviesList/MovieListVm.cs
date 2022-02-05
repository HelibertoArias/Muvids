namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class MovieListVm
{
    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string Rating { get; set; } = null!;
}
