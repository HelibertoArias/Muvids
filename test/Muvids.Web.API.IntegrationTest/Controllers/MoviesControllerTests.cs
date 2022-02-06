


using Muvids.Application.Features.Movies.Queries.GetMoviesList;
using Muvids.Web.API.IntegrationTest.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Muvids.Web.API.IntegrationTest.Controllers;
public class MoviesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public MoviesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        this._factory = factory;
    }

    [Fact]
    public async Task GetAllMovies_Should_Return_One_Record()
    {
        var client = _factory.GetAnonymousClient();

        var response = await client.GetAsync("/api/movies/all");

         

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<MovieListVm>>(responseString);

        Assert.IsType<List<MovieListVm>>(result);
        Assert.NotEmpty(result);
        Assert.Single(result);

        response.EnsureSuccessStatusCode();
    }
}

