using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Domain.Interfaces.IUserFinancialSystem;
using Finance.Entities.Entities;

namespace Finance.Domain.Services;

public class UserFinancialSystemService : IUserFinancialSystemService
{
    private readonly IUserFinancialSystem _iUserFinancialSystem;

    public UserFinancialSystemService(IUserFinancialSystem iUserFinancialSystem)
    {
        _iUserFinancialSystem = iUserFinancialSystem;
    }

    public async Task RegisterUserFinancialSystem(UserFinancialSystem userFinancialSystem)
    {
        await _iUserFinancialSystem.Add(userFinancialSystem);
    }
}
