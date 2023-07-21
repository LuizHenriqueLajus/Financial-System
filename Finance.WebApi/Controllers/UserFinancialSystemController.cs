using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Domain.Interfaces.IUserFinancialSystem;
using Finance.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserFinancialSystemController : ControllerBase
{
    private readonly IUserFinancialSystem _IUserFinancialSystem;
    private readonly IUserFinancialSystemService _IUserFinancialSystemService;

    public UserFinancialSystemController(IUserFinancialSystem IUserFinancialSystem, IUserFinancialSystemService IUserFinancialSystemService)
    {
        _IUserFinancialSystem = IUserFinancialSystem;
        _IUserFinancialSystemService = IUserFinancialSystemService;
    }

    [HttpGet("/api/ListUsersFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> ListUsersSystem(int idSystem)
    {
        return await _IUserFinancialSystem.ListUsersFinancialSystem(idSystem);
    }

    [HttpPost("/api/RegisterUserFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> RegisterUserFinancialSystem(int idSystem, string userEmail)
    {
        try
        {
            await _IUserFinancialSystemService.RegisterUserFinancialSystem(
            new UserFinancialSystem
            {
                IdSystem = idSystem,
                UserEmail = userEmail,
                Admin = false,
                CurrentSystem = true
            });
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    [HttpDelete("/api/DeleteUserFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> DeleteUserFinancialSystem(int id)
    {
        try
        {
            var userFinancialSystem = await _IUserFinancialSystem.GetEntityById(id);

            await _IUserFinancialSystem.Delete(userFinancialSystem);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}
