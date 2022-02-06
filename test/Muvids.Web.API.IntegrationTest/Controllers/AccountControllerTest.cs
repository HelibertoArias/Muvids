using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Models.Authentication;
using Muvids.Web.API.IntegrationTest.Base.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Muvids.Web.API.IntegrationTest.Controllers;

public class AccountControllerTest : IClassFixture<IdentityCustomWebApplicationFactory<Program>>
{
    private readonly IdentityCustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public AccountControllerTest(IdentityCustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        this._factory = factory;
        this._output = output;
    }

    //[Fact]
    //public async Task Authenticate_Should_Authenticate_An_Existing_User()
    //{
    //    var client = _factory.GetAnonymousClient();

    //    var authenticationRequest = new AuthenticationRequest()
    //    {
    //        Email = "helibertoarias@gmail.com",
    //        Password = "P4ss"
    //    };


    //    var response = await client.PostAsJsonAsync("/api/account/authenticate",
    //                                JsonConvert.SerializeObject(authenticationRequest)
    //        );

    //    response.EnsureSuccessStatusCode();

    //    var responseString = await response.Content.ReadAsStringAsync();

    //    var result = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);

    //    Assert.IsType<AuthenticationResponse>(result);
    //    response.EnsureSuccessStatusCode();
    //}


    [Fact]
    public async Task Register_Should_Create_New_Username()
    {
        var client = _factory.GetAnonymousClient();


        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Heliberto",
            LastName = "Arias",
            UserName = "helibertoarias",
            Email = "helibertoarias@gmail.com",
            Password = "P@ssword1"
        };



        var json = JsonConvert.SerializeObject(registrationRequest);

        var response = await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseString);

        Assert.IsType<RegistrationResponse>(result);

    }

    [Fact]
    public async Task Register_Should_Throw_With_Existing_Email()
    {
        // Arrange
        var client = _factory.GetAnonymousClient();

        var registrationRequest = new RegistrationRequest()
        {
            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "janedoe@gmail.com",
            Password = "P@ssword1"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Act
        registrationRequest.UserName = "theJaneDoe";
        json = JsonConvert.SerializeObject(registrationRequest);

        var response = await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseString);

        Assert.Equal("Email janedoe@gmail.com already exists.", responseString);
    }
    
    [Fact]
    public async Task Register_Should_Throw_With_Existing_User()
    {
        // Arrange
        var client = _factory.GetAnonymousClient();

        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "janedoe@gmail.com",
            Password = "P@ssword1"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);

        await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));

        // Act
        var response = await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal("Username 'janedoe' already exists.", responseString);
    }



    [Fact]
    public async Task Register_Should_Throw_Error_Password_Validation()
    {
        // Arrange
        var client = _factory.GetAnonymousClient();

        var registrationRequest = new RegistrationRequest()
        {

            FirstName = "Jane",
            LastName = "Doe",
            UserName = "janedoe",
            Email = "janedoe@gmail.com",
            Password = "password"
        };

        var json = JsonConvert.SerializeObject(registrationRequest);
 
        // Act
        var response = await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json"));
        var responseString = await response.Content.ReadAsStringAsync();


        var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(responseString);
      
        // Asert
        Assert.NotNull(result.Errors["Password"]);
    }
}
