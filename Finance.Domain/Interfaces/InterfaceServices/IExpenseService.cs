using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.InterfaceServices;

public interface IExpenseService
{
    Task AddExpense(Expense expense);
    Task UpdateExpense(Expense expense);
    Task<object> LoadGraphics(string userEmail);
}
