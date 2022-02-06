using MediatR;

namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class GetMovieListQuery : IRequest<List<MovieListVm>>
{
}
