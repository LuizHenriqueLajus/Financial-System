using Finance.Domain.Interfaces.Generics;
using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.IExpense;

public interface IExpense : IGeneric<Expense>
{
    Task<IList<Expense>> ListUserExpense(string userEmail);

    Task<IList<Expense>> ListUnpaidUserExpensesPreviousMonths(string userEmail);
}
