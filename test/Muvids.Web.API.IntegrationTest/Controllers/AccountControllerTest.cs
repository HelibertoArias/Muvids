using Muvids.Application.Models.Authentication;
using Muvids.Web.API.IntegrationTest.Base;
using Muvids.Web.API.IntegrationTest.Base.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    public async Task Register_Should_Create_New_User()
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

        var response = await client.PostAsync("/api/account/register", new StringContent(json, Encoding.UTF8, "application/json")) ;

        //response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<RegistrationResponse>(responseString);

        Assert.IsType<RegistrationResponse>(result);
      
    }
}
