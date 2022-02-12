using AutoMapper;
using MediatR;
using Muvids.Application.Contracts.Persistence.Common;
using Muvids.Application.Exceptions;
using Muvids.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Muvids.Application.Features.Movies.Commands.DeleteMovie;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<Movie> _movieRepository;

    public DeleteMovieCommandHandler(IMapper mapper,
                                    IAsyncRepository<Movie> movieRepository)
    {
        this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        this._movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }

    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        _ = request ?? throw new BadRequestException(nameof(request));

        var movieToDelete = await _movieRepository.GetByIdAsync(request.Id);

        if (movieToDelete == null)
        {
            throw new NotFoundException(nameof(Movie), request.Id);
        } 

        await _movieRepository.DeleteAsync(movieToDelete);
        return Unit.Value;
        
    }
}
