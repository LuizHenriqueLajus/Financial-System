using Finance.Domain.Interfaces.IFinancialSystem;
using Finance.Entities.Entities;
using Finance.Infra.Configuration;
using Finance.Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infra.Repository;

public class FinancialSystemRepository : GenericsRepository<FinancialSystem>, IFinancialSystem
{
    private readonly DbContextOptions<ContextBase> _OptionBuilder;

    public FinancialSystemRepository()
    {
        _OptionBuilder = new DbContextOptions<ContextBase>();
    }

    public async Task<IList<FinancialSystem>> ListUserSystem(string userEmail)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                (from s in database.FinancialSystem
                 join us in database.UserFinancialSystem on s.Id equals us.IdSystem
                 where us.UserEmail.Equals(userEmail)
                 select s).AsNoTracking().ToListAsync();
        }
    }
}
