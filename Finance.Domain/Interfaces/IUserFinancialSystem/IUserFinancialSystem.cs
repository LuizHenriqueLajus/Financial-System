using Finance.Domain.Interfaces.Generics;
using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.IUserFinancialSystem;

public interface IUserFinancialSystem : IGeneric<UserFinancialSystem>
{
    Task<IList<UserFinancialSystem>> ListUsersFinancialSystem(int IdSystem);

    Task RemoveUser(List<UserFinancialSystem> users);

    Task<UserFinancialSystem> GetUserByEmail(string userEmail);
}
