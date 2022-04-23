using AutoMapper;
using MediatR;
using Muvids.Application.Contracts;
using Muvids.Application.Contracts.Persistence;

namespace Muvids.Application.Features.Movies.Queries.GetMoviesList;

public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, List<MovieListVm>>
{
    private readonly IMapper _mapper;
    private readonly IMovieRepository _movieRepository;
    private readonly ILoggedInUserService _loggedInUserService;

    public GetMovieListQueryHandler(IMapper mapper,
                                    IMovieRepository movieRepository,
                                    ILoggedInUserService loggedInUserService)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        this._loggedInUserService = loggedInUserService;
    }

    public async Task<List<MovieListVm>> Handle(GetMovieListQuery request, CancellationToken cancellationToken)
    {

        var eventsFiltered = (await _movieRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize))
                                .ToList()
                                .Where(x => x.IsPublic || x.CreatedBy == _loggedInUserService.UserId)
                                .OrderBy(x => x.Title);


        return _mapper.Map<List<MovieListVm>>(eventsFiltered);
    }
}
