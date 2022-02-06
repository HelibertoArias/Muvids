using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Domain.Entities;

namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, List<MovieListVm>>
{
    private readonly IMapper _mapper;

    private readonly IAsyncRepository<Movie> _movieRepository;

    public GetMovieListQueryHandler(IMapper mapper,
                                    IAsyncRepository<Movie> movieRepository)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<List<MovieListVm>> Handle(GetMovieListQuery request,
                                                        CancellationToken cancellationToken)
    {
        var allEvents = (await _movieRepository.ListAllAsync()).OrderBy(x => x.Title);
        return _mapper.Map<List<MovieListVm>>(allEvents);
    }
}
