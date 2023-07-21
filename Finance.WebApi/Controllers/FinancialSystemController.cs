using Finance.Domain.Interfaces.IFinancialSystem;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Finance.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FinancialSystemController : ControllerBase
{
    private readonly IFinancialSystem _IFinancialSystem;
    private readonly IFinancialSystemService _IFinancialSystemService;
    public FinancialSystemController(IFinancialSystem IFinancialSystem, IFinancialSystemService IFinancialSystemService)
    {
        _IFinancialSystem = IFinancialSystem;
        _IFinancialSystemService = IFinancialSystemService;        
    }

    [HttpGet("/api/ListUserSystem")]
    [Produces("application/json")]
    public async Task<object> ListUserSystem(string userEmail)
    {
        return await _IFinancialSystem.ListUserSystem(userEmail);
    }

    [HttpPost("/api/AddFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> AddFinancialSystem(FinancialSystem financialSystem)
    {
        await _IFinancialSystemService.AddFinancialSystem(financialSystem);

        return Task.FromResult(financialSystem);
    }

    [HttpPut("/api/UpdateFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> UpdateFinancialSystem(FinancialSystem financialSystem)
    {
        await _IFinancialSystemService.UpdateFinancialSystem(financialSystem);

        return Task.FromResult(financialSystem);
    }

    [HttpGet("/api/GetFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> GetFinancialSystem(int id)
    {
        return await _IFinancialSystem.GetEntityById(id);
    }

    [HttpDelete("/api/DeleteFinancialSystem")]
    [Produces("application/json")]
    public async Task<object> DeleteFinancialSystem(int id)
    {
        try
        {
            var financialSystem = await _IFinancialSystem.GetEntityById(id);

            await _IFinancialSystem.Delete(financialSystem);
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}
