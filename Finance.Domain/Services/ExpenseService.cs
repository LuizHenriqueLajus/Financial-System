using Finance.Domain.Interfaces.IExpense;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Entities.Entities;

namespace Finance.Domain.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpense _IExpense;

    public ExpenseService(IExpense iExpense)
    {
        _IExpense = iExpense;        
    }

    public async Task AddExpense(Expense expense)
    {
        var date = DateTime.UtcNow;
        expense.DataRegistration = date;
        expense.Year = date.Year;
        expense.Month = date.Month;

        var valid = expense.ValidatePropertyString(expense.Name, "Name");
        if (valid)
            await _IExpense.Add(expense);
    }

    public async Task UpdateExpense(Expense expense)
    {
        var date = DateTime.UtcNow;
        expense.DataAlteration = date;

        if (expense.Paid)
        {
            expense.DataPayment = date;
        }

        var valid = expense.ValidatePropertyString(expense.Name, "Name");
        if (valid)
            await _IExpense.Update(expense);
    }

    public async Task<object> LoadGraphics(string userEmail)
    {
        var userExpense = await _IExpense.ListUserExpense(userEmail);
        var previousExpenses = await _IExpense.ListUnpaidUserExpensesPreviousMonths(userEmail);

        var unpaidExpensesPreviousMonths = previousExpenses.Any() ? previousExpenses.ToList().Sum(x => x.Value) : 0;

        var paidExpenses = userExpense.Where(e => e.Paid && e.ExpenseType == Entities.Enums.EnumExpenseType.Bills)
            .Sum(x => x.Value);

        var outstandingExpenses = userExpense.Where(e => !e.Paid && e.ExpenseType == Entities.Enums.EnumExpenseType.Bills)
            .Sum(x => x.Value);

        var investments = userExpense.Where(e => e.ExpenseType == Entities.Enums.EnumExpenseType.Investment)
            .Sum(x => x.Value);

        return new 
        {
            success = "OK",
            paidExpenses = paidExpenses,
            outstandingExpenses = outstandingExpenses,
            unpaidExpensesPreviousMonths = unpaidExpensesPreviousMonths,
            investments = investments
        };
    }
}
