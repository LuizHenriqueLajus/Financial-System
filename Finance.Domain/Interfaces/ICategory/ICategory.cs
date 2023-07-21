using Finance.Domain.Interfaces.Generics;
using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.ICategory;

public interface ICategory : IGeneric<Category>
{
    Task<IList<Category>> ListUserCategories(string userEmail);
}
