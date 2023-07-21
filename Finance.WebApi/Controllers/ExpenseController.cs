using Finance.Domain.Interfaces.ICategory;
using Finance.Domain.Interfaces.IExpense;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Domain.Services;
using Finance.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpense _IExpense;
    private readonly IExpenseService _IExpenseService;

    public ExpenseController(IExpense IExpense, IExpenseService IExpenseService)
    {
        _IExpense = IExpense;
        _IExpenseService = IExpenseService;
    }

    [HttpGet("/api/ListUserExpense")]
    [Produces("application/json")]
    public async Task<object> ListUserExpense(string userEmail)
    {
        return await _IExpense.ListUserExpense(userEmail);
    }

    [HttpPost("/api/AddExpense")]
    [Produces("application/json")]
    public async Task<object> AddExpense(Expense expense)
    {
        await _IExpenseService.AddExpense(expense);

        return expense;
    }

    [HttpPut("/api/UpdateExpense")]
    [Produces("application/json")]
    public async Task<object> UpdateExpense(Expense expense)
    {
        await _IExpenseService.UpdateExpense(expense);

        return expense;
    }

    [HttpGet("/api/GetExpense")]
    [Produces("application/json")]
    public async Task<object> GetExpense(int id)
    {
        return await _IExpense.GetEntityById(id);
    }

    [HttpDelete("/api/DeleteExpense")]
    [Produces("application/json")]
    public async Task<object> DeleteExpense(int id)
    {
        try
        {
            var expense = await _IExpense.GetEntityById(id);

            await _IExpense.Delete(expense);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    [HttpGet("/api/LoadGraphics")]
    [Produces("application/json")]
    public async Task<object> LoadGraphics(string userEmail)
    {
        return await _IExpenseService.LoadGraphics(userEmail);
    }
}
