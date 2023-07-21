using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.InterfaceServices;

public interface IUserFinancialSystemService
{
    Task RegisterUserFinancialSystem(UserFinancialSystem userFinancialSystem);
}
