using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.InterfaceServices;

public interface ICategoryService
{
    Task AddCategory(Category category);
    Task UpdateCategory(Category category);
}
