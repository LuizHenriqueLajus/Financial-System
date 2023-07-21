using Finance.Entities.Entities;
using Finance.WebApi.Models;
using Finance.WebApi.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/CreateToken")]
    public async Task<IActionResult> CreateToken([FromBody] InputModel Input)
    {
        if(string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
        {
            return Unauthorized();
        }

        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false); 
        if(result.Succeeded)
        {
            var token = new TokenJWTBuilder().AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("Finance System NET Core")
                .AddIssuer("Test.Security.Bearer")
                .AddAudience("Test.Security.Bearer")
                .AddClaim("UserAPINumber", "1")
                .AddExpiry(5)
                .Builder();

            return Ok(token.value);
        }
        else
        {
            return Unauthorized();
        }
    }
}
