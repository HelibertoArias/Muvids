using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Muvids.Web.API.Configurations;
using Newtonsoft.Json;
using System.Net;

namespace Muvids.Web.API.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class RandomController : ControllerBase
{
    private readonly GeneralSettings _generalSettingsOption;

    public RandomController(IOptions<GeneralSettings> generalSettingsOption)
    {
        this._generalSettingsOption = generalSettingsOption.Value ?? throw new ArgumentNullException(nameof(generalSettingsOption));
    }
    [HttpGet("getrandom", Name = "GetRandom")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRandom()
    {
        using (var client = new HttpClient())
        {
            try
            {
                var response = await client.GetAsync(_generalSettingsOption.UrlRandomService);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return BadRequest(response);
                }

                var content = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<int[]>(content);

                return Ok(JsonConvert.SerializeObject(new { Result = data?.First() }));
            }


            catch (HttpRequestException ex) { return GetMessage(ex.Message); }
            catch (Exception ex) { return GetMessage($"Ups: something happends: {ex.Message}"); }
        }

    }

    private IActionResult GetMessage(string message)
    {
        return BadRequest(JsonConvert.SerializeObject(new { error = message }));
    }
}
