using Muvids.Application.Features.Movies.Commands.CreateMovie;
using Muvids.Application.Features.Movies.Queries.GetMoviesList;
using Muvids.Web.API.IntegrationTest.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Muvids.Web.API.IntegrationTest.Controllers;
public class MoviesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    private readonly HttpClient _client;


    public MoviesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        this._factory = factory;

        _client = factory.WithWebHostBuilder(builder =>
        {

        }).CreateClient();
    }

    [Fact]
    public async Task GetAllMovies_Should_Return_One_Record()
    {
        var client = _factory.GeAuthenticatedClient();

        var response = await client.GetAsync("/api/movies/all");

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<MovieListVm>>(responseString);

        Assert.IsType<List<MovieListVm>>(result);
        Assert.NotEmpty(result);
        Assert.Single(result);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateMovie_Should_Return_Movie_Added()
    {
        var client = _factory.GeAuthenticatedClient();

        var newMoview = new CreateMovieCommand()
        {
            Description = "It is about ...",
            IsPublic = true,
            Language = "ES-en",
            ReleaseYear = 2000,
            Title = "Butterfly Effect"
        };

        var json = JsonConvert.SerializeObject(newMoview);

        var response = await client.PostAsync("/api/movies/createmovie", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CreateMovieCommandResponse>(responseString);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEqual(Guid.Empty, result?.Movie.Id);
    }
}

