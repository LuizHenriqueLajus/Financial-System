using Finance.Domain.Interfaces.IUserFinancialSystem;
using Finance.Entities.Entities;
using Finance.Infra.Configuration;
using Finance.Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infra.Repository;

public class UserFinancialSystemRepository : GenericsRepository<UserFinancialSystem>, IUserFinancialSystem
{
    private readonly DbContextOptions<ContextBase> _OptionBuilder;

    public UserFinancialSystemRepository()
    {
        _OptionBuilder = new DbContextOptions<ContextBase>();
    }

    public async Task<IList<UserFinancialSystem>> ListUsersFinancialSystem(int IdSystem)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                database.UserFinancialSystem
                .Where(s => s.IdSystem == IdSystem).AsNoTracking()
                .ToListAsync();
        }
    }

    public async Task<UserFinancialSystem> GetUserByEmail(string userEmail)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                database.UserFinancialSystem.AsNoTracking().FirstOrDefaultAsync(x => x.UserEmail.Equals(userEmail));
        }
    }

    public async Task RemoveUser(List<UserFinancialSystem> users)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            database.UserFinancialSystem
            .RemoveRange(users);

            await database.SaveChangesAsync();
        }
    }
}
