using Finance.Domain.Interfaces.Generics;
using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.IFinancialSystem;

public interface IFinancialSystem : IGeneric<FinancialSystem>
{
    Task<IList<FinancialSystem>> ListUserSystem(string userEmail);
}
