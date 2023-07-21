using Finance.Entities.Entities;
using Finance.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Finance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("/api/AddUser")]
    public async Task<IActionResult> AddUser([FromBody] Login login)
    {
        if(string.IsNullOrWhiteSpace(login.email) ||
           string.IsNullOrWhiteSpace(login.password) ||
           string.IsNullOrWhiteSpace(login.cpf))
        {
            return Ok("Missing some data");
        }

        var user = new ApplicationUser
        {
            Email = login.email,
            UserName = login.email,
            CPF = login.cpf,
        };

        var result = await _userManager.CreateAsync(user, login.password);

        if (result.Errors.Any())
        {
            return Ok(result.Errors);
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var response_Return = await _userManager.ConfirmEmailAsync(user, code); 

        if(response_Return.Succeeded)
        {
            return Ok("User added!");
        }
        else
        {
            return Ok("Error confirming user registration!");
        }
    }
}
