using Finance.Entities.Entities;

namespace Finance.Domain.Interfaces.InterfaceServices;

public interface IFinancialSystemService
{
    Task AddFinancialSystem(FinancialSystem financialSystem);
    Task UpdateFinancialSystem(FinancialSystem financialSystem);
}
