using Finance.Domain.Interfaces.IExpense;
using Finance.Entities.Entities;
using Finance.Infra.Configuration;
using Finance.Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infra.Repository;

public class ExpenseRepository : GenericsRepository<Expense>, IExpense
{
    private readonly DbContextOptions<ContextBase> _OptionBuilder;

    public ExpenseRepository()
    {
        _OptionBuilder = new DbContextOptions<ContextBase>();
    }
    

    public async Task<IList<Expense>> ListUserExpense(string userEmail)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                (from system in database.FinancialSystem
                 join category in database.Category on system.Id equals category.IdSystem
                 join usersystem in database.UserFinancialSystem on system.Id equals usersystem.IdSystem
                 join expense in database.Expense on category.Id equals expense.IdCategory
                 where usersystem.UserEmail.Equals(userEmail) && system.Month == expense.Month && system.Year == expense.Year
                 select expense).AsNoTracking().ToListAsync();
        }
    }

    public async Task<IList<Expense>> ListUnpaidUserExpensesPreviousMonths(string userEmail)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                (from system in database.FinancialSystem
                 join category in database.Category on system.Id equals category.IdSystem
                 join usersystem in database.UserFinancialSystem on system.Id equals usersystem.IdSystem
                 join expense in database.Expense on category.Id equals expense.IdCategory
                 where usersystem.UserEmail.Equals(userEmail) && expense.Month < DateTime.Now.Month && !expense.Paid
                 select expense).AsNoTracking().ToListAsync();
        }
    }
}
