using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Entities.Entities;

[Table("FinancialSystem")]
public class FinancialSystem : Base
{   
    public int Month { get; set; }
    public int Year { get; set; }
    public int ClosingDay { get; set; }
    public bool GenerateExpenseCopy { get; set; }
    public int MonthCopy { get; set; }
    public int YearCopy { get; set; }
}
