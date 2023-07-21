using Finance.Domain.Interfaces.ICategory;
using Finance.Entities.Entities;
using Finance.Infra.Configuration;
using Finance.Infra.Repository.Generics;
using Microsoft.EntityFrameworkCore;

namespace Finance.Infra.Repository;

public class CategoryRepository : GenericsRepository<Category>, ICategory
{
    private readonly DbContextOptions<ContextBase> _OptionBuilder;

    public CategoryRepository()
    {
        _OptionBuilder = new DbContextOptions<ContextBase>();
    }

    public async Task<IList<Category>> ListUserCategories(string userEmail)
    {
        using (var database = new ContextBase(_OptionBuilder))
        {
            return await
                (from system in database.FinancialSystem
                 join category in database.Category on system.Id equals category.IdSystem
                 join usersystem in database.UserFinancialSystem on system.Id equals usersystem.IdSystem
                 where usersystem.UserEmail.Equals(userEmail) && usersystem.CurrentSystem
                 select category).AsNoTracking().ToListAsync();
        }
    }
}
