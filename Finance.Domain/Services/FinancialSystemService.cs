using Finance.Domain.Interfaces.IFinancialSystem;
using Finance.Domain.Interfaces.InterfaceServices;
using Finance.Entities.Entities;

namespace Finance.Domain.Services;

public class FinancialSystemService : IFinancialSystemService
{
    private readonly IFinancialSystem _iFinancialSystem;

    public FinancialSystemService(IFinancialSystem iFinancialSystem)
    {
        _iFinancialSystem = iFinancialSystem;
    }

    public async Task AddFinancialSystem(FinancialSystem financialSystem)
    {
        var valid = financialSystem.ValidatePropertyString(financialSystem.Name, "Name");

        if (valid)
        {
            var date = DateTime.Now;

            financialSystem.ClosingDay = 1;
            financialSystem.Year = date.Year;
            financialSystem.Month = date.Month;
            financialSystem.YearCopy = date.Year;
            financialSystem.MonthCopy = date.Month;
            financialSystem.GenerateExpenseCopy = true;

            await _iFinancialSystem.Add(financialSystem);
        }
    }

    public async Task UpdateFinancialSystem(FinancialSystem financialSystem)
    {
        var valid = financialSystem.ValidatePropertyString(financialSystem.Name, "Name");

        if (valid)
        {
            financialSystem.ClosingDay = 1;
            await _iFinancialSystem.Update(financialSystem);
        }
    }
}
