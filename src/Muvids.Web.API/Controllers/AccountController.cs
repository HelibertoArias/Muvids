﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Muvids.Application.Contracts.Identity;
using Muvids.Application.Models.Authentication;

namespace Muvids.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public AccountController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("authenticate", Name = "authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request)
    {
        return Ok(await _authenticationService.AuthenticateAsync(request));
    }

    [HttpPost("register", Name = "register")]
    public async Task<ActionResult<RegistrationResponse>> Register( RegistrationRequest request)
    {
        return Ok(await _authenticationService.RegisterAsync(request));
    }


}